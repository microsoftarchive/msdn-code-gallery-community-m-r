//
//  TagItemMappingEntity.h
//  iPhoneListSample
//
//  Copyright 2010 Microsoft. All rights reserved.
//

#import <CoreData/CoreData.h>


@interface TagItemMappingEntity :  NSManagedObject  
{
}

@property (nonatomic, retain) NSNumber * tagID;
@property (nonatomic, retain) NSString * itemID;
@property (nonatomic, retain) NSNumber * IsTombstone;
@property (nonatomic, retain) NSNumber * localUpdate;

@end



