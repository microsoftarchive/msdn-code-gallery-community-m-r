//
//  ListEntity.m
//  iPhoneListSample
//
//  Copyright 2010 Microsoft. All rights reserved.
//

#import "ListEntity.h"


@implementation ListEntity


@dynamic ID;
@dynamic listName;
@dynamic listDescription;
@dynamic IsTombstone;
@dynamic localUpdate;
@dynamic createDate;

-(void) logList
{
	NSLog(@"Item ID: %@", self.ID);
	NSLog(@"Item name: %@", self.listName);
	NSLog(@"Item isTombstone: %@", self.IsTombstone);
	NSLog(@"Item descrition: %@", self.listDescription);
	NSLog(@"Item localUpdate: %@", self.localUpdate);
	NSLog(@"Item startDate: %@", self.createDate);
	
}
@end
