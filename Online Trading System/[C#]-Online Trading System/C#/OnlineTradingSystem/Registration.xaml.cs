using System.Windows;


namespace OnlineTradingSystem
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        #region Register 
        public void Register_Click(object sender, RoutedEventArgs e)
        {
            Store regi = new Store()

            {

                Getpassword = passwordBoxText_Edit.Password,
                Getusername = UserNameText_Edit.Text,
                Getmarketname = MarketNameText_Edit.Text,
                Getstorename = StoreNameText_Edit.Text

            };

            DAL dal = new DAL();

            /* check if no one is register in the same name 
             * if the name is empty return ok 
             * if the name is already busy return false
            */
            bool ok = dal.RegistrationData(regi);

            if (ok == true)
            {

                this.Close();
            }







        } 
        #endregion
    }
}
