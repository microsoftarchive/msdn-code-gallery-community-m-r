//
//  SyncController.m
//  iPhoneListSample
//
//  Copyright 2010 Microsoft. All rights reserved.
//

#import "SyncController.h"
#import "Utils.h"
#import "ListViewController.h"
#import "TagItemMappingEntity.h"
#import "SBJSON.h"

@implementation SyncController

@synthesize managedObjectContext, delegate, baseURL, downloadURL, uploadURL;


- (id)initWithContext:(NSManagedObjectContext *)inContext delegate:(id)myDelegate
{
	if ([super init])
	{
		self.managedObjectContext = inContext;
		self.delegate = myDelegate;
		
		// Get the base service address from the Info.plist file
		NSString *filePath = [[NSBundle mainBundle] pathForResource:@"Info" ofType:@"plist"];  
		NSMutableDictionary* plistDict = [[NSMutableDictionary alloc] initWithContentsOfFile:filePath];
		self.baseURL = [NSString stringWithString:[plistDict valueForKey:@"ServiceRoot"]];				

		// anc stores the userID which needs to be passed in the UploadChanges and DownloadChanges urls
		Anchor *anc = [Utils getAnchor:inContext];
		self.uploadURL = [NSURL URLWithString:[NSString stringWithFormat:@"%@/DefaultScopeSyncService.svc/?syncScope=DefaultScope&operation=UploadChanges&userid=%@", self.baseURL, [anc valueForKey:@"userID"]]];
		self.downloadURL = [NSURL URLWithString:[NSString stringWithFormat:@"%@/DefaultScopeSyncService.svc/?syncScope=DefaultScope&operation=DownloadChanges&userid=%@", self.baseURL, [anc valueForKey:@"userID"]]];
		
		[plistDict release];
	}
	
	return self;
}


- (void)dealloc
{
	[super dealloc];
	[managedObjectContext release];
	[uploadURL release];
	[downloadURL release];
	[baseURL release];
}

#pragma mark
#pragma mark Synchronize

// This method is called when the app wants to synchronize changes
// after downloadChanges has completed delegate.syncDone will be fired, letting
// the caller that the synchronization completed
-(void) synchronize
{
	// If there are changes to upload, need to uploadChanges first
	// when uploadChanges finishes succesfully, it will call downloadChanges.
	// Else if there are no changes to upload, or this is the first time sync
	// call downloadChanges directly
	if ([Utils hasChanges:self.managedObjectContext])
	{
		[self uploadChanges];
	}
	else 
	{
		[self downloadChanges];
	}
}

#pragma mark 
#pragma mark DownloadChanges methods


// Method sends request to sync service to download changes if any
// it sends the serverBlob stored in Anchor.syncBlob in the 
// http request payload (in ODataJson format). 
// The httpRequest  returns with the service with the list of changes
// in a json string.  These are sent to the processDownloadResponse 
// which saves them to the local store.  (syncBlob will be empty
// for the first time sync)
- (void)downloadChanges
{
	
	Anchor *anc = [Utils getAnchor:managedObjectContext];
	NSMutableURLRequest *request = [NSMutableURLRequest requestWithURL:downloadURL];
	[request setHTTPMethod:@"POST"];
	[request addValue:@"Application/json" forHTTPHeaderField:@"content-type"];
	[request addValue:@"Application/json" forHTTPHeaderField:@"accept"];
	[request setValue:@"gzip" forHTTPHeaderField:@"Accept-Encoding"];  	
	[request setHTTPBody: [[Utils GetODataJsonPayload:anc.syncBlob withChanges:nil] dataUsingEncoding:NSUTF8StringEncoding]];	
	
	NSURLResponse *response = nil; 
	NSError *error = nil;
	NSData *data = [NSURLConnection sendSynchronousRequest:request returningResponse:&response error:&error];	
	if (error != nil)
	{
		[Utils showAlert:@"Error downloading changes" withMessage:[error localizedDescription] withDelegate:delegate];
	}
	else 
	{
		NSString *responseString = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
		NSLog(@"response string: %@", responseString);
		[self processDownloadResponse:responseString];
		[responseString release];
	}
	
	[error release];
}

// This methods calls ParseODataJSonChanges method to parse json changes string
// and apply changes to local store
//
// If moreChangesAvailable is true, the method calls again downloadChanges 
// until moreChangesAvailable is false. When false, the delegate method syncDone is called
// which notifies the app that sync has completed.
- (void)processDownloadResponse:(NSString *) responseString {
	
	
	Anchor *anc = [Utils getAnchor:self.managedObjectContext];	
	bool moreChangesAvailable = [Utils ProcessODataJsonChanges:responseString withAnchor:&anc withContext:self.managedObjectContext];
	
	// save changes to store
	[Utils saveManagedObjects:self.managedObjectContext]; 

	
	if (moreChangesAvailable) 
	{
		// if more changes call downloadChanges again
		[self downloadChanges];
	}
	else 
	{
		if (![anc.hasSynced boolValue])
		{
			// store that a download has been completed
			// to enable future uploads
			anc.hasSynced = [NSNumber numberWithBool:YES];
		}
		

		// Tell NavController we're done
		if(delegate && [delegate respondsToSelector:@selector(syncDone:)])
		{
			[delegate syncDone:self];
		}
	}
		
}



#pragma mark 
#pragma mark UploadChanges methods

// Download changes after UploadChanges has completed succesfully
-(void) uploadDone
{
	[self downloadChanges];
}

// Sends an http request to the sync service with changes that need to be uploaded.
// These are encoded in a Json string which is written to the http request payload.
// After the http request has been processed, the server will return 
// a Json string in the http response which includes: 
// (1) the new serverBlob which will be stored in Anchor.syncBlob
// (2) a list of resolved syncConflicts (if any)
// (3) a list of syncErrors (if any)
//  If the request did not succeed, the server will send an error in the httpResponse, instead
// of the Json string
- (void)uploadChanges
{	
	NSString *uploadData = [[self getUploadData] retain];
	NSLog(@"upload data: %@", uploadData);
	NSMutableURLRequest *request = [NSMutableURLRequest requestWithURL:self.uploadURL];
	[request setHTTPMethod:@"POST"];
	[request addValue:@"Application/json" forHTTPHeaderField:@"content-type"];
	[request addValue:@"Application/json" forHTTPHeaderField:@"accept"];
	[request setValue:@"gzip" forHTTPHeaderField:@"Accept-Encoding"];  	
	[request setHTTPBody: [uploadData dataUsingEncoding:NSUTF8StringEncoding]];
	
	NSURLResponse *response = nil; 
	NSError *error = nil;
	NSData *data = [NSURLConnection sendSynchronousRequest:request returningResponse:&response error:&error];			
	if (error != nil)
	{
		[Utils showAlert:@"Upload failure" withMessage:[error localizedDescription] withDelegate:delegate];
	}
	else
	{
		NSString *responseString = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
		[self processUploadResponse:responseString];
		[responseString release];
	}
	[uploadData release];
}

// This method returns a JSON string including the list of local changes made by the client
// for the List, Item and TagItemMapping entities
- (NSString *)getUploadData
{
	// Get lists to upload
	NSFetchRequest *fetchRequestList = [[[NSFetchRequest alloc] init] autorelease];
	[fetchRequestList setEntity:[NSEntityDescription entityForName:@"List" inManagedObjectContext:self.managedObjectContext]];
	[fetchRequestList setPredicate:[NSPredicate predicateWithFormat:@"(localUpdate == YES)"]];
	NSArray *listsToUpload = [self.managedObjectContext executeFetchRequest:fetchRequestList error:nil];
	
	
	// Get items to upload
	NSFetchRequest *fetchRequestItem = [[[NSFetchRequest alloc] init] autorelease];
	[fetchRequestItem setEntity:[NSEntityDescription entityForName:@"Item" inManagedObjectContext:self.managedObjectContext]];
	[fetchRequestItem setPredicate:[NSPredicate predicateWithFormat:@"(localUpdate == YES)"]];
	NSArray *itemsToUpload = [self.managedObjectContext executeFetchRequest:fetchRequestItem error:nil];
	
	// Get tagItemMappings to upload
	NSFetchRequest *fetchTagItemMapping = [[[NSFetchRequest alloc] init] autorelease];
	[fetchTagItemMapping setEntity:[NSEntityDescription entityForName:@"TagItemMapping" inManagedObjectContext:self.managedObjectContext]];
	[fetchTagItemMapping setPredicate:[NSPredicate predicateWithFormat:@"(localUpdate == YES)"]];
	NSArray *tagItemMappingToUpload = [self.managedObjectContext executeFetchRequest:fetchTagItemMapping error:nil];		
	
		
	NSMutableArray *items = [[[NSMutableArray alloc] init] autorelease];

	Anchor *anc = [Utils getAnchor:self.managedObjectContext];
	
	for (TagItemMappingEntity *tagItemMapping in tagItemMappingToUpload)
	{
		bool isTombstone = [tagItemMapping.IsTombstone boolValue];
		NSString *uri = [NSString stringWithFormat:@"%@/DefaultScopeSyncService.svc/DefaultScope.TagItemMappings(TagID=%@, ItemID=guid'%@', UserID=guid'%@')", baseURL, [tagItemMapping tagID], [tagItemMapping itemID], [anc userID]];
		NSDictionary *metadata = nil;
		
		if (!isTombstone)
		{
			
			metadata = [NSDictionary dictionaryWithObjects:[NSArray arrayWithObjects:uri, @"DefaultScope.TagItemMapping", nil]
												   forKeys:[NSArray arrayWithObjects:@"uri",@"type", nil]];
		}
		else 
		{
			
			metadata = [NSDictionary dictionaryWithObjects:[NSArray arrayWithObjects:uri, @"DefaultScope.TagItemMapping", [NSNumber numberWithBool:isTombstone], nil]
												   forKeys:[NSArray arrayWithObjects:@"uri",@"type",@"isDeleted", nil]];
			
		}
		NSDictionary *entity = nil;
		NSString *itemID = [NSString stringWithString:[tagItemMapping itemID]];
		if (!isTombstone)
		{
			//ID, ListID, UserID, Name, Description, Priority, Status, StartDate, EndDate
			
			NSString *userIDString = [NSString stringWithString:anc.userID];
			
			entity = [NSDictionary dictionaryWithObjects:[NSArray arrayWithObjects:metadata, itemID, userIDString, tagItemMapping.tagID, nil]
												 forKeys:[NSArray arrayWithObjects:@"__metadata", @"ItemID", @"UserID", @"TagID", nil]];
		}
		else 
		{
			// need to include the primary keys when the item is deleted
			entity = [NSDictionary dictionaryWithObjects:[NSArray arrayWithObjects:metadata, itemID, tagItemMapping.tagID, anc.userID, nil] forKeys:[NSArray arrayWithObjects:@"__metadata", @"ItemID", @"TagID", @"UserID", nil]];
		}
		
		[items addObject:entity];
		
	}
	
	for (ItemEntity *item in itemsToUpload)
	{
		bool isTombstone = [item.IsTombstone boolValue];
		NSString *uri = [NSString stringWithFormat:@"%@/DefaultScopeSyncService.svc/DefaultScope.Items(ID=guid'%@')", baseURL, [item ID]];
		NSDictionary *metadata = nil;
		
		if (!isTombstone)
		{
			
			metadata = [NSDictionary dictionaryWithObjects:[NSArray arrayWithObjects:uri, @"DefaultScope.Item", nil]
												   forKeys:[NSArray arrayWithObjects:@"uri",@"type", nil]];
		}
		else 
		{
			
			metadata = [NSDictionary dictionaryWithObjects:[NSArray arrayWithObjects:uri, @"DefaultScope.Item", [NSNumber numberWithBool:isTombstone], nil]
												   forKeys:[NSArray arrayWithObjects:@"uri",@"type",@"isDeleted", nil]];
			
		}
		NSDictionary *entity = nil;
		NSString *idString = [NSString stringWithString:[item ID]];
		if (!isTombstone)
		{
			//ID, ListID, UserID, Name, Description, Priority, Status, StartDate, EndDate
			
			NSString *listIDString = [NSString stringWithString:item.listID];
			NSString *userIDString = [NSString stringWithString:anc.userID];
			NSString *nameString = [NSString stringWithString:item.itemName];
			NSString *descriptionString = [NSString stringWithString:item.itemDescription];
			id priority = item.priority;
			if ([priority intValue] == -1)
			{
				priority = [NSNull null];
			}
			
			id status = item.status;
			if ([status intValue] == -1)
			{
				status = [NSNull null];
			}
			
			id startDate = [Utils dateToString:item.startDate];
			id endDate = [Utils dateToString:item.endDate];
			
			entity = [NSDictionary dictionaryWithObjects:[NSArray arrayWithObjects:metadata, idString,listIDString, userIDString, nameString, descriptionString,priority , status, 
														  startDate, endDate, nil]
												 forKeys:[NSArray arrayWithObjects:@"__metadata", @"ID", @"ListID", @"UserID", @"Name", @"Description", @"Priority", @"Status",
														  @"StartDate", @"EndDate", nil]];
		}
		else 
		{
						// need to include the primary keys when the item is deleted
			entity = [NSDictionary dictionaryWithObjects:[NSArray arrayWithObjects:metadata, idString, nil] forKeys:[NSArray arrayWithObjects:@"__metadata", @"ID", nil]];
		}
		
		[items addObject:entity];
		
	}
	
	for (ListEntity *list in listsToUpload)
	{
		
		bool isTombstone = [[list primitiveValueForKey:@"IsTombstone"] boolValue];
		NSString *uri = [NSString stringWithFormat:@"%@/DefaultScopeSyncService.svc/DefaultScope.Lists(ID=guid'%@')", baseURL, [list ID]];
		NSDictionary *metadata = nil;
		
		if (!isTombstone)
		{
			metadata = [NSDictionary dictionaryWithObjects:[NSArray arrayWithObjects:uri, @"DefaultScope.List", nil]
												   forKeys:[NSArray arrayWithObjects:@"uri",@"type", nil]];
		}
		else 
		{
			metadata = [NSDictionary dictionaryWithObjects:[NSArray arrayWithObjects:uri, @"DefaultScope.List", [NSNumber numberWithBool:isTombstone], nil]
												   forKeys:[NSArray arrayWithObjects:@"uri",@"type", @"isDeleted", nil]];			
		}
		
		NSDictionary *entity = nil;
		NSString *idString = [NSString stringWithString:[list ID]];
		if (!isTombstone)
		{
			// ID, Name, Description, UserID, CreatedDate
			NSString *nameString = [NSString stringWithString:[list listName]];
			NSString *descriptionString = [NSString stringWithString:[list listDescription]];
			NSString *userIDString = [NSString stringWithString:[anc userID]];
			NSString *dateString = [Utils dateTimeToString:[list createDate]];
			
			entity = [NSDictionary dictionaryWithObjects:[NSArray arrayWithObjects:metadata, idString, nameString, descriptionString, userIDString, dateString, nil]
												 forKeys:[NSArray arrayWithObjects:@"__metadata", @"ID", @"Name", @"Description", @"UserID", @"CreatedDate", nil]];
		}
		else 
		{
			// need to include the primary keys when the item is deleted
			entity = [NSDictionary dictionaryWithObjects:[NSArray arrayWithObjects:metadata, idString, nil] forKeys:[NSArray arrayWithObjects:@"__metadata",@"ID", nil]];
		}
		
		[items addObject:entity];
	}
	
	NSString *string =[Utils GetODataJsonPayload:anc.syncBlob withChanges:items];
	NSLog(@"upload string: %@", string);
	return string;
}

// Processes the response from the UploadChanges request sent to the sync service
// if the UploadChanges succeeded the response will contain
// a JSON string with the new serverBlob that will be used for future syncs,
// plus it will contain any syncConflict or syncError that result from the changes uploaded.
// If the UploadChanges did not succeed for any other reason, the responseString will contain
// an error sent from the syncService with the description for the error (in case the error has
//  verbose logging enabled)
-(void) processUploadResponse:(NSString *) responseString
{
	// Verify if the response is a valid Json string
	NSError *error = nil;
	SBJSON *json = [[[SBJSON alloc] init] autorelease];
	[json objectWithString:responseString error:&error];
	
	// If the string is a valid json (ie. there was no error processing the UploadChanges request
	// we need to: (1) set the localUpdate field to NO for the uploaded items;
	//             (2) save the serverBlob
	//             (3) update store with list of solved syncConflicts
	if (error == nil)
	{
		// update items modified
		NSFetchRequest *fetchRequestList = [[[NSFetchRequest alloc] init] autorelease];
		[fetchRequestList setEntity:[NSEntityDescription entityForName:@"List" inManagedObjectContext:self.managedObjectContext]];
		[fetchRequestList setPredicate:[NSPredicate predicateWithFormat:@"(localUpdate == YES)"]];
		NSArray *listsUploaded = [self.managedObjectContext executeFetchRequest:fetchRequestList error:nil];
		
		
		// Get items to upload
		NSFetchRequest *fetchRequestItem = [[[NSFetchRequest alloc] init] autorelease];
		[fetchRequestItem setEntity:[NSEntityDescription entityForName:@"Item" inManagedObjectContext:self.managedObjectContext]];
		[fetchRequestItem setPredicate:[NSPredicate predicateWithFormat:@"(localUpdate == YES)"]];
		NSArray *itemsUploaded = [self.managedObjectContext executeFetchRequest:fetchRequestItem error:nil];		
		
		
		// Get tagItemMappings to upload
		NSFetchRequest *fetchTagItemMapping = [[[NSFetchRequest alloc] init] autorelease];
		[fetchTagItemMapping setEntity:[NSEntityDescription entityForName:@"TagItemMapping" inManagedObjectContext:self.managedObjectContext]];
		[fetchTagItemMapping setPredicate:[NSPredicate predicateWithFormat:@"(localUpdate == YES)"]];
		NSArray *tagItemMappingsUploaded = [self.managedObjectContext executeFetchRequest:fetchTagItemMapping error:nil];		
		
		for (ItemEntity *item in itemsUploaded)
		{			
			// update item in local cache
			if ([item.IsTombstone boolValue])
			{
				[self.managedObjectContext deleteObject:item];
			}
			else 
			{
				item.localUpdate = [NSNumber numberWithBool:NO];
				[item logItem];
			}			
		}
		
		for (ListEntity *list in listsUploaded)
		{			
			// update item in local cache
			if ([list.IsTombstone boolValue])
			{
				[self.managedObjectContext deleteObject:list];
			}
			else 
			{
				list.localUpdate = [NSNumber numberWithBool:NO];
				[list logList];
			}		
		}
		
		for (TagItemMappingEntity *tim in tagItemMappingsUploaded)
		{			
			// update item in local cache
			if ([tim.IsTombstone boolValue])
			{
				[self.managedObjectContext deleteObject:tim];
			}
			else 
			{
				tim.localUpdate = [NSNumber numberWithBool:NO];
			}		
		}
				
		Anchor *anc = [Utils getAnchor:managedObjectContext];
		
		// process json response string (update anchor, and process any resolved syncConflicts
		[Utils ProcessODataJsonChanges:responseString withAnchor:&anc withContext:self.managedObjectContext];
		
		[Utils saveManagedObjects:self.managedObjectContext];
		[self uploadDone];
	}
	else 
	{
		[Utils showAlert:@"Failed to upload changes" withMessage:@"Received error response from uploadChanges request.  View log for details" withDelegate:delegate];
		NSLog(@"Failed to upload changes %@ %@",error, [error userInfo]);
		NSLog(@"Got response %@", responseString);
	}
	
	[error release];
}





@end
