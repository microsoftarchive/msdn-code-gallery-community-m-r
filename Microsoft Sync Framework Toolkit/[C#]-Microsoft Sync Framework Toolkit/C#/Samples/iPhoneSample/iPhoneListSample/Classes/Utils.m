//
//  ItemDetailViewController.h
//  iPhoneListSample
//
//  Copyright 2010 Microsoft. All rights reserved.
//

#import "Utils.h"
#import "SBJSON.h"

@implementation Utils

// helper alert method
+ (void) showAlert:(NSString*)title withMessage:(NSString*)msg withDelegate:(id)delegate
{
	UIAlertView *alert = [[UIAlertView alloc] initWithTitle:title message:msg
						  	 delegate:delegate cancelButtonTitle:@"OK" otherButtonTitles:nil, nil];
	[alert show];
	[alert release];		
}

#pragma mark -
#pragma mark SyncController helpers

// Returns a OData JSon string with to send to the service in the DownloadChanges or UploadChanges 
// HTTP request
+(NSString *) GetODataJsonPayload:(NSString*)serverBlob withChanges:(NSMutableArray*)changes
{
	SBJSON *json = [[SBJSON alloc] init];
	[json setSortKeys:YES];
	[json setHumanReadable:YES];
	
	if (changes == nil)
	{
		changes = [NSArray arrayWithObjects:nil];
	}
	
	NSMutableDictionary *mainDict = [[NSMutableDictionary alloc] init];		
	[mainDict setObject:[NSDictionary dictionaryWithObjects:[NSArray arrayWithObjects:[NSDictionary dictionaryWithObjects:[NSArray arrayWithObjects:[NSNumber numberWithBool:false], serverBlob, nil] 
																												  forKeys:[NSArray arrayWithObjects:@"moreChangesAvailable", @"serverBlob", nil]], 
															 changes, 
															 nil] 
													forKeys:[NSArray arrayWithObjects:@"__sync", 
															 @"results", 
															 nil]] 
				 forKey:@"d"];
	
	NSString *string =[json stringWithObject:mainDict];
	
	[mainDict release];
	[json release];
	return string;
}

// This methods parses the ODataJson response from the server
// which includes the list of changes for the specified serverBlob.
// The response includes: (1) the new serverBlob which is stored locally 
// in the Anchor entity; (2) a flag stating if more changes are available; and 
// (3) the list of changes since the specified serverBlob.
//
// The list of changes is parsed and the changes are stored locally.  If the
// entity is new.  It will be created and stored to sqllite store, else the local
// entity is updated to reflect the new changes. If the change has the tag isDeleted set to true
// we delete the entity from the local store if present.
+(bool) ProcessODataJsonChanges:(NSString*)jsonChanges withAnchor:(Anchor**)anc withContext:(NSManagedObjectContext *)context
{
	SBJSON *json = [[SBJSON alloc] init];
	
	id myObj = [json objectWithString:jsonChanges];
	
	id syncData = [[myObj valueForKey:@"d"] valueForKey:@"__sync"];
	
	// get has more changes
	bool moreChangesAvailable = [[syncData valueForKey:@"moreChangesAvailable"] boolValue];
	
	// Update Anchor
	(*anc).syncBlob = [syncData valueForKey:@"serverBlob"];
	
	NSArray * results = [[myObj valueForKey:@"d"] valueForKey:@"results"];
	for (id table in results) {
		id metadata = [table valueForKey:@"__metadata"];
		id type = [metadata valueForKey:@"type"];
		
		if ([type isEqualToString:@"DefaultScope.Tag"])
		{
			[Utils populateTag:table withMetadata:metadata withContext:context];
		}
		if ([type isEqualToString:@"DefaultScope.List"])
		{ 
			[Utils populateList:table withMetadata:metadata withContext:context];
		}
		else if ([type isEqualToString:@"DefaultScope.Status"])			
		{
			[Utils populateStatus:table withMetadata:metadata withContext:context];
		}
		else if ([type isEqualToString:@"DefaultScope.Priority"])
		{ 
			[Utils populatePriority:table withMetadata:metadata withContext:context];
		}
		else if ([type isEqualToString:@"DefaultScope.Item"])
		{
			[Utils populateItem:table withMetadata:metadata withContext:context];
		}
		else if ([type isEqualToString:@"DefaultScope.TagItemMapping"])
		{
			[Utils populateTagItemMapping:table withMetadata:metadata withContext:context];
		}
	}
	
	[json release];

	return moreChangesAvailable;
}

// returns the anchor holding the serverBlob needed to syncDown to the service
// alongside with the userID
+(Anchor*) getAnchor:(NSManagedObjectContext*)context
{
	NSFetchRequest * fetch = [[NSFetchRequest alloc] init];
	[fetch setEntity:[NSEntityDescription entityForName:@"Anchor" inManagedObjectContext:context]];
	NSArray * result = [context executeFetchRequest:fetch error:nil];
	Anchor *anc = nil;
	if (result.count != 0)
	{
		anc = [result objectAtIndex:0];
	}
	
	[fetch release];
	return anc;
}


// returns true if the client has never synced
// returns false after the client has done one DownloadChanges
+ (bool) clientHasSynced: (NSManagedObjectContext*) context
{
	Anchor *anc = [Utils getAnchor:context];
	return [anc.hasSynced boolValue];
}

// returns true if the client has changes that have been locally modified (ie. lists, items or tagItemMappings)
// returns false if there are no changes or if the client has never synced to the sync service
+ (bool) hasChanges:(NSManagedObjectContext*) context
{
	
	// if client has not synced to server UploadChanges is not allowed
	if ([Utils clientHasSynced:context])
	{
		// there are changes to sync if the client has modified the Item, List or TagItemMapping tables
		// The status, tag and priority table are readonly
		// changes to these tables are not uploaded to sync service
		
		NSFetchRequest *fetchItems = [[[NSFetchRequest alloc]init]autorelease];
		[fetchItems setEntity:[NSEntityDescription entityForName:@"Item" inManagedObjectContext:context]];
		[fetchItems setPredicate:[NSPredicate predicateWithFormat:@"(localUpdate == YES)"]];
		NSArray *items = [context executeFetchRequest:fetchItems error:nil];
		
		NSFetchRequest *fetchLists = [[[NSFetchRequest alloc] init]autorelease];
		[fetchLists setEntity:[NSEntityDescription entityForName:@"List" inManagedObjectContext:context]];
		[fetchLists setPredicate:[NSPredicate predicateWithFormat:@"(localUpdate == YES)"]];
		NSArray *lists = [context executeFetchRequest:fetchLists error:nil];

		NSFetchRequest *fetchTagItemMapping = [[[NSFetchRequest alloc] init]autorelease];
		[fetchTagItemMapping setEntity:[NSEntityDescription entityForName:@"TagItemMapping" inManagedObjectContext:context]];
		[fetchTagItemMapping setPredicate:[NSPredicate predicateWithFormat:@"(localUpdate == YES)"]];
		NSArray *tims = [context executeFetchRequest:fetchTagItemMapping error:nil];
		
		return (lists.count + items.count + tims.count) > 0;	
	}
	else 
	{
		return false;
	}
}

// this method is called when a new user logs into the client
// it cleans up the cache for the previous user
+ (void) cleanupCache:(NSManagedObjectContext*) context
{
	// Cleanup tags
	NSFetchRequest *fetchTag = [[[NSFetchRequest alloc] init] autorelease];
	[fetchTag setEntity:[NSEntityDescription entityForName:@"Tag" inManagedObjectContext:context]];
	NSArray *tagList = [context executeFetchRequest:fetchTag error:nil];
	for (id entity in tagList)
	{
		[context deleteObject:entity];
	}
	
	// Cleanup Status
	NSFetchRequest *fetchStatus = [[[NSFetchRequest alloc] init] autorelease];
	[fetchStatus setEntity:[NSEntityDescription entityForName:@"Status" inManagedObjectContext:context]];
	NSArray *statusList = [context executeFetchRequest:fetchStatus error:nil];
	for (id entity in statusList)
	{
		[context deleteObject:entity];
	}
	
	
	// Cleanup Priority
	NSFetchRequest *fetchPriority = [[[NSFetchRequest alloc] init] autorelease];
	[fetchPriority setEntity:[NSEntityDescription entityForName:@"Priority" inManagedObjectContext:context]];
	NSArray *priorityList = [context executeFetchRequest:fetchPriority error:nil];
	for (id entity in priorityList)
	{
		[context deleteObject:entity];
	}
	
	// Cleanup Item
	NSFetchRequest *fetchItem = [[[NSFetchRequest alloc] init] autorelease];
	[fetchItem setEntity:[NSEntityDescription entityForName:@"Item" inManagedObjectContext:context]];
	NSArray *itemList = [context executeFetchRequest:fetchItem error:nil];
	for (id entity in itemList)
	{
		[context deleteObject:entity];
	}
	
	// Cleanup List
	NSFetchRequest *fetchList = [[[NSFetchRequest alloc] init] autorelease];
	[fetchList setEntity:[NSEntityDescription entityForName:@"List" inManagedObjectContext:context]];
	NSArray *listList = [context executeFetchRequest:fetchList error:nil];
	for (id entity in listList)
	{
		[context deleteObject:entity];
	}
	
	// Cleanup TagItemMapping
	NSFetchRequest *fetchTagItemMapping = [[[NSFetchRequest alloc] init] autorelease];
	[fetchTagItemMapping setEntity:[NSEntityDescription entityForName:@"TagItemMapping" inManagedObjectContext:context]];
	NSArray *tagItemMappingList = [context executeFetchRequest:fetchTagItemMapping error:nil];
	for (id entity in tagItemMappingList)
	{
		[context deleteObject:entity];
	}	
	
	// Cleanup Anchor
	NSFetchRequest *fetchAnchor = [[[NSFetchRequest alloc] init] autorelease];
	[fetchAnchor setEntity:[NSEntityDescription entityForName:@"Anchor" inManagedObjectContext:context]];
	NSArray *anchorList = [context executeFetchRequest:fetchAnchor error:nil];
	for (id entity in anchorList)
	{
		[context deleteObject:entity];
	}
	
	[Utils saveManagedObjects:context];
	
}

#pragma mark -
#pragma mark Core Data Helper methods

// method saves all the modified entities to local store
+ (void)saveManagedObjects:(NSManagedObjectContext *)context
{
	NSError * error = nil;
	if (![context save:&error]) {
		[Utils showAlert:@"Error saving" withMessage:@"Could not save context.  View log for details" withDelegate:nil];
		NSLog(@"error saving! %@", error);
		NSLog(@"Failed to save to data store: %@", [error localizedDescription]);
		NSArray* detailedErrors = [[error userInfo] objectForKey:NSDetailedErrorsKey];
		if(detailedErrors != nil && [detailedErrors count] > 0) {
			for(NSError* detailedError in detailedErrors) {
				NSLog(@"  DetailedError: %@", [detailedError userInfo]);
			}
		}
		else {
			NSLog(@"  %@", [error userInfo]);
		}
		abort();
	}
}					

#pragma mark -
#pragma mark data format helpers

// method returns date from string, if the string is nil or null the method will return 
// a nil NSDate
+ (NSDate *)parseDateTime:(id)strDate
{
	
	if (strDate == nil)
	{
		return nil;
	}
	
	if (strDate == [NSNull null])
	{
		return nil;
	}
	
	NSString *str = (NSString*) strDate;
	NSDateFormatter* customDateFormatter = [[NSDateFormatter alloc]init];
	[customDateFormatter setDateStyle:NSDateFormatterNoStyle];
	[customDateFormatter setDateFormat:@"yyyy-MM-dd HH:mm:ss.SS"];
	NSDate *date = [customDateFormatter dateFromString:[str stringByReplacingOccurrencesOfString:@"T" withString:@" "]];
	[customDateFormatter release];
	return date;
}

// method returns date from string, if the string is nil or null the method will return 
// a nil NSDate
+ (NSDate *)parseDate:(id)strDate
{
	
	if (strDate == nil)
	{
		return nil;
	}
	
	if (strDate == [NSNull null])
	{
		return nil;
	}
	
	NSString *str = (NSString*) strDate;
	NSDateFormatter* customDateFormatter = [[NSDateFormatter alloc]init];
	[customDateFormatter setDateStyle:NSDateFormatterNoStyle];
	[customDateFormatter setDateFormat:@"yyyy-MM-dd HH:mm:ss"];
	NSDate *date = [customDateFormatter dateFromString:[str stringByReplacingOccurrencesOfString:@"T" withString:@" "]];
	[customDateFormatter release];
	return date;
}

// method returns a string from NSDate if the date is nil the method returns NSNull
+ (id) dateTimeToString:(NSDate*)date
{
	if (date == nil)
	{
		return [NSNull null];
	}
	/*
		"2010-06-07T12:42:00.94"
	 */	
	
	NSDateFormatter* customDateFormatter = [[NSDateFormatter alloc]init];
	[customDateFormatter setDateStyle:NSDateFormatterNoStyle];
	[customDateFormatter setDateFormat:@"yyyy-MM-dd HH:mm:ss.SS"];
	
	NSString *dateString = [[customDateFormatter stringFromDate:date] stringByReplacingOccurrencesOfString:@" " withString:@"T"];
	[customDateFormatter release];
	return dateString;
}

// method returns a string from NSDate if the date is nil the method returns NSNull
+ (id) dateToString:(NSDate*)date
{
	if (date == nil)
	{
		return [NSNull null];
	}
	/*
	 "2010-06-07T12:42:00.94"
	 */	
	
	NSDateFormatter* customDateFormatter = [[NSDateFormatter alloc]init];
	[customDateFormatter setDateStyle:NSDateFormatterNoStyle];
	[customDateFormatter setDateFormat:@"yyyy-MM-dd HH:mm:ss"];
	
	NSString *dateString = [[customDateFormatter stringFromDate:date] stringByReplacingOccurrencesOfString:@" " withString:@"T"];
	[customDateFormatter release];
	return dateString;
}

// methods returns a new GUID in string format
+ (NSString *)GetUUID
{
	CFUUIDRef theUUID = CFUUIDCreate(NULL);
	CFStringRef string = CFUUIDCreateString(NULL, theUUID);
	CFRelease(theUUID);
	return (NSString *)string;
}


#pragma mark -
#pragma mark Helper methods used to populate store from DownloadChanges response


// Returns true if json object dict has the isDeleted tag set to true,
// false if isDeleted is not present in the json dictionary or if the value is false
+(bool) isDeleted: (id)dict;
{
	bool deleted = false;
	
	id deletedKey = [dict valueForKey:@"isDeleted"];
	
	if (deletedKey != nil)
	{
		deleted = [deletedKey boolValue];
	}
	
	return deleted;
}

// populates the TagEntity store with the values from the json object dict
+ (void)populateTag: (id)dict withMetadata:(id)metadata withContext:(NSManagedObjectContext*) context;
{
	bool isTombstone = [Utils isDeleted:metadata];
	TagEntity *tag = nil;
	
	NSFetchRequest *fetch = [[NSFetchRequest alloc] init];
	[fetch setEntity:[NSEntityDescription entityForName:@"Tag" inManagedObjectContext:context]];
	[fetch setPredicate:[NSPredicate predicateWithFormat:@"ID = %@", [dict valueForKey:@"ID"]]];
	NSArray *results = [context executeFetchRequest:fetch error:nil];
	if (results.count == 1) 
	{
		tag = [results objectAtIndex:0];
	}
	
	
	if (tag == nil && !isTombstone)
	{
		// insert new status
		tag = [NSEntityDescription insertNewObjectForEntityForName:@"Tag" inManagedObjectContext:context];
	}
	
	
	// if its deleted and we have not seen it ignore it
	// else delete it
	if (isTombstone && tag != nil)
	{
		[context deleteObject:tag];
	}
	else if (!isTombstone)
	{
		tag.ID = [dict valueForKey:@"ID"];
		tag.tagName = [dict valueForKey:@"Name"];
	}
	
	[fetch release];
}


// populates the StatusEntity store with the values from the json object dict
+ (void)populateStatus: (id)dict withMetadata:(id)metadata withContext:(NSManagedObjectContext*) context;
{
	bool isTombstone = [Utils isDeleted:metadata];
	StatusEntity *status = nil;
	
	NSFetchRequest *fetch = [[NSFetchRequest alloc] init];
	[fetch setEntity:[NSEntityDescription entityForName:@"Status" inManagedObjectContext:context]];
	[fetch setPredicate:[NSPredicate predicateWithFormat:@"ID = %@", [dict valueForKey:@"ID"]]];
	NSArray *results = [context executeFetchRequest:fetch error:nil];
	if (results.count == 1) 
	{
		status = [results objectAtIndex:0];
	}
	
	
	if (status == nil && !isTombstone)
	{
		// insert new status
		status = [NSEntityDescription insertNewObjectForEntityForName:@"Status" inManagedObjectContext:context];
	}
	

	// if its deleted and we have not seen it ignore it
	// else delete it
	if (isTombstone && status != nil)
	{
		[context deleteObject:status];
	}
	else if (!isTombstone)
	{
		status.ID = [dict valueForKey:@"ID"];
		status.statusName = [dict valueForKey:@"Name"];
	}
	
	[fetch release];
}

// populates the PriorityEntity store with the values from the json object dict
+ (void)populatePriority: (id)dict withMetadata:(id)metadata withContext:(NSManagedObjectContext*) context;
{
	bool isTombstone = [Utils isDeleted:metadata];
	PriorityEntity *priority = nil;
	
	NSFetchRequest *fetch = [[NSFetchRequest alloc] init];
	[fetch setEntity:[NSEntityDescription entityForName:@"Priority" inManagedObjectContext:context]];
	[fetch setPredicate:[NSPredicate predicateWithFormat:@"ID = %@", [dict valueForKey:@"ID"]]];
	
	NSArray *results = [context executeFetchRequest:fetch error:nil];
	if (results.count == 1) 
	{
		priority = [results objectAtIndex:0];
	}
	
	
	if (priority == nil && !isTombstone)
	{
		// insert new status
		priority = [NSEntityDescription insertNewObjectForEntityForName:@"Priority" inManagedObjectContext:context];
	}
	
	
	// if its deleted and we have not seen it ignore it
	// else delete it
	if (isTombstone && priority != nil)
	{
		[context deleteObject:priority];
	}
	else if (!isTombstone) 
	{
		priority.ID = [dict valueForKey:@"ID"];
		priority.priorityName = [dict valueForKey:@"Name"];
	}
	
	[fetch release];
}

// populates the ListEntity store with the values from the json object dict
+ (void)populateList: (id)dict withMetadata:(id)metadata withContext:(NSManagedObjectContext*) context;
{
	bool isTombstone = [Utils isDeleted:metadata];
	ListEntity *list = nil;
	
	NSFetchRequest *fetch = [[NSFetchRequest alloc] init];
	[fetch setEntity:[NSEntityDescription entityForName:@"List" inManagedObjectContext:context]];
	[fetch setPredicate:[NSPredicate predicateWithFormat:@"ID LIKE[cd] %@", [dict valueForKey:@"ID"]]];
	
	NSArray *results = [context executeFetchRequest:fetch error:nil];
	if (results.count == 1) 
	{
		list = [results objectAtIndex:0];
	}
	
	
	if (list == nil && !isTombstone)
	{
		// insert new status
		list = [NSEntityDescription insertNewObjectForEntityForName:@"List" inManagedObjectContext:context];
	}
	
	
	// if its deleted and we have not seen it ignore it
	// else delete it
	if (isTombstone && list != nil)
	{
		[context deleteObject:list];
	}
	else if (!isTombstone) 
	{
		id desc = [dict valueForKey:@"Description"];
		list.IsTombstone = [NSNumber numberWithBool:NO];
		list.localUpdate = [NSNumber numberWithBool:NO];
		list.listName = [NSString stringWithString:[dict valueForKey:@"Name"]];
		list.ID = [NSString stringWithString:[dict valueForKey:@"ID"]];
		if (desc != [NSNull null])
		{
			list.listDescription = [NSString stringWithString:desc];
		}
		else 
		{
			list.listDescription =@"";
		}
		list.createDate = [Utils parseDateTime:[dict valueForKey:@"CreatedDate"]];
	}
	
	[fetch release];
}

// populates the ItemEntity store with the values from the json object dict
+ (void)populateItem:  (id)dict withMetadata:(id)metadata withContext:(NSManagedObjectContext*) context;
{
	bool isTombstone = [Utils isDeleted:metadata];
	ItemEntity *item = nil;
	
	NSFetchRequest *fetch = [[NSFetchRequest alloc] init];
	[fetch setEntity:[NSEntityDescription entityForName:@"Item" inManagedObjectContext:context]];
	[fetch setPredicate:[NSPredicate predicateWithFormat:@"ID LIKE[cd] %@", [dict valueForKey:@"ID"]]];
	NSArray *results = [context executeFetchRequest:fetch error:nil];
	if (results.count == 1) 
	{
		item = [results objectAtIndex:0];
	}
	
	
	if (item == nil && !isTombstone)
	{
		// insert new item when it doesnt exist
		item = [NSEntityDescription insertNewObjectForEntityForName:@"Item" inManagedObjectContext:context];
	}
	
	
	// if its deleted and we have not seen it ignore it
	// else delete it
	if (isTombstone && item != nil)
	{
		[context deleteObject:item];
	}
	else if (!isTombstone)  // modify new or existing live item
	{
	
		// get optional values
		id priority = [dict valueForKey:@"Priority"];
		id status = [dict valueForKey:@"Status"];
		
		item.IsTombstone = [NSNumber numberWithBool:NO];
		item.localUpdate = [NSNumber numberWithBool:NO];
		item.itemName = [dict valueForKey:@"Name"];
		item.ID = [dict valueForKey:@"ID"];
		item.itemDescription = [dict valueForKey:@"Description"];
		item.startDate = [Utils parseDate:[dict valueForKey:@"StartDate"]] ;
		item.endDate = [Utils parseDate:[dict valueForKey:@"EndDate"]];						
		item.listID = [dict valueForKey:@"ListID"];
		
		if (priority == [NSNull null])
		{
			priority = [NSNumber numberWithInt:-1];
		}
		item.priority = priority;
		
		if (status == [NSNull null])
		{
			status = [NSNumber numberWithInt:-1];
		}
		item.status = status;
		[item logItem];
	}
	
	[fetch release];
}

// populates the TagItemMappingEntity store with the values from the json object dict
+ (void)populateTagItemMapping:(id)dict withMetadata:(id)metadata withContext:(NSManagedObjectContext*)context
{
	bool isTombstone = [Utils isDeleted:metadata];
	TagItemMappingEntity *tagItemMapping = nil;
	
	NSFetchRequest *fetch = [[NSFetchRequest alloc] init];
	[fetch setEntity:[NSEntityDescription entityForName:@"TagItemMapping" inManagedObjectContext:context]];
	[fetch setPredicate:[NSPredicate predicateWithFormat:@"tagID == %@ AND itemID LIKE[cd] %@", [dict valueForKey:@"TagID"], [dict valueForKey:@"ItemID"]]];
	NSArray *results = [context executeFetchRequest:fetch error:nil];
	if (results.count == 1) 
	{
		tagItemMapping = [results objectAtIndex:0];
	}
	
	
	if (tagItemMapping == nil && !isTombstone)
	{
		// insert new item when it doesnt exist
		tagItemMapping = [NSEntityDescription insertNewObjectForEntityForName:@"TagItemMapping" inManagedObjectContext:context];
	}
	
	
	// if its deleted and we have not seen it ignore it
	// else delete it
	if (isTombstone && tagItemMapping != nil)
	{
		[context deleteObject:tagItemMapping];
	}
	else if (!isTombstone)  // modify new or existing live item
	{
		
		tagItemMapping.IsTombstone = [NSNumber numberWithBool:NO];
		tagItemMapping.localUpdate = [NSNumber numberWithBool:NO];
		tagItemMapping.itemID = [dict valueForKey:@"ItemID"];
		tagItemMapping.tagID = [dict valueForKey:@"TagID"];
	}
	
	[fetch release];	
}

#pragma mark -
#pragma mark Helper methods for cascade deletion of entities

+ (void) deleteList:(ListEntity*)list inContext:(NSManagedObjectContext*)context
{	
	// look up items that reference list
	NSFetchRequest *fetch = [[[NSFetchRequest alloc] init]autorelease];
	[fetch setEntity:[NSEntityDescription entityForName:@"Item" inManagedObjectContext:context]];
	[fetch setPredicate:[NSPredicate predicateWithFormat:@"listID LIKE[cd] %@", [list ID]]];
	NSArray *listItems = [context executeFetchRequest:fetch error:nil];
	
	// delete items that reference list
	for (ItemEntity* item in listItems)
	{
		[Utils deleteItem:item inContext:context];
	}
	
	// Save listID to create tombstone
	NSString *listId = [NSString stringWithString:[list ID]];
	
	// Delete list to trigger refresh
	[context deleteObject:list];
	
	// create tombstone to sync up
	ListEntity *listTombstone = [NSEntityDescription insertNewObjectForEntityForName:@"List" inManagedObjectContext:context];
	listTombstone.ID = [NSString stringWithString:listId];
	listTombstone.localUpdate = [NSNumber numberWithBool:YES];
	listTombstone.IsTombstone = [NSNumber numberWithBool:YES];
	listTombstone.listName = @"";
	listTombstone.listDescription = @"";
	listTombstone.createDate = nil;
	
	// Save the context.
	[Utils saveManagedObjects:context];

}

+ (void) deleteItem:(ItemEntity*)item inContext:(NSManagedObjectContext*)context
{
	// delete tags that reference item
	NSFetchRequest *fetch = [[[NSFetchRequest alloc] init]autorelease];
	[fetch setEntity:[NSEntityDescription entityForName:@"TagItemMapping" inManagedObjectContext:context]];
	[fetch setPredicate:[NSPredicate predicateWithFormat:@"itemID LIKE[cd] %@", item.ID]];
	NSArray *tagItemMappingList = [context executeFetchRequest:fetch error:nil];
	for (TagItemMappingEntity* tim in tagItemMappingList)
	{
		[Utils deleteTagItemMapping:tim inContext:context];
	}

	// save item id to create tombstone
	NSString *itemID = [NSString stringWithString:[item ID]];
	
	// delete item
	[context deleteObject:item];
	
	// create tombstone for item
	ItemEntity *tombstoneItem = [NSEntityDescription insertNewObjectForEntityForName:@"Item" inManagedObjectContext:context];
	tombstoneItem.localUpdate = [NSNumber numberWithBool:YES];
	tombstoneItem.IsTombstone = [NSNumber numberWithBool:YES];
	tombstoneItem.ID = [NSString stringWithString:itemID];
	tombstoneItem.listID = @"";
	tombstoneItem.itemName = @"";
	tombstoneItem.itemDescription = @"";
	tombstoneItem.priority = [NSNumber numberWithInt:-1];
	tombstoneItem.status = [NSNumber numberWithInt:-1];
	tombstoneItem.startDate = nil;
	tombstoneItem.endDate = nil;
	
	// Save the context.
	[Utils saveManagedObjects:context];
	
}

+ (void) deleteTagItemMapping:(TagItemMappingEntity*)tagItemMapping inContext:(NSManagedObjectContext*)context
{
	
	// save item id and tag id to create tombstone
	NSString *itemID = [NSString stringWithString:[tagItemMapping itemID]];
	NSNumber *tagID = [NSNumber numberWithInt:[tagItemMapping.tagID intValue]];
	
	// delete tagItemMapping
	[context deleteObject:tagItemMapping];
	
	// create tombstone for tagItemMapping
	TagItemMappingEntity *tombstoneTagItemMapping = [NSEntityDescription insertNewObjectForEntityForName:@"TagItemMapping" inManagedObjectContext:context];
	tombstoneTagItemMapping.localUpdate = [NSNumber numberWithBool:YES];
	tombstoneTagItemMapping.IsTombstone = [NSNumber numberWithBool:YES];
	tombstoneTagItemMapping.itemID = itemID;
	tombstoneTagItemMapping.tagID = tagID;
	
	// Save the context.
	[Utils saveManagedObjects:context];
	
}
@end
