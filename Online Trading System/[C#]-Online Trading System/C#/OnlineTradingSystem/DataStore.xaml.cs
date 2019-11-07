using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;








namespace OnlineTradingSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        

        #region constructor
        public MainWindow(Store _ur)
        {
            InitializeComponent();
            regis = _ur;
            NameofStore.Text = regis.Getstorename;
        }
        public MainWindow()
        {
            InitializeComponent();

        } 
        #endregion

        #region Instance

        private Store regis;

     

        private Canvas piCanv;

        private ImageBrush ib;

        List<Border> bord = new List<Border>();
        
        #endregion

        // ********* Product ************
        #region Creat Product List

        private List<Product> products;
        private List<Product> LoadCollectionData()
        {
            products = new List<Product>();
            products.Add(new Product());                      
            return products;
        }
        
        #endregion
        
        #region Add Product

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
             
            // Add item to the data tabel
            if (products.Count==0)
            {

            products.Add(new Product());
            }
            else
            {

             

                 
                products = products[products.Count-1].AddList(products,numbersID);
            }
            dataGrid1.Items.Refresh();



            Product proud = products[products.Count - 1];




            Grid gridborder = new Grid()
            {
                Width = 80,
                Height = 150

            };


            piCanv = new Canvas()
           {
               Name = "c" + products[products.Count - 1].ID.ToString(),
               ToolTip = "Click here to add Image",
               Width = 75,
               Height = 120,
               Margin = new Thickness(1, 40, 1, 1),
               Background = new SolidColorBrush(Colors.LightCyan),

           };



            Label labelproduct = new Label()
            {
                Content = products[products.Count - 1].ID + "\n " + products[products.Count - 1].ProductName
            };



            bord.Add(new Border()
           {
               Name = "b" + products[products.Count - 1].ID.ToString(),
               Width = 100,
               Height = 200,
               CornerRadius = new CornerRadius(15),
               BorderThickness = new Thickness(5, 10, 15, 20),
               Margin = new Thickness(15, 5, 5, 5),
               BorderBrush = Brushes.SlateBlue,
               Child = gridborder,
               Style = Resources["borderStyle"] as Style


           });


            bord[bord.Count - 1].MouseRightButtonDown += RemoveItem_Click;

            piCanv.MouseLeftButtonDown += piCanv_MouseLeftButtonDown;

            gridborder.Children.Add(piCanv);
            gridborder.Children.Add(labelproduct);

            Warp.Children.Add(bord[bord.Count - 1]);


        }

        
        #endregion

        #region Remove Product

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (RemoveItem.IsChecked == true)
            {

                Border item = sender as Border;

                try
                {

                    Warp.Children.Remove(item);
                    int index = dataGrid1.SelectedIndex;
                    var indx = dataGrid1.SelectedItems;
                    if (indx.Count == 0)
                    {
                       
                        string bordName = item.Name;
                        int num = Convert.ToInt32(bordName.Remove(0, 1));
                        Product pid = products.Where<Product>(X => X.ID == num).Single<Product>();
                        index = products.IndexOf(pid);
                        products.RemoveRange(index, 1);
                    
                    }
                    else if (indx.Count != 0)
                    {
                        products.RemoveRange(index, indx.Count);
                        bord.RemoveRange(index, indx.Count);
                        Warp.Children.RemoveRange(index, indx.Count);
                    }
                }

                catch
                {
                    return;
                }

                finally
                {

                    dataGrid1.Items.Refresh();
                }

            }


        }

        #endregion

        // ******************************
        #region Open File image
        private void Open_Image()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Title = "Select Photos";

            openFileDialog1.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;



            Stream myStream = null;



            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            Imgeloc = openFileDialog1.FileName.ToString();
                            string file = openFileDialog1.FileName;
                            ib = new ImageBrush();
                            ib.ImageSource = new BitmapImage(new Uri(file, UriKind.Relative));
                            piCanv.Background = ib;
                            piCanv.Children.Clear();

                            FileStream fs = new FileStream(Imgeloc, FileMode.Open, FileAccess.Read);
                            BinaryReader br = new BinaryReader(fs);
                           
                            //check wher is the picture location
                            string CanvasName = piCanv.Name;
                            int num = Convert.ToInt32(CanvasName.Remove(0, 1));
                            Product pid = products.Where<Product>(X => X.ID == num).Single<Product>();
                            int index = products.IndexOf(pid);
                            ///////////////////////////////

                            products[index].img = br.ReadBytes((int)fs.Length);
                            Saveimg(products,index);
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }


        private void piCanv_MouseLeftButtonDown(object sender, EventArgs e)
        {

            piCanv = sender as Canvas;
            
            Open_Image();

        }


        #endregion 

     
        #region Creat Border List

        private List<Border> GiveMeBorde()
        {
            bord = new List<Border>();
            return bord;

        } 
        #endregion

        //************ Refrese Events *********** 

        #region Refrese Grid

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {

            dataGrid1.Items.Refresh();


        } 
        #endregion

        #region Refrase Table

        private void RefraseTable()
        {

            dataGrid1.Items.Refresh();

        } 
        #endregion

        //*********************** data Grid ***************
        #region Data Grid Events

        private void dataGrid1_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

            var indx = dataGrid1.SelectedIndex;


            products[indx].DateUpdate = DateTime.Now;
            
           
        }


        private void addcolum_Click(object sender, RoutedEventArgs e)
        {
            string ColumeName = Prompt.ShowDialog();

            if (string.IsNullOrWhiteSpace(ColumeName) == false)
            {
                dynamic product = new Product();
                DataGridTextColumn textColumn = new DataGridTextColumn();
                textColumn.Header = ColumeName;

                // textColumn.Binding = new Binding("FirstName");
                dataGrid1.Columns.Add(textColumn);

            }
        }

        private void dataGrid1_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid1.Items.Refresh();
        }


        private void dataGrid1_Sorting(object sender, DataGridSortingEventArgs e)
        {

            //var b = dataGrid1.ItemsSource;

            //List<Product> product = (List<Product>)b;


        }
        
        #endregion

        //********************** canvas ******************

        #region Canvas Events

        private void Mycanvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            piCanv = sender as Canvas;
            Open_Image();
        }
        
        #endregion

        //  **************** Sql Section *****************************//
        //********************************************//
        #region  build Sql conection
        
        SqlConnection sc = new SqlConnection("Data Source=HASEE-0E7936831;Initial Catalog=Online Trading System DB;Integrated Security=True");
       

        #endregion

        #region Exsport Data Table To Sql Server

        private void Export_data_Click(object sender, RoutedEventArgs e)
        {
            DAL sqlmetode = new DAL();



            sqlmetode.ExportSqlData(products,regis);


            Export_data.Click += new RoutedEventHandler(Window_Activated);


        }


        #endregion

        #region Import Data table To GrideData
        List<Int32> numbersID = new List<Int32>();

        private void Import_data_Click(object sender, RoutedEventArgs e)
        {
          
            List<Int32> numbersdataGirdId = new List<Int32>();
           
            products=new List<Product>();
           
            DAL sqldata=new DAL();
           
            products = sqldata.ImportSqlData(regis);

            sqldata.ImportStoreName();
            numbersID = sqldata.ImportListNumberId();
            Warp.Children.Clear();

            ShowAllTheImageProducts();

            dataGrid1.ItemsSource = products;
            dataGrid1.Items.Refresh();



        }

        #endregion

        #region Window Loadaed

        DAL sqldata = new DAL();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dataGrid1.ItemsSource = LoadCollectionData();
            //products = new List<Product>();
            products = LoadCollectionData();
            //dataGrid1.ItemsSource = products;

            
            //UserPassword usrp = new UserPassword();
            //List<Product> FillProduct = new List<Product>();
            //FillProduct = sqldata.ImportSqlData(regis);
            //if (FillProduct.Count != 0)
            //{
            //    products = FillProduct;

            //}

           Import_data_Click(sender,e);

            
            dataGrid1.ItemsSource = products;
            dataGrid1.Items.Refresh();

        }


        private void ShowAllTheImageProducts()
        {

            for (int i = 0; i < products.Count; i++)
            {

                Buid_border_Pic(i);


            };

        }
        #endregion

        #region Save image in the canvas
        private void Buid_border_Pic(int i)
        {

            Grid gridborder = new Grid()
            {
                Width = 80,
                Height = 150

            };


            piCanv = new Canvas()
            {
                Name = "c" + products[i].ID.ToString(),
                ToolTip = "Click here to add Image",
                Width = 75,
                Height = 120,
                Margin = new Thickness(1, 40, 1, 1),
               
                
                Background = new SolidColorBrush(Colors.LightCyan)
                
            };

            if (products[i].img.Length>1000)
            {

                byte[] imageBytes = products[i].img;
                 
                MemoryStream stream = new MemoryStream(imageBytes);
                stream.Write(imageBytes, 0, imageBytes.Length);
                stream.Position = 0;
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                BitmapImage BitObj = new BitmapImage();
                BitObj.BeginInit();

                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                BitObj.StreamSource = ms;
                BitObj.EndInit();

                ib = new ImageBrush();
                ib.ImageSource = BitObj;
                piCanv.Background = ib;
            }
                
                


                
            
            
            


            Label labelproduct = new Label()
            {
                Content = products[i].ID + "\n " + products[i].ProductName
            };




            bord.Add(new Border()
            {
                Name = "m" + products[i].ID.ToString(),
                Width = 100,
                Height = 200,
                CornerRadius = new CornerRadius(15),
                BorderThickness = new Thickness(5, 10, 15, 20),
                Margin = new Thickness(15, 5, 5, 5),
                BorderBrush = Brushes.SlateBlue,
                Child = gridborder,
                Style = Resources["borderStyle"] as Style


            });


            bord[bord.Count - 1].MouseRightButtonDown += RemoveItem_Click;

            piCanv.MouseLeftButtonDown += piCanv_MouseLeftButtonDown;

            gridborder.Children.Add(piCanv);
            gridborder.Children.Add(labelproduct);

            Warp.Children.Add(bord[bord.Count - 1]);
        
        
        
        
        
        
        }
        #endregion
        //************************* Animation ***********************


        #region Window_Activate -Start animation

        private void Window_Activated(object sender, EventArgs e)
        {

            var winHigth = this.Height;
            var winWidth = this.Width;


            var top = Canvas.GetTop(image1);
            var left = Canvas.GetLeft(image1);

            Storyboard sb = new Storyboard();
            foreach (var ab in MoveTo(image1, left, top, winWidth - 300, 0))
                sb.Children.Add(ab);

            foreach (var ab in MoveTo(image1, 0, winWidth - 300, 3, 3))
            {
                ab.BeginTime = TimeSpan.FromSeconds(5);
                sb.Children.Add(ab);
            }

            (image1).BeginStoryboard(sb);




        }

        public static void MoveTo(Image target, double newX, double newY)
        {

            var top = Canvas.GetTop(target);
            var left = Canvas.GetLeft(target);
            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation(top, newY - top, TimeSpan.FromSeconds(2));
            DoubleAnimation anim2 = new DoubleAnimation(left, newX - left, TimeSpan.FromSeconds(2));
            trans.BeginAnimation(TranslateTransform.XProperty, anim1);
            trans.BeginAnimation(TranslateTransform.YProperty, anim2);

        }

        static IEnumerable<DoubleAnimation> MoveTo(Image target, double origX, double origY, double newX, double newY)
        {

            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;
            var a = new DoubleAnimation(origX, newY, TimeSpan.FromSeconds(2));
            Storyboard.SetTargetProperty(a, new PropertyPath("(Canvas.Top)"));
            var b = new DoubleAnimation(origY, newX, TimeSpan.FromSeconds(2));
            Storyboard.SetTargetProperty(b, new PropertyPath("(Canvas.Left)"));

            yield return a;
            yield return b;
        }





        
        #endregion
     
 
        //******************* Show your web Store **************

        #region Show Web Page


        private void MyWebrowser_Loaded(object sender, RoutedEventArgs e)
        {
            MyWebrowser.Navigate(new Uri("http://localhost:1822"));
        }



        
        #endregion



        #region Back To The Main Window
        private void BackMainWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Are you sure?

            this.Hide();
            Window1 super = new Window1();
            super.Show();



        } 
        #endregion
        string Imgeloc="";

        #region  Save Image To Sql 
        private void Saveimg(List<Product> products, int index)
        {

            //FileStream fs = new FileStream(Imgeloc[Imgeloc.Count], FileMode.Open, FileAccess.Read);
            //BinaryReader br = new BinaryReader(fs);
            //products[products.Count].img = br.ReadBytes((int)fs.Length);

            DAL sqlCom = new DAL();
            sqlCom.SaveImageToSql(products, index);


        } 
        #endregion

        #region Window Closed
        private void Window_Closed(object sender, EventArgs e)
        {

            Environment.Exit(0);
        } 
        #endregion

        #region Product Sales Click
        private void Product_Sales_Click(object sender, RoutedEventArgs e)
        {
            DAL dal = new DAL();



            DataTable dt = new DataTable();
            dt = dal.Product_Sales_by_Store_Id(regis);
            dataGrid1.ItemsSource = dt.AsDataView();


        } 
        #endregion

        #region Show Custumer List

        private void Custumer_List_click(object sender, RoutedEventArgs e)
        {
            DAL dal = new DAL();



            DataTable dt = new DataTable();
            dt = dal.Customer_List_By_Store_Id(regis);
            dataGrid1.ItemsSource = dt.AsDataView();



        }
        #endregion






    }




        #region Enter new Colume


    public static class Prompt
    {
        public static string ShowDialog()
        {
            System.Windows.Forms.Form prompt = new System.Windows.Forms.Form();
            prompt.Width = 200;
            prompt.Height = 150;
            prompt.Left = 0;
            prompt.Text = "New Column";
            System.Windows.Forms.Label textLabel = new System.Windows.Forms.Label() { Left = 50, Top = 20, Text = "Enter new Colume" };
            System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox() { Left = 50, Top = 50, Width = 100 };
            System.Windows.Forms.Button confirmation = new System.Windows.Forms.Button() { Text = "Ok", Left = 90, Width = 50, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.ShowDialog();
            return textBox.Text;
        }
    }

    
    #endregion
   
   

}

    









       
 
