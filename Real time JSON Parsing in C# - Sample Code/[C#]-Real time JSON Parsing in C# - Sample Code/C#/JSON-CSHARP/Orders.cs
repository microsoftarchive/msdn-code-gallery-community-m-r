using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_CSHARP
{
    #region For Fetching Orders - Completed by Sandeep Thakur
    public class Orders
    {
        public List<Shipment> shipments { get; set; }
        public string notes_from_customer { get; set; }
        public string shippability { get; set; }
        public string shipping_amount { get; set; }
        public string shipping_type { get; set; }
        public List<string> commments { get; set; }
        public string company_id { get; set; }
        public string _link { get; set; }
        public string credit_card_issuer { get; set; }
        public string status { get; set; }
        public string updated { get; set; }
        public oAddress billing_address { get; set; }
        public List<string> tags { get; set; }
        public string order_id { get; set; }
        public string product_amount { get; set; }
        public string grand_total { get; set; }
        public string cart { get; set; }
        public string order_date { get; set; }
        public string cart_order_id { get; set; }
        public string cart_shipment_id { get; set; }
        public string discount_amount { get; set; }
        public string tax_amount { get; set; }
        public string created { get; set; }
        public List<Lines> lines { get; set; }
        public string cart_name { get; set; }
        public oAddress shipping_address { get; set; }



    }

    public class Shipment
    {
        public audit_shipment auditshipment { get; set; }
        public string notes_from_customer { get; set; }
        public string weight { get; set; }
        //public string return_tracking { get; set; }
        public string _linkshipmentcreatedtaskstatus { get; set; }
        public string assigned_to_id { get; set; }
        //public string comments { get; set; }
        public string _linkreturnlabelrate { get; set; }
        public oAddress shipto { get; set; }
        public string notify_cart_success { get; set; }
        public string _linkgeneratelabel { get; set; }
        public string requested_shipping_method { get; set; }
        public string company_id { get; set; }
        public string _link { get; set; }
        public string cart_vendor { get; set; }
        public List<audit_label> audit_label { get; set; }
        public string notify_cart { get; set; }
        public oAddress shipfrom { get; set; }
        public string status { get; set; }
        public string updated { get; set; }
        //public string tags { get; set; }
        public string order_id { get; set; }
        public string _linklabelrate { get; set; }
        public string cart { get; set; }
        public string ship_date { get; set; }
        //public string preset { get; set; }
        public string exported { get; set; }
        public string order_date { get; set; }
        public string _linkgeneratereturnlabel { get; set; }
        public string cart_order_id { get; set; }
        public tracking tracking { get; set; }
        public notify_cart_history notifycarthistory { get; set; }
        public string created { get; set; }
        //public List<string> lines { get; set; }
        public string shipped { get; set; }
        public string cart_notified_on { get; set; }
        public string added_by_id { get; set; }
        public oAddress assignedto { get; set; }
        public string shipment_id { get; set; }
    }

    public class assigned_to
    {
        public string assignedto { get; set; }
    }
    public class notify_cart_history
    {
        public string notifycarthistory { get; set; }
    }
    public class tracking
    {
        public string trackingg { get; set; }
    }

    public class audit_shipment
    {
        public List<string> tracking { get; set; }
        public List<string> sent { get; set; }
    }
    public class audit_label
    {
        public string updated { get; set; }
        public string success { get; set; }
        public string created { get; set; }
        public string error_message { get; set; }
        public string company_id { get; set; }
        public string _link { get; set; }
        public string id { get; set; }
        public string paramss { get; set; }
        public string tracking_id { get; set; }
        public string shipment_id { get; set; }
    }
    public class Lines
    {
        public product product { get; set; }
        public string item_price { get; set; }
        public List<tax_lines> tax_lines { get; set; }
        public shippability shippability { get; set; }
        public string total_price { get; set; }
        public string selected_option { get; set; }
        public string cart_orderitem_id { get; set; }
        public string manually_edited { get; set; }
        public string order_line_id { get; set; }
        public string option_price { get; set; }
        public string _link { get; set; }
        public string product_name { get; set; }
        public string quantity { get; set; }

    }
    public class tax_lines
    {
        public string tax { get; set; }
    }
    public class shippability
    {
        public string num_ordered { get; set; }
        public string supplier_id { get; set; }
        public string shippabilityStatus { get; set; }
        public string is_dropship { get; set; }
        public string num_shippable { get; set; }
        public string track_inventory { get; set; }
        public string on_hand { get; set; }
        public string num_shipped { get; set; }

    }
    public class product
    {
        public string sku { get; set; }
        public string category { get; set; }
        public string bigc_variant_migrated { get; set; }
        public string archive_date { get; set; }
        public string updated { get; set; }
        public string name { get; set; }
        public string weight { get; set; }
        public string customer_text { get; set; }
        public string price { get; set; }
        public string created { get; set; }
        public string company_id { get; set; }
        public string _link { get; set; }
        public string total_on_hand { get; set; }
        public string cost { get; set; }
        public string taxable { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string is_kit_parent { get; set; }
    }
    public class Root
    {
        public string count { get; set; }
    }
    public class oAddress
    {
        public string city { get; set; }
        public string fax { get; set; }
        public string _linkaddresscreatedtaskstatus { get; set; }
        public string name { get; set; }
        public string zip { get; set; }
        public string phone { get; set; }
        public string street1 { get; set; }
        public string company { get; set; }
        public string _link { get; set; }
        public string id { get; set; }
        public string reference_number { get; set; }
        public string email { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string cart_address_id { get; set; }
        public validation validation { get; set; }
        public string street2 { get; set; }

    }
    public class validation
    {
        public List<suggested> suggested { get; set; }
        public string additional_text { get; set; }
        public string is_error { get; set; }
    }
    public class suggested
    {
        public string city { get; set; }
        public string zip { get; set; }
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string is_commercial { get; set; }
        public string state { get; set; }
        public string country_code { get; set; }

    }
    #endregion

}
