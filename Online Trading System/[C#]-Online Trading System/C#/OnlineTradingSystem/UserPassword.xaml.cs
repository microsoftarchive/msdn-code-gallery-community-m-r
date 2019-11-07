using System;
using System.Windows;

namespace OnlineTradingSystem
{
    /// <summary>
    /// Interaction logic for UserPassword.xaml
    /// </summary>
    public partial class UserPassword : Window
    {
        MainWindow Datastore;
        Window1 Super=new Window1();

        public UserPassword()
        {
            InitializeComponent();

           
        }

        public UserPassword(Window1 super)
        {
            InitializeComponent();
            Super = super;

        } 
        //Data member
       internal  Store regis = new Store();

      

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(regis.Getmarketname);
        }

     

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameBox.Text;
            string password = passwordBox.Password;
            regis = new Store(password, username);

            DAL dal = new DAL();
            regis = dal.ValidateUsernamePasswordCompatible(regis);
            Datastore = new MainWindow(regis);
             
            if (regis.Getstorename != null)
            {
                //close this window and  open the datastore window

                Super.Close();
                this.Close();
                
                Datastore.Show();
       
            }
            else if (regis.Getstorename == null)
            {

                textWrongValidate.Text = "Wrong Validate";
            }
           

        }

        private void usernameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            textWrongValidate.Text = "";
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            

        }




 

	 

       

        
 
    }
}
