//
//  ItemDetailViewController.h
//  iPhoneListSample
//
//  Copyright 2010 Microsoft. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "ItemEntity.h"
#import "ListEntity.h"


@interface ItemDescriptionViewController : UIViewController <UIPickerViewDelegate, UIPickerViewDataSource>
{
	bool inAddMode;
	IBOutlet UITextField *nameTextField;
	IBOutlet UITextView *descriptionTextField;
	IBOutlet UIPickerView *priorityStatusPicker;
	ItemEntity *item;
	ListEntity *list;
	NSManagedObjectContext *managedObjectContext;
	int selectedPriority;
	int selectedStatus;
	NSArray *priorityList;
	NSArray *statusList;
}

@property (nonatomic, retain) NSArray *priorityList;
@property (nonatomic, retain) NSArray *statusList;
@property (nonatomic, retain) NSManagedObjectContext *managedObjectContext;
@property (nonatomic) int selectedPriority;
@property (nonatomic) int selectedStatus;
@property (nonatomic, retain) ListEntity *list;
@property (nonatomic, retain) ItemEntity *item;
@property (nonatomic) bool inAddMode;
@property (nonatomic, retain) IBOutlet UITextField *nameTextField;
@property (nonatomic, retain) IBOutlet UITextView *descriptionTextField;
@property (nonatomic, retain) IBOutlet UIPickerView *priorityStatusPicker;
-(IBAction)dismissKeyboard: (id) sender;
@end
