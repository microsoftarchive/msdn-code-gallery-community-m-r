//
//  TagEntity.h
//  iPhoneListSample
//
//  Copyright 2010 Microsoft. All rights reserved.
//

#import <CoreData/CoreData.h>


@interface TagEntity :  NSManagedObject  
{
}

@property (nonatomic, retain) NSNumber * ID;
@property (nonatomic, retain) NSString * tagName;

@end



