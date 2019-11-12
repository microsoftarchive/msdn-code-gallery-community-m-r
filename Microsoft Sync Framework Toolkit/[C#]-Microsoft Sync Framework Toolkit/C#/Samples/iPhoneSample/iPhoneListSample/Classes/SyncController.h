//
//  SyncController.h
//  iPhoneListSample
//
//  Copyright 2010 Microsoft. All rights reserved.
//

#import <Foundation/Foundation.h>


@interface SyncController : NSObject 
{
	NSManagedObjectContext *managedObjectContext;
	id delegate;
	NSString *baseURL;
	NSURL *downloadURL;
	NSURL *uploadURL;
}

@property (nonatomic, retain) NSManagedObjectContext *managedObjectContext;
@property (nonatomic, assign) id delegate;
@property (nonatomic, retain) NSString *baseURL;
@property (nonatomic, retain) NSURL *downloadURL;
@property (nonatomic, retain) NSURL *uploadURL;

- (id)initWithContext:(NSManagedObjectContext *)inContext delegate:(id)myDelegate;
- (void)processDownloadResponse:(NSString *) responseString;
-(void) downloadChanges;
-(void) uploadChanges;
- (NSString *)getUploadData;
-(void) processUploadResponse:(NSString *) responseString;
-(void) synchronize;
@end


