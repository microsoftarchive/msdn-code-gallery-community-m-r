//
//  Item.m
//  iPhoneListSample
//
//  Copyright 2010 Microsoft. All rights reserved.
//

#import "ItemEntity.h"
#import "Utils.h"

@implementation ItemEntity

@dynamic endDate;
@dynamic ID;
@dynamic IsTombstone;
@dynamic itemDescription;
@dynamic itemName;
@dynamic listID;
@dynamic localUpdate;
@dynamic priority;
@dynamic startDate;
@dynamic status;

-(void) logItem
{
	NSLog(@"Item ID: %@", self.ID);
	NSLog(@"Item name: %@", self.itemName);
	NSLog(@"Item endDate: %@", self.endDate);
	NSLog(@"Item isTombstone: %@", self.IsTombstone);
	NSLog(@"Item descrition: %@", self.itemDescription);
	NSLog(@"Item listID: %@", self.listID);
	NSLog(@"Item localUpdate: %@", self.localUpdate);
	NSLog(@"Item priority: %@", self.priority);
	NSLog(@"Item startDate: %@", self.startDate);
	NSLog(@"Item status: %@", self.status);
	
}
@end





