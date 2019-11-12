//
//  LoginPage.m
//  iPhoneListSample
//
//  Copyright 2010 Microsoft. All rights reserved.
//

#import "LoginPage.h"
#import "Anchor.h"
#import "iPhoneListSampleAppDelegate.h"
#import "Utils.h"
#import "ListViewController.h"

@implementation LoginPage
@synthesize userName, responseData;



#pragma mark -
#pragma mark View flow
// Implement viewDidLoad to do additional setup after loading the view, typically from a nib.
- (void)viewDidLoad {
    [super viewDidLoad];
	
}



#pragma mark -
#pragma mark delegate for userName text field
-(IBAction)dismissKeyboard: (id)sender {
	[sender resignFirstResponder];
}

#pragma mark -
#pragma mark action delegate for login button
- (IBAction) loginAction: (id) sender
{
	if ([[NSString stringWithFormat:@""] isEqualToString:userName.text])
	{
		[Utils showAlert:@"Username" withMessage:@"Please enter username" withDelegate:self];
	}
	else 
	{
		// if user has logged in already, and userName is the same do not post login request
		NSManagedObjectContext *managedObjectContext = [(iPhoneListSampleAppDelegate*)[[UIApplication sharedApplication] delegate] managedObjectContext];
		Anchor *anc = [Utils getAnchor:managedObjectContext];
		if (anc != nil && [anc.userName isEqualToString:userName.text])
		{
			[self loginDone:[NSNumber numberWithBool:YES]];
		}
		else 
		{		
			[self postRequestForLogin:[NSString stringWithFormat:@"%@",userName.text]];
		}
	}
	
	
}

#pragma mark -
#pragma mark login flow
-(void) postRequestForLogin:(NSString*) user
{
	NSString *filePath = [[NSBundle mainBundle] pathForResource:@"Info" ofType:@"plist"];  
	NSMutableDictionary* plistDict = [[[NSMutableDictionary alloc] initWithContentsOfFile:filePath] autorelease];
	NSString *urlString = [NSString stringWithString:[plistDict valueForKey:@"ServiceRoot"]];
						   	
	NSURL *url = [NSURL URLWithString:[NSString stringWithFormat:@"%@/login.ashx/?username=%@",urlString, user]];
	responseData = [[NSMutableData data] retain];
	NSMutableURLRequest *request = [NSMutableURLRequest requestWithURL:url];
	[request setHTTPMethod:@"GET"];
	[request addValue:@"text/plain" forHTTPHeaderField:@"accept"];
	[[NSURLConnection alloc] initWithRequest:request delegate:self];	
}


// if login was a success show the next view with list of lists
- (void) loginDone:(id) success
{
	if ([success boolValue])
	{
		ListViewController *listViewController = [[ListViewController alloc] init];
		[self.navigationController pushViewController:listViewController animated:YES];
		[listViewController release];
	}
	else 
	{
		[Utils showAlert:@"Login" withMessage:@"Login failed" withDelegate:self];
	}	
}


#pragma mark -
#pragma mark NSURLConnection delegates

- (void)connection:(NSURLConnection *)connection didReceiveResponse:(NSURLResponse *)response {
	[responseData setLength:0];
}

- (void)connection:(NSURLConnection *)connection didReceiveData:(NSData *)data {
	[responseData appendData:data];
}

- (void)connection:(NSURLConnection *)connection didFailWithError:(NSError *)error {
	NSLog(@"Connection failed: %@", [error description]);
	[Utils showAlert:@"Connection Error" withMessage:[error localizedDescription] withDelegate:self];
	[connection release];

	[self loginDone:[NSNumber numberWithBool:NO]];
}


- (void)connectionDidFinishLoading:(NSURLConnection *)connection {
	[connection release];
	
	NSString *responseString = [[NSString alloc] initWithData:responseData encoding:NSUTF8StringEncoding];
	
    NSLog(@"response string: %@", responseString);
	
	NSManagedObjectContext *managedObjectContext = [(iPhoneListSampleAppDelegate*)[[UIApplication sharedApplication] delegate] managedObjectContext];
	NSFetchRequest * fetch = [[[NSFetchRequest alloc] init] autorelease];
	[fetch setEntity:[NSEntityDescription entityForName:@"Anchor" inManagedObjectContext:managedObjectContext]];
	NSArray * result = [managedObjectContext executeFetchRequest:fetch error:nil];
	Anchor *anc = nil;
	if ([result count] == 0) 
	{
		NSLog(@"new SyncBlob");
		anc = [NSEntityDescription insertNewObjectForEntityForName:@"Anchor" inManagedObjectContext:managedObjectContext];
		anc.syncBlob = @"";
		anc.userID = [NSString stringWithString:responseString];
		anc.userName = [NSString stringWithString:userName.text];
		anc.hasSynced = [NSNumber numberWithBool:NO];
		
		[Utils saveManagedObjects:managedObjectContext];
	}
	else if ([result count] == 1)
	{
		anc = [result objectAtIndex:0];
		if (![anc.userID isEqualToString:responseString])
		{
			[Utils cleanupCache:managedObjectContext];
			anc = [NSEntityDescription insertNewObjectForEntityForName:@"Anchor" inManagedObjectContext:managedObjectContext];
			// user switch, need to switch the sync blob
			
			NSLog(@"user switch with new syncBlob");
			anc.userID = [NSString stringWithString:responseString];
			anc.syncBlob = @"";
			anc.hasSynced = [NSNumber numberWithBool:NO];
			anc.userName = [NSString stringWithString:userName.text];
			[Utils saveManagedObjects:managedObjectContext];
		}
		else 
		{
			NSLog(@"old syncBlob");
			NSLog(@"userID %@", anc.userID);
			NSLog(@"syncBlob %@", anc.syncBlob);
		}
		
	}	
	NSLog(@"%@", [anc primitiveValueForKey:@"hasSynced"]);
	[responseString release];
	
	[self loginDone:[NSNumber numberWithBool:YES]];
}


#pragma mark -
#pragma mark Memory management

- (void)didReceiveMemoryWarning {
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Release any cached data, images, etc that aren't in use.
}

- (void)viewDidUnload {
    [super viewDidUnload];
    // Release any retained subviews of the main view.
    // e.g. self.myOutlet = nil;
	self.userName = nil;
}

- (void)dealloc {
    [super dealloc];
	[userName release];
	[responseData release];
	
}


@end
