//
//  ItemDetailViewController.h
//  iPhoneListSample
//
//  Copyright 2010 Microsoft. All rights reserved.
//

#import "StatusEntity.h"
#import "PriorityEntity.h"
#import "ItemEntity.h"
#import "ListEntity.h"
#import "Anchor.h"
#import "TagEntity.h"
#import "TagItemMappingEntity.h"

@interface Utils : NSObject {

}

+ (void)saveManagedObjects:(NSManagedObjectContext *)context;

+ (NSDate *)parseDateTime:(id)str;
+ (id) dateTimeToString:(NSDate*)date;
+ (NSDate *)parseDate:(id)str;
+ (id) dateToString:(NSDate*)date;

+ (void)populateTag:(id)dict withMetadata:(id)metadata withContext:(NSManagedObjectContext*)context;
+ (void)populateStatus:(id)dict withMetadata:(id)metadata withContext:(NSManagedObjectContext*)context;
+ (void)populatePriority:(id)dict withMetadata:(id)metadata withContext:(NSManagedObjectContext*)context;
+ (void)populateList:(id)dict withMetadata:(id)metadata withContext:(NSManagedObjectContext*)context;
+ (void)populateItem:(id)dict withMetadata:(id)metadata withContext:(NSManagedObjectContext*)context;
+ (void)populateTagItemMapping:(id)dict withMetadata:(id)metadata withContext:(NSManagedObjectContext*)context;

+ (bool) isDeleted:(id)dict;
+ (NSString *) GetUUID;

+ (Anchor*) getAnchor:(NSManagedObjectContext*)context;
+ (void) cleanupCache:(NSManagedObjectContext*) context;
+ (bool) hasChanges:(NSManagedObjectContext*) context;
+ (bool) clientHasSynced: (NSManagedObjectContext*) context;
+ (void) showAlert:(NSString*)title withMessage:(NSString*)message withDelegate:(id)delegate;

+ (void) deleteList:(ListEntity*)list inContext:(NSManagedObjectContext*)context;
+ (void) deleteItem:(ItemEntity*)item inContext:(NSManagedObjectContext*)context;
+ (void) deleteTagItemMapping:(TagItemMappingEntity*)tagItemMapping inContext:(NSManagedObjectContext*)context;

+(NSString *) GetODataJsonPayload:(NSString*)serverBlob withChanges:(NSMutableArray*)changes;
+(bool) ProcessODataJsonChanges:(NSString*)jsonChanges withAnchor:(Anchor**)anc withContext:(NSManagedObjectContext *)context;

@end
