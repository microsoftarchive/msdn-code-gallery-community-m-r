// =====================================================================
//  This file is part of the Microsoft Dynamics CRM SDK code samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
// =====================================================================

// Implementation notes:
// Some SDK classes have the following methods.
// 1. LoadFromXml - It takes an XElement and returns its class instance, which is
// a static method so that other classes can call it without instantiating the class.
// 2. ToXml - It returns XML data for the SOAP request. This is not a static method
// as it needs the actual instance data to XML.
// 3. Some classes have a ToValueXml method, which is a core part of ToXml. The reason 
// is that different methods may need a different tag.

// There is some duplicate code in places which may be consolidated if necessary.
// The implementation is similar to that used in Sdk.Soap.js with slight changes 
// due to the language differences.
// For more information about Sdk.Soap.js, see http://code.msdn.microsoft.com/SdkSoapjs-9b51b99a

namespace Microsoft.Crm.Sdk.Samples
{
    public static class BusinessUnitInheritanceMask
    {
        public const int InheritNone = 0;
        public const int InheritProcessTemplate = 1;
        public const int InheritEmailTemplate = 2;
        public const int InheritReferralSource = 4;
        public const int InheritCompetitor = 8;
        public const int InheritSalesProcess = 16;
        public const int MustInheritProcessTemplate = 32;
        public const int MustInheritEmailTemplate = 64;
        public const int MustInheritReferralSource = 128;
        public const int MustInheritCompetitor = 256;
        public const int MustInheritSalesProcess = 512;
        public const int InheritAll = 1023;
    }
    public static class CalendarRuleExtentCode
    {
        public const int Transparent = 0;
        public const int SubtractRecurrenceIntervals = 1;
        public const int SubtractResults = 2;
    }
    public static class ListMemberType
    {
        public const int Account = 1;
        public const int Contact = 2;
        public const int Lead = 4;
    }
    public static class OrganizationFiscalYearDisplayCode
    {
        public const int UseStartDate = 1;
        public const int UseEndDate = 2;
    }
    public static class SavedQueryQueryType
    {
        public const int AddressBookFilters = 512;
        public const int AdvancedSearch = 1;
        public const int CustomDefinedView = 16384;
        public const int InteractiveWorkflowView = 4096;
        public const int LookupView = 64;
        public const int MainApplicationView = 0;
        public const int MainApplicationViewWithoutSubject = 1024;
        public const int OfflineFilters = 16;
        public const int OfflineTemplate = 8192;
        public const int OutlookFilters = 256;
        public const int OutlookTemplate = 131072;
        public const int QuickFindSearch = 4;
        public const int Reporting = 8;
        public const int SavedQueryTypeOther = 2048;
        public const int SMAppointmentBookView = 128;
        public const int SubGrid = 2;
    }
    public static class SdkMessageAvailability
    {
        public const int Server = 0;
        public const int Client = 1;
        public const int All = 2;
    }
    public static class SdkMessageFilterAvailability
    {
        public const int Server = 0;
        public const int Client = 1;
        public const int All = 2;
    }
    public static class SubscriptionSubscriptionType
    {
        public const int Offline = 0;
        public const int Outlook = 1;
        public const int AddressBookProvider = 2;
    }
    public static class TemplateGenerationTypeCode
    {
        public const int BulkDupDetectCompleted = 1;
        public const int BulkDeleteCompleted = 2;
        public const int BulkDeleteCompletedWithFailures = 3;
        public const int BulkDeleteFailed = 4;
        public const int ImportCompleted = 5;
        public const int ImportFailed = 6;
    }
    public static class UserQueryQueryType
    {
        public const int MainApplicationView = 0;
        public const int AdvancedSearch = 1;
        public const int SubGrid = 2;
        public const int QuickFindSearch = 4;
        public const int Reporting = 8;
        public const int OfflineFilters = 16;
        public const int LookupView = 64;
        public const int SMAppointmentBookView = 128;
        public const int OutlookFilters = 256;
        public const int AddressBookFilters = 512;
        public const int MainApplicationViewWithoutSubject = 1024;
        public const int SavedQueryTypeOther = 2048;
        public const int InteractiveWorkflowView = 4096;
        public const int CustomDefinedView = 16384;
    }
    public static class UserSettingsAdvancedFindStartupMode
    {
        public const int Simple = 1;
        public const int Detailed = 2;
    }
    public static class UserSettingsDefaultCalendarView
    {
        public const int Day = 0;
        public const int Week = 1;
        public const int Month = 2;
    }
    public static class UserSettingsFullNameConventionCode
    {
        public const int LastFirst = 0;
        public const int FirstLast = 1;
        public const int LastFirstMiddleInitial = 2;
        public const int FirstMiddleInitialLast = 3;
        public const int LastFirstMiddle = 4;
        public const int FirstMiddleLast = 5;
        public const int LastSpaceFirst = 6;
        public const int LastNoSpaceFirst = 7;
    }
}