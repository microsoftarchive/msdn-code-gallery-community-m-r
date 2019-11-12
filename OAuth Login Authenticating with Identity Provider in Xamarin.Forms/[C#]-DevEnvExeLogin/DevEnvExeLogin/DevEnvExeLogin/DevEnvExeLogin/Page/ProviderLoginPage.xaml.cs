
using Xamarin.Forms;

namespace DevEnvExeLogin
{
    public partial class ProviderLoginPage : ContentPage
    {
        //we will refer providename from renderer page
        public string  ProviderName { get; set; }
        public ProviderLoginPage(string _providername)
        {
            InitializeComponent();
            ProviderName = _providername;
        }
    }
}
