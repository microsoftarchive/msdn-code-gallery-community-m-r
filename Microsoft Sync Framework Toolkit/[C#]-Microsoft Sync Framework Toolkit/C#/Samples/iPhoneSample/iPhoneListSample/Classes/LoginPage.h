//
//  LoginPage.h
//  iPhoneListSample
//
//  Copyright 2010 Microsoft. All rights reserved.
//

#import <UIKit/UIKit.h>


@interface LoginPage : UIViewController {
	IBOutlet UITextField *userName; 
	NSMutableData *responseData;
}

@property (nonatomic, retain) IBOutlet UITextField *userName;
@property (nonatomic, retain) NSMutableData *responseData;

-(IBAction)loginAction:(id)sender;
-(IBAction)dismissKeyboard: (id)sender ;
-(void) postRequestForLogin:(NSString*) user;
-(void) loginDone:(id) sender;
@end
