//
//  ListDescriptionViewController.m
//  iPhoneListSample
//
//  Copyright 2010 Microsoft. All rights reserved.
//

#import "ListDescriptionViewController.h"
#import "iPhoneListSampleAppDelegate.h"
#import "Utils.h"
#import "Anchor.h"

@implementation ListDescriptionViewController
@synthesize inAddMode, listToEdit, descriptionField, nameField;



#pragma mark -
#pragma mark View lifecycle
- (void)viewDidLoad {
    [super viewDidLoad];
	
	[self.navigationItem setHidesBackButton:YES];
	UIBarButtonItem *saveButton = [[UIBarButtonItem alloc] initWithBarButtonSystemItem:UIBarButtonSystemItemSave
																				target:self action:@selector(saveAction:)];
	
	UIBarButtonItem *cancelButton = [[UIBarButtonItem alloc] initWithBarButtonSystemItem:UIBarButtonSystemItemCancel
																				  target:self action:@selector(cancelAction:)];
	
	self.toolbarItems = [NSArray arrayWithObjects:saveButton, cancelButton, nil]; 
	
	if (inAddMode)
	{
		self.navigationItem.title = @"New List";
		descriptionField.text = nil;
		nameField.text = nil;
	}
	else 
	{	
		self.navigationItem.title = @"List Edit";
		nameField.text = [NSString stringWithString:[listToEdit listName]];
		descriptionField.text = [NSString stringWithString:[listToEdit listDescription]];
	}

	[saveButton release];
	[cancelButton release];
}

#pragma mark -
#pragma mark nameField delegate
-(IBAction)dismissKeyboard: (id)sender {
	[sender resignFirstResponder];
}

#pragma mark -
#pragma mark descriptionField delegate 
- (void)textViewDidEndEditing:(UITextView *)textView
{
	[textView resignFirstResponder];
}

// make keyboard hide after enter (done) is pressed
- (BOOL)textView:(UITextView *)textView shouldChangeTextInRange:(NSRange)range 
 replacementText:(NSString *)text
{
    // Any new character added is passed in as the "text" parameter
    if ([text isEqualToString:@"\n"]) {
        // Be sure to test for equality using the "isEqualToString" message
        [textView resignFirstResponder];
		
        // Return FALSE so that the final '\n' character doesn't get added
        return FALSE;
    }
    // For any other character return TRUE so that the text gets added to the view
    return TRUE;
}

#pragma mark -
#pragma mark saveButton action

-(void)saveAction:sender 
{
	if (nameField.text != nil && ![[NSString stringWithFormat:@""] isEqualToString:nameField.text])
	{	
		NSManagedObjectContext *context = [(iPhoneListSampleAppDelegate*)[[UIApplication sharedApplication] delegate] managedObjectContext];

		if (inAddMode)
		{						
			ListEntity *newList = [NSEntityDescription insertNewObjectForEntityForName:@"List" inManagedObjectContext:context];
			newList.localUpdate = [NSNumber numberWithBool:YES];
			newList.IsTombstone = [NSNumber numberWithBool:NO];
			newList.listName = [NSString stringWithString:nameField.text];

			if (descriptionField.text == nil)
			{
				newList.listDescription = @"";
			}
			else 
			{
				newList.listDescription = [NSString stringWithString:descriptionField.text];
			}

			newList.ID = [Utils GetUUID];
			newList.createDate = [NSDate date];			
		}
		else 
		{	listToEdit.localUpdate = [NSNumber numberWithBool:YES];
			listToEdit.listName = [NSString stringWithString:nameField.text];
			
			if (descriptionField.text == nil)
			{
				listToEdit.listDescription = @"";
			}
			else 
			{
				listToEdit.listDescription = [NSString stringWithString:descriptionField.text];
			}			
		}
		[Utils saveManagedObjects:context];
		[self.navigationController popViewControllerAnimated:YES];
	}
	else 
	{
		[Utils showAlert:@"Missing List Name" withMessage:@"Enter a list name before saving." withDelegate:self];		
	}

	
}

#pragma mark -
#pragma mark cancelButton action
-(void)cancelAction:sender
{
	[self.navigationController popViewControllerAnimated:YES];
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
    self.descriptionField = nil;
	self.nameField = nil;
	listToEdit = nil;
}

- (void)dealloc {
    [super dealloc];
	[descriptionField release];
	[nameField release];
}


@end
