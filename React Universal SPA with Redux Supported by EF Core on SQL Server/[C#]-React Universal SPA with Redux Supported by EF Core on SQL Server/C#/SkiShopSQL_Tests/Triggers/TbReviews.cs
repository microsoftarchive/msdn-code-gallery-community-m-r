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
    public class TbReviews : SqlDatabaseTestClass
    {

        public TbReviews()
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
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Reviews_Del_PreventTest_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TbReviews));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Reviews_InsertTest_One_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction testInitializeAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction testCleanupAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Reviews_Del_PreventTest_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Reviews_InsertTest_One_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition3;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition3;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Reviews_InsertTest_Two_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Reviews_InsertTest_Two_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition4;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition4;
            this.dbo_Trigger_Reviews_Del_PreventTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_Trigger_Reviews_InsertTest_OneData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_Trigger_Reviews_InsertTest_TwoData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            dbo_Trigger_Reviews_Del_PreventTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_Trigger_Reviews_InsertTest_One_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            testInitializeAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            scalarValueCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            rowCountCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            testCleanupAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_Trigger_Reviews_Del_PreventTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            scalarValueCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_Trigger_Reviews_InsertTest_One_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            scalarValueCondition3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_Trigger_Reviews_InsertTest_Two_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_Trigger_Reviews_InsertTest_Two_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            scalarValueCondition4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            // 
            // dbo_Trigger_Reviews_Del_PreventTest_TestAction
            // 
            resources.ApplyResources(dbo_Trigger_Reviews_Del_PreventTest_TestAction, "dbo_Trigger_Reviews_Del_PreventTest_TestAction");
            // 
            // dbo_Trigger_Reviews_InsertTest_One_TestAction
            // 
            dbo_Trigger_Reviews_InsertTest_One_TestAction.Conditions.Add(rowCountCondition3);
            dbo_Trigger_Reviews_InsertTest_One_TestAction.Conditions.Add(scalarValueCondition3);
            resources.ApplyResources(dbo_Trigger_Reviews_InsertTest_One_TestAction, "dbo_Trigger_Reviews_InsertTest_One_TestAction");
            // 
            // testInitializeAction
            // 
            testInitializeAction.Conditions.Add(scalarValueCondition1);
            testInitializeAction.Conditions.Add(rowCountCondition1);
            resources.ApplyResources(testInitializeAction, "testInitializeAction");
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
            // rowCountCondition1
            // 
            rowCountCondition1.Enabled = true;
            rowCountCondition1.Name = "rowCountCondition1";
            rowCountCondition1.ResultSet = 1;
            rowCountCondition1.RowCount = 1;
            // 
            // testCleanupAction
            // 
            resources.ApplyResources(testCleanupAction, "testCleanupAction");
            // 
            // dbo_Trigger_Reviews_Del_PreventTestData
            // 
            this.dbo_Trigger_Reviews_Del_PreventTestData.PosttestAction = dbo_Trigger_Reviews_Del_PreventTest_PosttestAction;
            this.dbo_Trigger_Reviews_Del_PreventTestData.PretestAction = null;
            this.dbo_Trigger_Reviews_Del_PreventTestData.TestAction = dbo_Trigger_Reviews_Del_PreventTest_TestAction;
            // 
            // dbo_Trigger_Reviews_InsertTest_OneData
            // 
            this.dbo_Trigger_Reviews_InsertTest_OneData.PosttestAction = dbo_Trigger_Reviews_InsertTest_One_PosttestAction;
            this.dbo_Trigger_Reviews_InsertTest_OneData.PretestAction = null;
            this.dbo_Trigger_Reviews_InsertTest_OneData.TestAction = dbo_Trigger_Reviews_InsertTest_One_TestAction;
            // 
            // dbo_Trigger_Reviews_Del_PreventTest_PosttestAction
            // 
            dbo_Trigger_Reviews_Del_PreventTest_PosttestAction.Conditions.Add(rowCountCondition2);
            dbo_Trigger_Reviews_Del_PreventTest_PosttestAction.Conditions.Add(scalarValueCondition2);
            resources.ApplyResources(dbo_Trigger_Reviews_Del_PreventTest_PosttestAction, "dbo_Trigger_Reviews_Del_PreventTest_PosttestAction");
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
            // dbo_Trigger_Reviews_InsertTest_One_PosttestAction
            // 
            resources.ApplyResources(dbo_Trigger_Reviews_InsertTest_One_PosttestAction, "dbo_Trigger_Reviews_InsertTest_One_PosttestAction");
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
            // dbo_Trigger_Reviews_InsertTest_TwoData
            // 
            this.dbo_Trigger_Reviews_InsertTest_TwoData.PosttestAction = dbo_Trigger_Reviews_InsertTest_Two_PosttestAction;
            this.dbo_Trigger_Reviews_InsertTest_TwoData.PretestAction = null;
            this.dbo_Trigger_Reviews_InsertTest_TwoData.TestAction = dbo_Trigger_Reviews_InsertTest_Two_TestAction;
            // 
            // dbo_Trigger_Reviews_InsertTest_Two_TestAction
            // 
            dbo_Trigger_Reviews_InsertTest_Two_TestAction.Conditions.Add(rowCountCondition4);
            dbo_Trigger_Reviews_InsertTest_Two_TestAction.Conditions.Add(scalarValueCondition4);
            resources.ApplyResources(dbo_Trigger_Reviews_InsertTest_Two_TestAction, "dbo_Trigger_Reviews_InsertTest_Two_TestAction");
            // 
            // dbo_Trigger_Reviews_InsertTest_Two_PosttestAction
            // 
            resources.ApplyResources(dbo_Trigger_Reviews_InsertTest_Two_PosttestAction, "dbo_Trigger_Reviews_InsertTest_Two_PosttestAction");
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
            // TbReviews
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
        public void dbo_Trigger_Reviews_Del_PreventTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_Trigger_Reviews_Del_PreventTestData;
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
        public void dbo_Trigger_Reviews_InsertTest_One()
        {
            SqlDatabaseTestActions testActions = this.dbo_Trigger_Reviews_InsertTest_OneData;
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
        public void dbo_Trigger_Reviews_InsertTest_Two()
        {
            SqlDatabaseTestActions testActions = this.dbo_Trigger_Reviews_InsertTest_TwoData;
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

        private SqlDatabaseTestActions dbo_Trigger_Reviews_Del_PreventTestData;
        private SqlDatabaseTestActions dbo_Trigger_Reviews_InsertTest_OneData;
        private SqlDatabaseTestActions dbo_Trigger_Reviews_InsertTest_TwoData;
    }
}
