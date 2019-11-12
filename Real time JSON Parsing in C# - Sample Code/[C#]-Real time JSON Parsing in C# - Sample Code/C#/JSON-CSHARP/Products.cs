using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_CSHARP
{
    #region For Fetching Products and Inventories - Completed by Sandeep Thakur
    public class Products
    {
        public string available_on_hand { get; set; }
        public string is_kit_parent { get; set; }
        public string weight { get; set; }
        public List<kit_components> kit_components { get; set; }
        public string total_on_hand { get; set; }
        public string cost { get; set; }
        public string id { get; set; }
        public string sku { get; set; }
        public List<cart> carts { get; set; }
        public List<warehouse> warehouses { get; set; }
        public string company_id { get; set; }
        public string _link { get; set; }
        public string type { get; set; }
        public string category { get; set; }
        public string updated { get; set; }
        public string price { get; set; }
        public string taxable { get; set; }
        public string archive_date { get; set; }
        public string bigc_variant_migrated { get; set; }
        public string name { get; set; }
        public string customer_text { get; set; }
        public List<supplier> suppliers { get; set; }
        public string created { get; set; }
        public string to_be_shipped { get; set; }
        public string on_pos { get; set; }
    }

    public class supplier
    {
        public string supplr { get; set; }
    }

    public class kit_components
    {
        public string kits { get; set; }
    }

    public class warehouse
    {
        public string updated { get; set; }
        public string always_dropship { get; set; }
        public string is_configured_for_shipping { get; set; }
        public string created { get; set; }
        public string out_of_stock_threshold { get; set; }
        public string cartw { get; set; }
        public string is_default_location { get; set; }
        public string low_stock_threshold { get; set; }
        public List<oAddress> addreses { get; set; }
        public string on_hand { get; set; }
        public string location_in_warehouse { get; set; }
        public string id { get; set; }
    }
    public class cart
    {
        public string inventory_changed { get; set; }
        public string is_demo { get; set; }
        public string sync { get; set; }
        public string max_export_qty { get; set; }
        public string _link { get; set; }
        public string vendor_display { get; set; }
        public string worker_machinga { get; set; }
        public string vendor { get; set; }
        public string id { get; set; }
        public List<extra_info> extra_infos { get; set; }
        public audit audit { get; set; }
        public string name { get; set; }
        public string variant_sku { get; set; }
        public string original_sku { get; set; }

    }

    public class extra_info
    {
        public string info { get; set; }
    }

    public class audit
    {
        public first_import first_import { get; set; }
        public latest_export latest_export { get; set; }
        public latest_import latest_import { get; set; }
        public first_export first_export { get; set; }
    }

    public class first_export
    {
        public string message { get; set; }
        public string created { get; set; }
        public string success { get; set; }
        public string task_id { get; set; }
        public string quantity { get; set; }
    }

    public class latest_import
    {
        public string created { get; set; }
        public string task_id { get; set; }
        public string quantity { get; set; }
    }

    public class latest_export
    {
        public string message { get; set; }
        public string created { get; set; }
        public string success { get; set; }
        public string task_id { get; set; }
        public string quantity { get; set; }

    }

    public class first_import
    {
        public string created { get; set; }
        public string task_id { get; set; }
        public string quantity { get; set; }
    }
    #endregion
}
