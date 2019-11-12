using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using Microsoft.Graph;

namespace Zhukov.Demo.GraphLicensesReport
{
    class Program
    {
        static void Main(string[] args)
        {
            // Microsoft Graph Provider
            var provider = new GraphProvider();

            // Microsoft Graph Client
            var client = new GraphServiceClient(provider);

            // List of users
            var users = new List<User>();

            // Paged Request
            IGraphServiceUsersCollectionPage items = null;

            // Fill up the list while next page is available
            while (items == null || items.NextPageRequest != null)
            {
                // Request to Microsoft Graph
                var itemsRequest = items?.NextPageRequest ?? client
                                       .Users
                                       .Request()
                                       .OrderBy("displayName")
                                       .Select(
                                           "displayName, mail, department, assignedLicenses, accountEnabled, userType")
                                       .Top(100);

                // Get Users
                items = itemsRequest
                    .GetAsync()
                    .GetAwaiter()
                    .GetResult();

                // Add users to the list
                users.AddRange(items);
            }

// New Excel Workbook
using (var workbook = new XLWorkbook())
{
    // Add new sheet
    using (var worksheet = workbook.Worksheets.Add("Assigned licenses"))
    {
        // row number
        var r = 1;
        // Header -->
        worksheet.Cell(r, 1).Value = "User";
        worksheet.Cell(r, 2).Value = "User Type";
        worksheet.Cell(r, 3).Value = "Email";
        worksheet.Cell(r, 4).Value = "Department";
        worksheet.Cell(r, 5).Value = "Blocked";

        // column number
        var c = 6;
        foreach (var sku in SKUs)
        {
            worksheet.Cell(r, c).Value = sku.Value;
            c = c + 1;
        }
        // <-- Header


        // go to the next row
        r = r + 1;

        foreach (var user in users)
        {
            // User Information
            worksheet.Cell(r, 1).Value = user.DisplayName;
            worksheet.Cell(r, 2).Value = user.UserType;
            worksheet.Cell(r, 3).Value = user.Mail;
            worksheet.Cell(r, 4).Value = user.Department;
            worksheet.Cell(r, 5).Value = user.AccountEnabled == false ? "Blocked" : string.Empty;

            // go to 6th column (1-5 columns for user information)
            c = 6;

            foreach (var sku in SKUs)
            {
                // is current SKU is assigned to current user
                var assignedFlag = user
                    .AssignedLicenses
                    .Any(x => x.SkuId == sku.Key);

                worksheet.Cell(r, c).Value = assignedFlag ? 1 : 0;

                // go to the next column
                c = c + 1;
            }

            // go to the next row
            r = r + 1;
        }

        // create table -->
        var table = worksheet.Range(1, 1, r - 1, 5 + SKUs.Count).CreateTable();
        table.Theme = XLTableTheme.TableStyleMedium2;
        // <-- create table

        // adjust column width to content
        worksheet.Columns().AdjustToContents(5, 40);

        // freeze first 2 columns
        worksheet.SheetView.FreezeColumns(2);
    }

    // save the report
    workbook.SaveAs("AssignedLicesesReport.xlsx");
}
        }

        private static readonly Dictionary<Guid, string> SKUs = new Dictionary<Guid, string>
        {
            {new Guid("2b9c8e7c-319c-43a2-a2a0-48c5c6161de7"), "Azure Active Directory Basic"},
            {new Guid("c7d15985-e746-4f01-b113-20b575898250"), "Dynamics 365 for Field Service Enterprise Edition"},
            {new Guid("6a4a1628-9b9a-424d-bed5-4118f0ede3fd"), "Dynamics 365 for Financials for IWs"},
            {new Guid("28b81ef4-b535-4e5c-ae14-bd40148c89c5"), "Dynamics 365 for Project Service Automation Enterprise Edition"},
            {new Guid("8e7a3d30-d97d-43ab-837c-d7701cef83dc"), "Dynamics 365 for Sales Enterprise Edition"},
            {new Guid("e561871f-74fa-4f02-abee-5b0ef54dd36d"), "Dynamics 365 for Talent: Attract"},
            {new Guid("1e1a282c-9c54-43a2-9310-98ef728faace"), "Dynamics 365 for Team Members Enterprise Edition"},
            {new Guid("ea126fc5-a19e-42e2-a731-da9d437bffcf"), "Dynamics 365 Plan 1 Enterprise Edition"},
            {new Guid("b05e124f-c7cc-45a0-a6aa-8cf78c946968"), "Enterprise Mobility + Security E5"},
            {new Guid("efccb6f7-5641-4e0e-bd10-b4976e1bf68e"), "Enterprise Mobility Suite"},
            {new Guid("9aaf7827-d63c-4b61-89c3-182f06f82e5c"), "Exchange Online (Plan 1)"},
            {new Guid("0f9b09cb-62d1-4ff4-9129-43f4996f83f4"), "Flow for Office 365 in E1"},
            {new Guid("76846ad7-7776-4c40-a281-a386362dd1b9"), "Flow for Office 365 in E3"},
            {new Guid("061f9ace-7d42-4136-88ac-31dc755f143f"), "Intune"},
            {new Guid("fcecd1f9-a91e-488d-a918-a96cdb6ce2b0"), "Microsoft Dynamics AX7 User Trial"},
            {new Guid("f30db892-07e9-47e9-837c-80727f46fd3d"), "Microsoft Flow Free"},
            {new Guid("87bbbc60-4754-4998-8c88-227dca264858"), "Microsoft PowerApps and Logic flows"},
            {new Guid("dcb1a3ae-b33f-4487-846a-a640262fadf4"), "Microsoft PowerApps Plan 2 Trial"},
            {new Guid("1f2f344a-700d-42c9-9427-5cea1d5d7ba6"), "Microsoft Stream Trial"},
            {new Guid("57ff2da0-773e-42df-b2af-ffb7a2317929"), "Microsoft Teams"},
            {new Guid("3b555118-da6a-4418-894f-7df1e2096870"), "Office 365 Business Essentials"},
            {new Guid("f245ecc8-75af-4f8e-b61f-27d8114de5f3"), "Office 365 Business Premium"},
            {new Guid("18181a46-0d4e-45cd-891e-60aabd171b4e"), "Office 365 Enterprise E1"},
            {new Guid("6fd2c87f-b296-42f0-b197-1e91e994b900"), "Office 365 Enterprise E3"},
            {new Guid("c7df2760-2c81-4ef7-b578-5b5392b571df"), "Office 365 Enterprise E5"},
            {new Guid("26d45bd9-adf1-46cd-a9e1-51e9a5524128"), "Office 365 Enterprise E5 without Audio Conferencing"},
            {new Guid("e95bec33-7c88-4a70-8e19-b10bd9d0c014"), "Office Online"},
            {new Guid("92f7a6f3-b89b-4bbd-8c30-809e6da5ad1c"), "Power App for Office 365 in E1"},
            {new Guid("a403ebcc-fae0-4ca2-8c8c-7a907fd6c235"), "Power BI (Free)"},
            {new Guid("f8a1db68-be16-40ed-86d5-cb42ce701560"), "Power BI Pro"},
            {new Guid("c68f8d98-5534-41c8-bf36-22fa496fa792"), "PowerApps for Office 365 in E3"},
            {new Guid("53818b1b-4a27-454b-8896-0dba576410e6"), "Project Online Professional"},
            {new Guid("a10d5e58-74da-4312-95c8-76be4e5b75a0"), "Project Pro for Office 365"},
            {new Guid("8c4ce438-32a7-4ac5-91a6-e22ae08d9c8b"), "Rights Management Adhoc"},
            {new Guid("1fc08a02-8b3d-43b9-831e-f76859e04e1a"), "SharePoint Online (Plan 1)"},
            {new Guid("0feaeb32-d00e-4d66-bd5a-43b5b83db82c"), "Skype Enterprise Online (plan 2)"},
            {new Guid("e43b5b99-8dfb-405f-9987-dc307f34bcbd"), "Skype for Business Cloud PBX"},
            {new Guid("47794cd0-f0e5-45c5-9033-2eb6b5fc84e0"), "Skype for Business PSTN Consumption"},
            {new Guid("d3b4fe1f-9992-4930-8acb-ca6ec609365e"), "Skype for Business PSTN Domestic and International Calling"},
            {new Guid("c5928f49-12ba-48f7-ada3-0d743a3601d5"), "Visio Pro for Office 365"}
        };
    }
}