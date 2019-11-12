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
    public class TbBrands : SqlDatabaseTestClass
    {

        public TbBrands()
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
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Brands_Del_PreventTest_ById_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TbBrands));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Brands_Del_Prevent_ByName_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Brands_Del_Prevent_ByName_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_Trigger_Brands_Del_PreventTest_ById_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition3;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition3;
            this.dbo_Trigger_Brands_Del_PreventTest_ByIdData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_Trigger_Brands_Del_Prevent_ByNameData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            dbo_Trigger_Brands_Del_PreventTest_ById_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_Trigger_Brands_Del_Prevent_ByName_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_Trigger_Brands_Del_Prevent_ByName_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            scalarValueCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_Trigger_Brands_Del_PreventTest_ById_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            scalarValueCondition3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            // 
            // dbo_Trigger_Brands_Del_PreventTest_ById_TestAction
            // 
            resources.ApplyResources(dbo_Trigger_Brands_Del_PreventTest_ById_TestAction, "dbo_Trigger_Brands_Del_PreventTest_ById_TestAction");
            // 
            // dbo_Trigger_Brands_Del_Prevent_ByName_TestAction
            // 
            resources.ApplyResources(dbo_Trigger_Brands_Del_Prevent_ByName_TestAction, "dbo_Trigger_Brands_Del_Prevent_ByName_TestAction");
            // 
            // dbo_Trigger_Brands_Del_Prevent_ByName_PosttestAction
            // 
            dbo_Trigger_Brands_Del_Prevent_ByName_PosttestAction.Conditions.Add(rowCountCondition2);
            dbo_Trigger_Brands_Del_Prevent_ByName_PosttestAction.Conditions.Add(scalarValueCondition2);
            resources.ApplyResources(dbo_Trigger_Brands_Del_Prevent_ByName_PosttestAction, "dbo_Trigger_Brands_Del_Prevent_ByName_PosttestAction");
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
            scalarValueCondition2.ExpectedValue = "G3";
            scalarValueCondition2.Name = "scalarValueCondition2";
            scalarValueCondition2.NullExpected = false;
            scalarValueCondition2.ResultSet = 1;
            scalarValueCondition2.RowNumber = 1;
            // 
            // dbo_Trigger_Brands_Del_PreventTest_ById_PosttestAction
            // 
            dbo_Trigger_Brands_Del_PreventTest_ById_PosttestAction.Conditions.Add(rowCountCondition3);
            dbo_Trigger_Brands_Del_PreventTest_ById_PosttestAction.Conditions.Add(scalarValueCondition3);
            resources.ApplyResources(dbo_Trigger_Brands_Del_PreventTest_ById_PosttestAction, "dbo_Trigger_Brands_Del_PreventTest_ById_PosttestAction");
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
            // dbo_Trigger_Brands_Del_PreventTest_ByIdData
            // 
            this.dbo_Trigger_Brands_Del_PreventTest_ByIdData.PosttestAction = dbo_Trigger_Brands_Del_PreventTest_ById_PosttestAction;
            this.dbo_Trigger_Brands_Del_PreventTest_ByIdData.PretestAction = null;
            this.dbo_Trigger_Brands_Del_PreventTest_ByIdData.TestAction = dbo_Trigger_Brands_Del_PreventTest_ById_TestAction;
            // 
            // dbo_Trigger_Brands_Del_Prevent_ByNameData
            // 
            this.dbo_Trigger_Brands_Del_Prevent_ByNameData.PosttestAction = dbo_Trigger_Brands_Del_Prevent_ByName_PosttestAction;
            this.dbo_Trigger_Brands_Del_Prevent_ByNameData.PretestAction = null;
            this.dbo_Trigger_Brands_Del_Prevent_ByNameData.TestAction = dbo_Trigger_Brands_Del_Prevent_ByName_TestAction;
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
        public void dbo_Trigger_Brands_Del_PreventTest_ById()
        {
            SqlDatabaseTestActions testActions = this.dbo_Trigger_Brands_Del_PreventTest_ByIdData;
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
        public void dbo_Trigger_Brands_Del_Prevent_ByName()
        {
            SqlDatabaseTestActions testActions = this.dbo_Trigger_Brands_Del_Prevent_ByNameData;
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

        private SqlDatabaseTestActions dbo_Trigger_Brands_Del_PreventTest_ByIdData;
        private SqlDatabaseTestActions dbo_Trigger_Brands_Del_Prevent_ByNameData;
    }
}
