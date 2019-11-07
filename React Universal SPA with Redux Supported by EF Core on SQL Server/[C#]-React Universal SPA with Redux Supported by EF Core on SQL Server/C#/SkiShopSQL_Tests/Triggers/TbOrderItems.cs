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
    public class TbOrderItems : SqlDatabaseTestClass
    {

        public TbOrderItems()
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
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_OrderItems_Del_PreventTest_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TbOrderItems));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_OrderItems_InsertTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition3;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition3;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction testInitializeAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction testCleanupAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_OrderItems_InsertTest_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_OrderItems_InsertTest_UpdateSoldOut_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition4;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition4;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_OrderItems_InsertTest_UpdateSoldOut_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_OrderItems_Del_PreventTest_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition5;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition5;
            this.dbo_Trigger_OrderItems_Del_PreventTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_Trigger_OrderItems_InsertTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_Trigger_OrderItems_InsertTest_UpdateSoldOutData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            dbo_Trigger_OrderItems_Del_PreventTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_Trigger_OrderItems_InsertTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            scalarValueCondition3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            testInitializeAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            scalarValueCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            testCleanupAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            scalarValueCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_Trigger_OrderItems_InsertTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_Trigger_OrderItems_InsertTest_UpdateSoldOut_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            scalarValueCondition4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_Trigger_OrderItems_InsertTest_UpdateSoldOut_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_Trigger_OrderItems_Del_PreventTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition5 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            scalarValueCondition5 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            // 
            // dbo_Trigger_OrderItems_Del_PreventTest_TestAction
            // 
            resources.ApplyResources(dbo_Trigger_OrderItems_Del_PreventTest_TestAction, "dbo_Trigger_OrderItems_Del_PreventTest_TestAction");
            // 
            // dbo_Trigger_OrderItems_InsertTest_TestAction
            // 
            dbo_Trigger_OrderItems_InsertTest_TestAction.Conditions.Add(rowCountCondition3);
            dbo_Trigger_OrderItems_InsertTest_TestAction.Conditions.Add(scalarValueCondition3);
            resources.ApplyResources(dbo_Trigger_OrderItems_InsertTest_TestAction, "dbo_Trigger_OrderItems_InsertTest_TestAction");
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
            scalarValueCondition3.ExpectedValue = "1";
            scalarValueCondition3.Name = "scalarValueCondition3";
            scalarValueCondition3.NullExpected = false;
            scalarValueCondition3.ResultSet = 1;
            scalarValueCondition3.RowNumber = 1;
            // 
            // testInitializeAction
            // 
            testInitializeAction.Conditions.Add(rowCountCondition1);
            testInitializeAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(testInitializeAction, "testInitializeAction");
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
            // testCleanupAction
            // 
            testCleanupAction.Conditions.Add(rowCountCondition2);
            testCleanupAction.Conditions.Add(scalarValueCondition2);
            resources.ApplyResources(testCleanupAction, "testCleanupAction");
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
            scalarValueCondition2.ExpectedValue = "1";
            scalarValueCondition2.Name = "scalarValueCondition2";
            scalarValueCondition2.NullExpected = false;
            scalarValueCondition2.ResultSet = 1;
            scalarValueCondition2.RowNumber = 1;
            // 
            // dbo_Trigger_OrderItems_InsertTest_PosttestAction
            // 
            resources.ApplyResources(dbo_Trigger_OrderItems_InsertTest_PosttestAction, "dbo_Trigger_OrderItems_InsertTest_PosttestAction");
            // 
            // dbo_Trigger_OrderItems_InsertTest_UpdateSoldOut_TestAction
            // 
            dbo_Trigger_OrderItems_InsertTest_UpdateSoldOut_TestAction.Conditions.Add(rowCountCondition4);
            dbo_Trigger_OrderItems_InsertTest_UpdateSoldOut_TestAction.Conditions.Add(scalarValueCondition4);
            resources.ApplyResources(dbo_Trigger_OrderItems_InsertTest_UpdateSoldOut_TestAction, "dbo_Trigger_OrderItems_InsertTest_UpdateSoldOut_TestAction");
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
            // dbo_Trigger_OrderItems_InsertTest_UpdateSoldOut_PosttestAction
            // 
            resources.ApplyResources(dbo_Trigger_OrderItems_InsertTest_UpdateSoldOut_PosttestAction, "dbo_Trigger_OrderItems_InsertTest_UpdateSoldOut_PosttestAction");
            // 
            // dbo_Trigger_OrderItems_Del_PreventTest_PosttestAction
            // 
            dbo_Trigger_OrderItems_Del_PreventTest_PosttestAction.Conditions.Add(rowCountCondition5);
            dbo_Trigger_OrderItems_Del_PreventTest_PosttestAction.Conditions.Add(scalarValueCondition5);
            resources.ApplyResources(dbo_Trigger_OrderItems_Del_PreventTest_PosttestAction, "dbo_Trigger_OrderItems_Del_PreventTest_PosttestAction");
            // 
            // rowCountCondition5
            // 
            rowCountCondition5.Enabled = true;
            rowCountCondition5.Name = "rowCountCondition5";
            rowCountCondition5.ResultSet = 1;
            rowCountCondition5.RowCount = 1;
            // 
            // scalarValueCondition5
            // 
            scalarValueCondition5.ColumnNumber = 1;
            scalarValueCondition5.Enabled = true;
            scalarValueCondition5.ExpectedValue = "1";
            scalarValueCondition5.Name = "scalarValueCondition5";
            scalarValueCondition5.NullExpected = false;
            scalarValueCondition5.ResultSet = 1;
            scalarValueCondition5.RowNumber = 1;
            // 
            // dbo_Trigger_OrderItems_Del_PreventTestData
            // 
            this.dbo_Trigger_OrderItems_Del_PreventTestData.PosttestAction = dbo_Trigger_OrderItems_Del_PreventTest_PosttestAction;
            this.dbo_Trigger_OrderItems_Del_PreventTestData.PretestAction = null;
            this.dbo_Trigger_OrderItems_Del_PreventTestData.TestAction = dbo_Trigger_OrderItems_Del_PreventTest_TestAction;
            // 
            // dbo_Trigger_OrderItems_InsertTestData
            // 
            this.dbo_Trigger_OrderItems_InsertTestData.PosttestAction = dbo_Trigger_OrderItems_InsertTest_PosttestAction;
            this.dbo_Trigger_OrderItems_InsertTestData.PretestAction = null;
            this.dbo_Trigger_OrderItems_InsertTestData.TestAction = dbo_Trigger_OrderItems_InsertTest_TestAction;
            // 
            // dbo_Trigger_OrderItems_InsertTest_UpdateSoldOutData
            // 
            this.dbo_Trigger_OrderItems_InsertTest_UpdateSoldOutData.PosttestAction = dbo_Trigger_OrderItems_InsertTest_UpdateSoldOut_PosttestAction;
            this.dbo_Trigger_OrderItems_InsertTest_UpdateSoldOutData.PretestAction = null;
            this.dbo_Trigger_OrderItems_InsertTest_UpdateSoldOutData.TestAction = dbo_Trigger_OrderItems_InsertTest_UpdateSoldOut_TestAction;
            // 
            // TbOrderItems
            // 
            this.TestCleanupAction = testCleanupAction;
            this.TestInitializeAction = testInitializeAction;
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
        public void dbo_Trigger_OrderItems_Del_PreventTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_Trigger_OrderItems_Del_PreventTestData;
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
        public void dbo_Trigger_OrderItems_InsertTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_Trigger_OrderItems_InsertTestData;
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
        public void dbo_Trigger_OrderItems_InsertTest_UpdateSoldOut()
        {
            SqlDatabaseTestActions testActions = this.dbo_Trigger_OrderItems_InsertTest_UpdateSoldOutData;
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

        private SqlDatabaseTestActions dbo_Trigger_OrderItems_Del_PreventTestData;
        private SqlDatabaseTestActions dbo_Trigger_OrderItems_InsertTestData;
        private SqlDatabaseTestActions dbo_Trigger_OrderItems_InsertTest_UpdateSoldOutData;
    }
}
