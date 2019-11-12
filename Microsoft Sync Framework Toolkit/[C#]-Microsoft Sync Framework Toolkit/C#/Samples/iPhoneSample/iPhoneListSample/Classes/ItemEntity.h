//
//  Item.h
//  iPhoneListSample
//
//  Copyright 2010 Microsoft. All rights reserved.
//

#import <CoreData/CoreData.h>


@interface ItemEntity : NSManagedObject {

}

@property (nonatomic, retain) NSDate * endDate;
@property (nonatomic, retain) NSString * ID;
@property (nonatomic, retain) NSNumber * IsTombstone;
@property (nonatomic, retain) NSString * itemDescription;
@property (nonatomic, retain) NSString * itemName;
@property (nonatomic, retain) NSString * listID;
@property (nonatomic, retain) NSNumber * localUpdate;
@property (nonatomic, retain) NSNumber * priority;
@property (nonatomic, retain) NSDate * startDate;
@property (nonatomic, retain) NSNumber * status;

-(void) logItem;
@end


