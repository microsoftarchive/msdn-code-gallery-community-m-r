using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
/// Author      : Shanu
/// Create date : 2014-11-11
/// Description : Shanu Nested Datagrid View // In this form i have created a Master Details relation for Datagridview
/// Latest
/// Modifier    : Shanu
/// Modify date : 2014-11-11
/// </summary>

namespace ShanuNestedDataGridView
{
    public partial class frmMasterDetailGrid : Form
    {
        #region Variables
        // Declared for the Master grid
        DataGridView Master_shanuDGV = new DataGridView();
        // Declared for the Detail grid
        DataGridView Detail_shanuDGV = new DataGridView();

        List<int> lstNumericTextBoxColumns;

        Helper.ShanuDGVHelper objshanudgvHelper = new Helper.ShanuDGVHelper();
        public int ColumnIndex;
        DataTable dtName = new DataTable();
        # endregion

        public frmMasterDetailGrid()
        {
            InitializeComponent();
        }
        #region Form Load
        private void frmMasterDetailGrid_Load(object sender, EventArgs e)
        {
            // To bind the Master data to List 
            Master_BindData();

            // To bind the Detail data to List 
            Detail_BindData();


          MasterGrid_Initialize();

            DetailGrid_Initialize();
            
        }
        # endregion


            #region Methods
        private void Master_BindData()
        {
            DataClass.OrderMasterBindClass obj1 = new DataClass.OrderMasterBindClass("", "Order_001", "Table1", "Total 4 ppl per table", DateTime.Now, "Shanu");
            DataClass.OrderMasterBindClass obj2 = new DataClass.OrderMasterBindClass("", "Order_002", "Table2", "Urgent-Need to be served with in 5 min", DateTime.Now, "Shanu");
            DataClass.OrderMasterBindClass obj3 = new DataClass.OrderMasterBindClass("", "Order_003", "Table3", "Need all More Spicy", DateTime.Now, "Shanu");
            DataClass.OrderMasterBindClass obj4 = new DataClass.OrderMasterBindClass("", "Order_004", "Table4", "Add Little Spicy to all Items", DateTime.Now, "Shanu");
            DataClass.OrderMasterBindClass obj5 = new DataClass.OrderMasterBindClass("", "Order_005", "Table5", "Total 3 ppl at this table", DateTime.Now, "Shanu");
            DataClass.OrderMasterBindClass obj6 = new DataClass.OrderMasterBindClass("", "Order_006", "Table5", "Hot and Spicy", DateTime.Now, "Shanu");

            DataClass.OrderMasterBindClass.objMasterDGVBind.Add(obj1);
            DataClass.OrderMasterBindClass.objMasterDGVBind.Add(obj2);
            DataClass.OrderMasterBindClass.objMasterDGVBind.Add(obj3);
            DataClass.OrderMasterBindClass.objMasterDGVBind.Add(obj4);
            DataClass.OrderMasterBindClass.objMasterDGVBind.Add(obj5);
            DataClass.OrderMasterBindClass.objMasterDGVBind.Add(obj6);
          
        }

        private void Detail_BindData()
        {
            DataClass.OrderDetailBindClass obj1 = new DataClass.OrderDetailBindClass("Ord_dtl_001", "Order_001", "Burger Set", "With double chees", 150, 4);
            DataClass.OrderDetailBindClass obj2 = new DataClass.OrderDetailBindClass("Ord_dtl_002", "Order_001", "Chicken Fry", "Spicy", 120, 2);
            DataClass.OrderDetailBindClass obj3 = new DataClass.OrderDetailBindClass("Ord_dtl_003", "Order_001", "Fruit Salad", "WithIce cream", 75, 2);

            DataClass.OrderDetailBindClass obj4 = new DataClass.OrderDetailBindClass("Ord_dtl_004", "Order_002", "Bibimbap", "Hot", 450, 2);
            DataClass.OrderDetailBindClass obj5 = new DataClass.OrderDetailBindClass("Ord_dtl_005", "Order_002", "Sundubu", "Spicy", 390, 1);

            DataClass.OrderDetailBindClass obj6 = new DataClass.OrderDetailBindClass("Ord_dtl_006", "Order_003", "Pizza", "Hot and served fast", 235, 1);

            DataClass.OrderDetailBindClass obj7 = new DataClass.OrderDetailBindClass("Ord_dtl_007", "Order_005", "Kimchi jjigae", "Spicy", 650, 4);
            DataClass.OrderDetailBindClass obj8 = new DataClass.OrderDetailBindClass("Ord_dtl_008", "Order_005", "Chicken Fry", "Spicy", 120, 2);
            DataClass.OrderDetailBindClass obj9 = new DataClass.OrderDetailBindClass("Ord_dtl_009", "Order_005", "Fruit Salad", "WithIce cream", 75, 2);

            DataClass.OrderDetailBindClass obj10 = new DataClass.OrderDetailBindClass("Ord_dtl_010", "Order_006", "chicken kebab", "Spicy", 250, 3);
            DataClass.OrderDetailBindClass obj11 = new DataClass.OrderDetailBindClass("Ord_dtl_011", "Order_006", "Lamb kebab", "Spicy", 300, 2);


            DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj1);
            DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj2);
            DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj3);
            DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj4);
            DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj5);
            DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj6);
            DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj7);
            DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj8);
            DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj9);
            DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj10);
            DataClass.OrderDetailBindClass.objDetailDGVBind.Add(obj11);

          
        }

        // to generate Master Datagridview with your coding
        public void MasterGrid_Initialize()
        {

            //First generate the grid Layout Design
            Helper.ShanuDGVHelper.Layouts(Master_shanuDGV, Color.LightSteelBlue, Color.AliceBlue, Color.LightSkyBlue, false, Color.SteelBlue, false, false, false, Color.White, 30);

            //Set Height,width and add panel to your selected control
            Helper.ShanuDGVHelper.Generategrid(Master_shanuDGV, pnlShanuGrid, 1000, 600, 10, 10);


            // Color Image Column creation
            Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.ImageColumn, "img", "", "", true, 26, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleRight, Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, "Order_No", "Order NO", "Order NO", true, 90, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, "Table_ID", "Table ID", "Table ID", true, 80, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, "Description", "Description", "Description", true, 320, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, "Order_DATE", "Order DATE", "Order DATE", true, 140, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Master_shanuDGV, ShanuControlTypes.BoundColumn, "Waiter_ID", "Waiter_ID", "Waiter_ID", true, 120, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            //Convert the List to DataTable
            DataTable detailTableList = ListtoDataTable(DataClass.OrderDetailBindClass.objDetailDGVBind);

            // Image Colum Click Event - In  this method we create an event for cell click and we will display the Detail grid with result.

            objshanudgvHelper.DGVMasterGridClickEvents(Master_shanuDGV, Detail_shanuDGV, Master_shanuDGV.Columns["img"].Index, ShanuEventTypes.cellContentClick, ShanuControlTypes.ImageColumn, detailTableList, "Order_No");
            
            // Bind data to DGV.
            Master_shanuDGV.DataSource = DataClass.OrderMasterBindClass.objMasterDGVBind;



        }
        //List to Data Table Convert
        private static DataTable ListtoDataTable<T>(IEnumerable<T> DetailList)
        {
            Type type = typeof(T);
            var typeproperties = type.GetProperties();      

            DataTable listToDT = new DataTable();
            foreach (PropertyInfo propInfo in typeproperties)
            {
                listToDT.Columns.Add(new DataColumn(propInfo.Name, propInfo.PropertyType));
            }

            foreach (T ListItem in DetailList)
            {
                object[] values = new object[typeproperties.Length];
                for (int i = 0; i < typeproperties.Length; i++)
                {
                    values[i] = typeproperties[i].GetValue(ListItem, null);
                }

                listToDT.Rows.Add(values);
            }

            return listToDT;
        }

        
        // to generate Detail Datagridview with your coding
        public void DetailGrid_Initialize()
        {

            //First generate the grid Layout Design
            Helper.ShanuDGVHelper.Layouts(Detail_shanuDGV, Color.SkyBlue, Color.AliceBlue, Color.LavenderBlush, false, Color.LightBlue, false, false, false, Color.Black, 30);

            //Set Height,width and add panel to your selected control
           Helper.ShanuDGVHelper.Generategrid(Detail_shanuDGV, pnlShanuGrid, 800, 200, 10, 10);

            // Color Dialog Column creation
            Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, "Order_Detail_No", "Detail No", "Order Detail No", true, 90, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleRight, Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, "Order_No", "Order NO", "Order NO", true, 80, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, "Item_Name", "Item_Name", "Item_Name", true,160, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, "Notes", "Notes", "Notes", true, 260, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, "Price", "Price", "Price", true, 70, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleRight, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            // BoundColumn creation
            Helper.ShanuDGVHelper.Templatecolumn(Detail_shanuDGV, ShanuControlTypes.BoundColumn, "QTY", "QTY", "QTY", true, 40, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleRight, DataGridViewContentAlignment.MiddleCenter, Color.Transparent, null, "", "", Color.Black);


            objshanudgvHelper.DGVDetailGridClickEvents(Detail_shanuDGV);
            

        }
            # endregion
    }
}
