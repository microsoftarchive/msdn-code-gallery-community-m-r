//
//  Anchor.h
//  iPhoneListSample
//
//  Copyright 2010 Microsoft. All rights reserved.
//

#import <CoreData/CoreData.h>


@interface Anchor :  NSManagedObject  
{
}

@property (nonatomic, retain) NSString * syncBlob;
@property (nonatomic, retain) NSString * userID;
@property (nonatomic, retain) NSNumber * hasSynced;
@property (nonatomic, retain) NSString * userName;
@end



