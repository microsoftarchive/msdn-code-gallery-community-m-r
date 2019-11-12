//
//  Status.h
//  iPhoneListSample
//
//  Copyright 2010 Microsoft. All rights reserved.
//

#import <CoreData/CoreData.h>


@interface StatusEntity : NSManagedObject {

}
@property (nonatomic, retain) NSNumber * ID;
@property (nonatomic, retain) NSString * statusName;
@end
