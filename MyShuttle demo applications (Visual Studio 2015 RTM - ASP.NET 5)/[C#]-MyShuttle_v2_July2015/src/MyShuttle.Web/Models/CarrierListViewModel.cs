namespace MyShuttle.Web.Models
{
    using Model;
    using System.Collections.Generic;

    public class CarrierListViewModel
    {
        public CarrierListViewModel(List<Carrier> carriers)
        {
            CarrierList = carriers;
            Carrier = new CarrierViewModel();
            Search = new SearchViewModel();
        }
        
        public List<Carrier> CarrierList { get; set; }

        public CarrierViewModel Carrier { get; set; }

        public SearchViewModel Search { get; set; }

        public void LoadCarrierFrom(CarrierViewModel vm)
        {
            Carrier = vm;
        }
    }
}