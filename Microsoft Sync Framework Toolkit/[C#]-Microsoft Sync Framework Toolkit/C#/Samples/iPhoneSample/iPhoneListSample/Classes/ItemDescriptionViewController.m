//
//  ItemDetailViewController.m
//  iPhoneListSample
//
//  Copyright 2010 Microsoft. All rights reserved.
//

#import "ItemDescriptionViewController.h"
#import "iPhoneListSampleAppDelegate.h"
#import "Utils.h"

@implementation ItemDescriptionViewController

@synthesize inAddMode, nameTextField, descriptionTextField, item, list;
@synthesize managedObjectContext;
@synthesize priorityStatusPicker, priorityList, statusList, selectedPriority, selectedStatus;

#pragma mark 
#pragma mark DataSource for Priority and Status pickerView
-(NSArray *)priorityList
{

	NSFetchRequest *fetch = [[[NSFetchRequest alloc] init] autorelease];
	[fetch setEntity:[NSEntityDescription entityForName:@"Priority" inManagedObjectContext:self.managedObjectContext]];
	
	// Edit the sort key as appropriate.
    NSSortDescriptor *sortDescriptor = [[[NSSortDescriptor alloc] initWithKey:@"ID" ascending:YES] autorelease];
    NSArray *sortDescriptors = [[[NSArray alloc] initWithObjects:sortDescriptor, nil] autorelease];
    
    [fetch setSortDescriptors:sortDescriptors];
	
	priorityList = [self.managedObjectContext executeFetchRequest:fetch error:nil];	
	return priorityList;
}

-(NSArray *)statusList
{
	NSFetchRequest *fetch = [[[NSFetchRequest alloc] init] autorelease];
	[fetch setEntity:[NSEntityDescription entityForName:@"Status" inManagedObjectContext:self.managedObjectContext]];
	// Edit the sort key as appropriate.
    NSSortDescriptor *sortDescriptor = [[[NSSortDescriptor alloc] initWithKey:@"ID" ascending:YES] autorelease];
    NSArray *sortDescriptors = [[[NSArray alloc] initWithObjects:sortDescriptor, nil] autorelease];
    
    [fetch setSortDescriptors:sortDescriptors];
	
	statusList = [self.managedObjectContext executeFetchRequest:fetch error:nil];	
	return statusList;	
}

- (NSInteger) indexOfPriority:(NSNumber*)priorityID
{
	NSArray *plist = self.priorityList;
	int row = -1;
	if (priorityID != nil)
	{
		for (int i = 0 ;i < plist.count; i++)
		{
			PriorityEntity* p = [plist objectAtIndex:i];
			if ([p.ID  intValue] == [priorityID intValue])
			{
				row = i;
			}
		}
	}
	
	return row + 1;
}

-(NSInteger) indexOfStatus:(NSNumber*)statusID
{
	NSArray *slist = self.statusList;
	int row = -1;
	if (statusID != nil)
	{
		for (int i = 0 ;i <slist.count; i++)
		{
			StatusEntity* s = [slist objectAtIndex:i];
			if ([s.ID intValue] == [statusID intValue])
			{
				row = i;
			}
		}
	}
	
	return row + 1;
	
}

#pragma mark 
#pragma mark view flow
- (void)viewDidLoad {
	
	[super viewDidLoad];
	[self.navigationItem setHidesBackButton:YES];

	
	if (inAddMode)
	{
		self.navigationItem.title = @"New Item";
		nameTextField.text = nil;
		descriptionTextField.text = nil;
		selectedStatus = -1;
		selectedPriority = -1;
	}
	else 
	{
		self.navigationItem.title = @"Item Edit";
		selectedPriority = [item.priority intValue];
		selectedStatus = [item.status intValue];
		nameTextField.text = [NSString stringWithString:item.itemName];
		descriptionTextField.text = [NSString stringWithString:item.itemDescription];
	}

	[priorityStatusPicker selectRow:[self indexOfStatus:[NSNumber numberWithInt:selectedStatus]] inComponent:1 animated:YES];
	[priorityStatusPicker selectRow:[self indexOfPriority:[NSNumber numberWithInt:selectedPriority]] inComponent:0 animated:YES];
	
	UIBarButtonItem *saveButton = [[UIBarButtonItem alloc] initWithBarButtonSystemItem:UIBarButtonSystemItemSave
																			   target:self action:@selector(saveAction:)];

	UIBarButtonItem *cancelButton = [[UIBarButtonItem alloc] initWithBarButtonSystemItem:UIBarButtonSystemItemCancel
																			   target:self action:@selector(cancelAction:)];

	self.navigationItem.leftBarButtonItem = cancelButton;
	self.navigationItem.rightBarButtonItem = saveButton;
	

	[saveButton release];
	[cancelButton release];
	 
}

-(NSManagedObjectContext *)managedObjectContext
{
	if ( managedObjectContext != nil)
	{
		return managedObjectContext;
	}
	
	[self setManagedObjectContext:[(iPhoneListSampleAppDelegate *)[[UIApplication sharedApplication] delegate] managedObjectContext]];	
	
	return managedObjectContext;
	
}


#pragma mark 
#pragma mark UITextField delegate for  nameTextField
-(IBAction)dismissKeyboard: (id)sender {
	[sender resignFirstResponder];
}

#pragma mark 
#pragma mark UITextView delegate to release keyboard for descriptionTextField
- (void)textViewDidEndEditing:(UITextView *)textView
{
	[textView resignFirstResponder];
}

- (BOOL)textViewShouldEndEditing:(UITextView *)textView
{
	return YES;
}

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


#pragma mark 
#pragma mark save button action

-(void) saveAction:(id) sender
{
	if (nameTextField.text == nil || [nameTextField.text isEqualToString:@""])
	{
		// please enter name
		[Utils showAlert:@"Missing Item Name" withMessage:@"Enter item name before saving" withDelegate:self];
	}
	else if (self.selectedPriority == -1)
	{
		[Utils showAlert:@"Missing Priority Selection" withMessage:@"Select a priority before saving." withDelegate:self];
	}
	else if (self.selectedStatus == -1)
	{
		[Utils showAlert:@"Missing Status Selection" withMessage:@"Select status before saving." withDelegate:self];
	}	
	else 
	{
		if (inAddMode)
		{
			ItemEntity *newItem = [NSEntityDescription insertNewObjectForEntityForName:@"Item" inManagedObjectContext:self.managedObjectContext];
			newItem.localUpdate = [NSNumber numberWithBool:YES];
			newItem.IsTombstone = [NSNumber numberWithBool:NO];
			newItem.itemName = [NSString stringWithString:nameTextField.text];
			if (descriptionTextField.text == nil)
			{
				newItem.itemDescription = @"";
			}
			else 
			{
				newItem.itemDescription = [NSString stringWithString:descriptionTextField.text];
			}
			newItem.ID = [Utils GetUUID];
			newItem.priority = [NSNumber numberWithInt:self.selectedPriority]; 
			newItem.status = [NSNumber numberWithInt:self.selectedStatus];
			newItem.listID = [NSString stringWithString:list.ID]; 

		}
		else 
		{
			item.localUpdate = [NSNumber numberWithBool:YES];
			item.IsTombstone = [NSNumber numberWithBool:NO];
			item.itemName = nameTextField.text;
			if (descriptionTextField.text == nil)
			{
				item.itemDescription = @"";
			}
			else 
			{
				item.itemDescription = [NSString stringWithString:descriptionTextField.text];
			}	
			item.priority = [NSNumber numberWithInt:self.selectedPriority]; 
			item.status = [NSNumber numberWithInt:self.selectedStatus];
		}
		[Utils saveManagedObjects:self.managedObjectContext];			

		[self.navigationController popViewControllerAnimated:YES];
	
	}
}

#pragma mark -
#pragma mark cancel button action
-(void) cancelAction:(id) sender
{
	[self.navigationController popViewControllerAnimated:YES];
}

#pragma mark -
#pragma mark UIPickerView DataSource
- (NSInteger)numberOfComponentsInPickerView:(UIPickerView *)pickerView
{
	return 2;
}

- (NSInteger)pickerView:(UIPickerView *)pickerView numberOfRowsInComponent:(NSInteger)component
{
	
	NSArray *items = nil;
	if (component == 0)
	{
		items = self.priorityList;
	}
	else 
	{
		items = self.statusList;
	}
	
	return items.count + 1;	
}


#pragma mark -
#pragma mark UIPickerView delegate

- (void)pickerView:(UIPickerView *)pickerView didSelectRow:(NSInteger)row inComponent:(NSInteger)component
{
	if (row == 0)
	{
		if (component == 0)
		{
			selectedPriority = -1;
		}
		else 
		{
			selectedStatus = -1;
		}
	}
	else 
	{
		if (component == 0)
		{
			selectedPriority = [[[self.priorityList objectAtIndex:(row - 1)] primitiveValueForKey:@"ID"] intValue];
		}
		else 
		{
			selectedStatus = [[[self.statusList objectAtIndex:(row -1)] primitiveValueForKey:@"ID"] intValue];
		}
		
	}
}


- (NSString *)pickerView:(UIPickerView *)pickerView titleForRow:(NSInteger)row forComponent:(NSInteger)component
{
	NSString *name = nil;
	if (row == 0)
	{
		name = @"";
	}
	else 
	{
		if (component == 0) // priority 
		{
			name = [NSString stringWithString:[[self.priorityList objectAtIndex:(row - 1)] priorityName]];
		}
		else 
		{
			name = [NSString stringWithString:[[self.statusList objectAtIndex:(row - 1)] statusName]];		
		}
	}	

	return name;
}


#pragma mark -
#pragma mark Memory management
- (void)didReceiveMemoryWarning {
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Release any cached data, images, etc that aren't in use.
}

- (void)viewDidUnload {
    // Release any retained subviews of the main view.
    // e.g. self.myOutlet = nil;

    [super viewDidUnload];
	self.nameTextField = nil;
	self.descriptionTextField = nil;
	self.priorityStatusPicker = nil;
}


- (void)dealloc {
    [super dealloc];
	[managedObjectContext release];
	[nameTextField release];
	[descriptionTextField release];
	[priorityStatusPicker release];
}


@end
