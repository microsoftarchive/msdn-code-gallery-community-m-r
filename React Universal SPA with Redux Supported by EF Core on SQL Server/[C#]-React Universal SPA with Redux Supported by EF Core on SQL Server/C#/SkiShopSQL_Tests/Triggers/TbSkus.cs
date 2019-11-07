using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSkiShopDB
{
    [TestClass()]
    public class TbSkus : SqlDatabaseTestClass
    {

        public TbSkus()
        {
            InitializeComponent();
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            base.InitializeTest();
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            base.CleanupTest();
        }

        #region Designer support code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Skus_Del_PreventTest_ById_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TbSkus));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Skus_InsertTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition4;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition4;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Skus_Del_PreventTest_ById_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Skus_Del_PreventTest_ByNo_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Skus_Del_PreventTest_ByNo_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Skus_InsertTest_PretestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition3;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition3;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Skus_InsertTest_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition5;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Skus_UpdateTest_Add_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition6;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition9;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Skus_UpdateTest_Add_PretestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition6;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition5;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Skus_UpdateTest_Add_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition7;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Skus_UpdateTest_Subtract_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition8;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition7;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Skus_UpdateTest_Subtract_PretestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition10;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition8;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Skus_UpdateTest_Subtract_PosttestAction;
            this.dbo_Trigger_Skus_Del_PreventTest_ByIdData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_Trigger_Skus_InsertTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_Trigger_Skus_Del_PreventTest_ByNoData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_Trigger_Skus_UpdateTest_AddData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_Trigger_Skus_UpdateTest_SubtractData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            dbo_Trigger_Skus_Del_PreventTest_ById_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_Trigger_Skus_InsertTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            scalarValueCondition4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_Trigger_Skus_Del_PreventTest_ById_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            scalarValueCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_Trigger_Skus_Del_PreventTest_ByNo_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_Trigger_Skus_Del_PreventTest_ByNo_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            scalarValueCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_Trigger_Skus_InsertTest_PretestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            scalarValueCondition3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_Trigger_Skus_InsertTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition5 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            dbo_Trigger_Skus_UpdateTest_Add_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            scalarValueCondition6 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            rowCountCondition9 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            dbo_Trigger_Skus_UpdateTest_Add_PretestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition6 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            scalarValueCondition5 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_Trigger_Skus_UpdateTest_Add_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition7 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            dbo_Trigger_Skus_UpdateTest_Subtract_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition8 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            scalarValueCondition7 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_Trigger_Skus_UpdateTest_Subtract_PretestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition10 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            scalarValueCondition8 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_Trigger_Skus_UpdateTest_Subtract_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            // 
            // dbo_Trigger_Skus_Del_PreventTest_ById_TestAction
            // 
            resources.ApplyResources(dbo_Trigger_Skus_Del_PreventTest_ById_TestAction, "dbo_Trigger_Skus_Del_PreventTest_ById_TestAction");
            // 
            // dbo_Trigger_Skus_InsertTest_TestAction
            // 
            dbo_Trigger_Skus_InsertTest_TestAction.Conditions.Add(rowCountCondition4);
            dbo_Trigger_Skus_InsertTest_TestAction.Conditions.Add(scalarValueCondition4);
            resources.ApplyResources(dbo_Trigger_Skus_InsertTest_TestAction, "dbo_Trigger_Skus_InsertTest_TestAction");
            // 
            // rowCountCondition4
            // 
            rowCountCondition4.Enabled = true;
            rowCountCondition4.Name = "rowCountCondition4";
            rowCountCondition4.ResultSet = 1;
            rowCountCondition4.RowCount = 1;
            // 
            // scalarValueCondition4
            // 
            scalarValueCondition4.ColumnNumber = 1;
            scalarValueCondition4.Enabled = true;
            scalarValueCondition4.ExpectedValue = "1";
            scalarValueCondition4.Name = "scalarValueCondition4";
            scalarValueCondition4.NullExpected = false;
            scalarValueCondition4.ResultSet = 1;
            scalarValueCondition4.RowNumber = 1;
            // 
            // dbo_Trigger_Skus_Del_PreventTest_ById_PosttestAction
            // 
            dbo_Trigger_Skus_Del_PreventTest_ById_PosttestAction.Conditions.Add(rowCountCondition1);
            dbo_Trigger_Skus_Del_PreventTest_ById_PosttestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(dbo_Trigger_Skus_Del_PreventTest_ById_PosttestAction, "dbo_Trigger_Skus_Del_PreventTest_ById_PosttestAction");
            // 
            // rowCountCondition1
            // 
            rowCountCondition1.Enabled = true;
            rowCountCondition1.Name = "rowCountCondition1";
            rowCountCondition1.ResultSet = 1;
            rowCountCondition1.RowCount = 1;
            // 
            // scalarValueCondition1
            // 
            scalarValueCondition1.ColumnNumber = 1;
            scalarValueCondition1.Enabled = true;
            scalarValueCondition1.ExpectedValue = "1";
            scalarValueCondition1.Name = "scalarValueCondition1";
            scalarValueCondition1.NullExpected = false;
            scalarValueCondition1.ResultSet = 1;
            scalarValueCondition1.RowNumber = 1;
            // 
            // dbo_Trigger_Skus_Del_PreventTest_ByNo_TestAction
            // 
            resources.ApplyResources(dbo_Trigger_Skus_Del_PreventTest_ByNo_TestAction, "dbo_Trigger_Skus_Del_PreventTest_ByNo_TestAction");
            // 
            // dbo_Trigger_Skus_Del_PreventTest_ByNo_PosttestAction
            // 
            dbo_Trigger_Skus_Del_PreventTest_ByNo_PosttestAction.Conditions.Add(rowCountCondition2);
            dbo_Trigger_Skus_Del_PreventTest_ByNo_PosttestAction.Conditions.Add(scalarValueCondition2);
            resources.ApplyResources(dbo_Trigger_Skus_Del_PreventTest_ByNo_PosttestAction, "dbo_Trigger_Skus_Del_PreventTest_ByNo_PosttestAction");
            // 
            // rowCountCondition2
            // 
            rowCountCondition2.Enabled = true;
            rowCountCondition2.Name = "rowCountCondition2";
            rowCountCondition2.ResultSet = 1;
            rowCountCondition2.RowCount = 1;
            // 
            // scalarValueCondition2
            // 
            scalarValueCondition2.ColumnNumber = 1;
            scalarValueCondition2.Enabled = true;
            scalarValueCondition2.ExpectedValue = "12345601";
            scalarValueCondition2.Name = "scalarValueCondition2";
            scalarValueCondition2.NullExpected = false;
            scalarValueCondition2.ResultSet = 1;
            scalarValueCondition2.RowNumber = 1;
            // 
            // dbo_Trigger_Skus_InsertTest_PretestAction
            // 
            dbo_Trigger_Skus_InsertTest_PretestAction.Conditions.Add(rowCountCondition3);
            dbo_Trigger_Skus_InsertTest_PretestAction.Conditions.Add(scalarValueCondition3);
            resources.ApplyResources(dbo_Trigger_Skus_InsertTest_PretestAction, "dbo_Trigger_Skus_InsertTest_PretestAction");
            // 
            // rowCountCondition3
            // 
            rowCountCondition3.Enabled = true;
            rowCountCondition3.Name = "rowCountCondition3";
            rowCountCondition3.ResultSet = 1;
            rowCountCondition3.RowCount = 1;
            // 
            // scalarValueCondition3
            // 
            scalarValueCondition3.ColumnNumber = 1;
            scalarValueCondition3.Enabled = true;
            scalarValueCondition3.ExpectedValue = "True";
            scalarValueCondition3.Name = "scalarValueCondition3";
            scalarValueCondition3.NullExpected = false;
            scalarValueCondition3.ResultSet = 1;
            scalarValueCondition3.RowNumber = 1;
            // 
            // dbo_Trigger_Skus_InsertTest_PosttestAction
            // 
            dbo_Trigger_Skus_InsertTest_PosttestAction.Conditions.Add(rowCountCondition5);
            resources.ApplyResources(dbo_Trigger_Skus_InsertTest_PosttestAction, "dbo_Trigger_Skus_InsertTest_PosttestAction");
            // 
            // rowCountCondition5
            // 
            rowCountCondition5.Enabled = true;
            rowCountCondition5.Name = "rowCountCondition5";
            rowCountCondition5.ResultSet = 1;
            rowCountCondition5.RowCount = 0;
            // 
            // dbo_Trigger_Skus_UpdateTest_Add_TestAction
            // 
            dbo_Trigger_Skus_UpdateTest_Add_TestAction.Conditions.Add(scalarValueCondition6);
            dbo_Trigger_Skus_UpdateTest_Add_TestAction.Conditions.Add(rowCountCondition9);
            resources.ApplyResources(dbo_Trigger_Skus_UpdateTest_Add_TestAction, "dbo_Trigger_Skus_UpdateTest_Add_TestAction");
            // 
            // scalarValueCondition6
            // 
            scalarValueCondition6.ColumnNumber = 1;
            scalarValueCondition6.Enabled = true;
            scalarValueCondition6.ExpectedValue = "False";
            scalarValueCondition6.Name = "scalarValueCondition6";
            scalarValueCondition6.NullExpected = false;
            scalarValueCondition6.ResultSet = 1;
            scalarValueCondition6.RowNumber = 1;
            // 
            // rowCountCondition9
            // 
            rowCountCondition9.Enabled = true;
            rowCountCondition9.Name = "rowCountCondition9";
            rowCountCondition9.ResultSet = 1;
            rowCountCondition9.RowCount = 1;
            // 
            // dbo_Trigger_Skus_UpdateTest_Add_PretestAction
            // 
            dbo_Trigger_Skus_UpdateTest_Add_PretestAction.Conditions.Add(rowCountCondition6);
            dbo_Trigger_Skus_UpdateTest_Add_PretestAction.Conditions.Add(scalarValueCondition5);
            resources.ApplyResources(dbo_Trigger_Skus_UpdateTest_Add_PretestAction, "dbo_Trigger_Skus_UpdateTest_Add_PretestAction");
            // 
            // rowCountCondition6
            // 
            rowCountCondition6.Enabled = true;
            rowCountCondition6.Name = "rowCountCondition6";
            rowCountCondition6.ResultSet = 1;
            rowCountCondition6.RowCount = 1;
            // 
            // scalarValueCondition5
            // 
            scalarValueCondition5.ColumnNumber = 1;
            scalarValueCondition5.Enabled = true;
            scalarValueCondition5.ExpectedValue = "True";
            scalarValueCondition5.Name = "scalarValueCondition5";
            scalarValueCondition5.NullExpected = false;
            scalarValueCondition5.ResultSet = 1;
            scalarValueCondition5.RowNumber = 1;
            // 
            // dbo_Trigger_Skus_UpdateTest_Add_PosttestAction
            // 
            dbo_Trigger_Skus_UpdateTest_Add_PosttestAction.Conditions.Add(rowCountCondition7);
            resources.ApplyResources(dbo_Trigger_Skus_UpdateTest_Add_PosttestAction, "dbo_Trigger_Skus_UpdateTest_Add_PosttestAction");
            // 
            // rowCountCondition7
            // 
            rowCountCondition7.Enabled = true;
            rowCountCondition7.Name = "rowCountCondition7";
            rowCountCondition7.ResultSet = 1;
            rowCountCondition7.RowCount = 0;
            // 
            // dbo_Trigger_Skus_UpdateTest_Subtract_TestAction
            // 
            dbo_Trigger_Skus_UpdateTest_Subtract_TestAction.Conditions.Add(rowCountCondition8);
            dbo_Trigger_Skus_UpdateTest_Subtract_TestAction.Conditions.Add(scalarValueCondition7);
            resources.ApplyResources(dbo_Trigger_Skus_UpdateTest_Subtract_TestAction, "dbo_Trigger_Skus_UpdateTest_Subtract_TestAction");
            // 
            // rowCountCondition8
            // 
            rowCountCondition8.Enabled = true;
            rowCountCondition8.Name = "rowCountCondition8";
            rowCountCondition8.ResultSet = 1;
            rowCountCondition8.RowCount = 1;
            // 
            // scalarValueCondition7
            // 
            scalarValueCondition7.ColumnNumber = 1;
            scalarValueCondition7.Enabled = true;
            scalarValueCondition7.ExpectedValue = "True";
            scalarValueCondition7.Name = "scalarValueCondition7";
            scalarValueCondition7.NullExpected = false;
            scalarValueCondition7.ResultSet = 1;
            scalarValueCondition7.RowNumber = 1;
            // 
            // dbo_Trigger_Skus_UpdateTest_Subtract_PretestAction
            // 
            dbo_Trigger_Skus_UpdateTest_Subtract_PretestAction.Conditions.Add(rowCountCondition10);
            dbo_Trigger_Skus_UpdateTest_Subtract_PretestAction.Conditions.Add(scalarValueCondition8);
            resources.ApplyResources(dbo_Trigger_Skus_UpdateTest_Subtract_PretestAction, "dbo_Trigger_Skus_UpdateTest_Subtract_PretestAction");
            // 
            // rowCountCondition10
            // 
            rowCountCondition10.Enabled = true;
            rowCountCondition10.Name = "rowCountCondition10";
            rowCountCondition10.ResultSet = 1;
            rowCountCondition10.RowCount = 1;
            // 
            // scalarValueCondition8
            // 
            scalarValueCondition8.ColumnNumber = 1;
            scalarValueCondition8.Enabled = true;
            scalarValueCondition8.ExpectedValue = "False";
            scalarValueCondition8.Name = "scalarValueCondition8";
            scalarValueCondition8.NullExpected = false;
            scalarValueCondition8.ResultSet = 1;
            scalarValueCondition8.RowNumber = 1;
            // 
            // dbo_Trigger_Skus_UpdateTest_Subtract_PosttestAction
            // 
            resources.ApplyResources(dbo_Trigger_Skus_UpdateTest_Subtract_PosttestAction, "dbo_Trigger_Skus_UpdateTest_Subtract_PosttestAction");
            // 
            // dbo_Trigger_Skus_Del_PreventTest_ByIdData
            // 
            this.dbo_Trigger_Skus_Del_PreventTest_ByIdData.PosttestAction = dbo_Trigger_Skus_Del_PreventTest_ById_PosttestAction;
            this.dbo_Trigger_Skus_Del_PreventTest_ByIdData.PretestAction = null;
            this.dbo_Trigger_Skus_Del_PreventTest_ByIdData.TestAction = dbo_Trigger_Skus_Del_PreventTest_ById_TestAction;
            // 
            // dbo_Trigger_Skus_InsertTestData
            // 
            this.dbo_Trigger_Skus_InsertTestData.PosttestAction = dbo_Trigger_Skus_InsertTest_PosttestAction;
            this.dbo_Trigger_Skus_InsertTestData.PretestAction = dbo_Trigger_Skus_InsertTest_PretestAction;
            this.dbo_Trigger_Skus_InsertTestData.TestAction = dbo_Trigger_Skus_InsertTest_TestAction;
            // 
            // dbo_Trigger_Skus_Del_PreventTest_ByNoData
            // 
            this.dbo_Trigger_Skus_Del_PreventTest_ByNoData.PosttestAction = dbo_Trigger_Skus_Del_PreventTest_ByNo_PosttestAction;
            this.dbo_Trigger_Skus_Del_PreventTest_ByNoData.PretestAction = null;
            this.dbo_Trigger_Skus_Del_PreventTest_ByNoData.TestAction = dbo_Trigger_Skus_Del_PreventTest_ByNo_TestAction;
            // 
            // dbo_Trigger_Skus_UpdateTest_AddData
            // 
            this.dbo_Trigger_Skus_UpdateTest_AddData.PosttestAction = dbo_Trigger_Skus_UpdateTest_Add_PosttestAction;
            this.dbo_Trigger_Skus_UpdateTest_AddData.PretestAction = dbo_Trigger_Skus_UpdateTest_Add_PretestAction;
            this.dbo_Trigger_Skus_UpdateTest_AddData.TestAction = dbo_Trigger_Skus_UpdateTest_Add_TestAction;
            // 
            // dbo_Trigger_Skus_UpdateTest_SubtractData
            // 
            this.dbo_Trigger_Skus_UpdateTest_SubtractData.PosttestAction = dbo_Trigger_Skus_UpdateTest_Subtract_PosttestAction;
            this.dbo_Trigger_Skus_UpdateTest_SubtractData.PretestAction = dbo_Trigger_Skus_UpdateTest_Subtract_PretestAction;
            this.dbo_Trigger_Skus_UpdateTest_SubtractData.TestAction = dbo_Trigger_Skus_UpdateTest_Subtract_TestAction;
        }

        #endregion


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        #endregion

        [TestMethod(), ExpectedSqlException(MessageNumber = 50001, MatchFirstError = false, State = 1)]
        public void dbo_Trigger_Skus_Del_PreventTest_ById()
        {
            SqlDatabaseTestActions testActions = this.dbo_Trigger_Skus_Del_PreventTest_ByIdData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_Trigger_Skus_InsertTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_Trigger_Skus_InsertTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }
        [TestMethod(), ExpectedSqlException(MessageNumber = 50001, MatchFirstError = false, State = 1)]
        public void dbo_Trigger_Skus_Del_PreventTest_ByNo()
        {
            SqlDatabaseTestActions testActions = this.dbo_Trigger_Skus_Del_PreventTest_ByNoData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }
        [TestMethod()]
        public void dbo_Trigger_Skus_UpdateTest_Add()
        {
            SqlDatabaseTestActions testActions = this.dbo_Trigger_Skus_UpdateTest_AddData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }
        [TestMethod()]
        public void dbo_Trigger_Skus_UpdateTest_Subtract()
        {
            SqlDatabaseTestActions testActions = this.dbo_Trigger_Skus_UpdateTest_SubtractData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }



        private SqlDatabaseTestActions dbo_Trigger_Skus_Del_PreventTest_ByIdData;
        private SqlDatabaseTestActions dbo_Trigger_Skus_InsertTestData;
        private SqlDatabaseTestActions dbo_Trigger_Skus_Del_PreventTest_ByNoData;
        private SqlDatabaseTestActions dbo_Trigger_Skus_UpdateTest_AddData;
        private SqlDatabaseTestActions dbo_Trigger_Skus_UpdateTest_SubtractData;
    }
}
