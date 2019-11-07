//
//  ListEntity.h
//  iPhoneListSample
//
//  Copyright 2010 Microsoft. All rights reserved.
//


#import <CoreData/CoreData.h>


@interface ListEntity : NSManagedObject {
	
}

@property (nonatomic, retain) NSString * ID;
@property (nonatomic, retain) NSString * listName;
@property (nonatomic, retain) NSString * listDescription;
@property (nonatomic, retain) NSDate *createDate;
@property (nonatomic, retain) NSNumber *IsTombstone;
@property (nonatomic, retain) NSNumber *localUpdate;

-(void) logList;
@end
