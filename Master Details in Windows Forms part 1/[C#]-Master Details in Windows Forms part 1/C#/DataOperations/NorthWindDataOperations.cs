using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

/*
 * Roughed out class for part 2 and part 3 of this series
 */
namespace DataOperations
{
    public class NorthWindDataOperations
    {
        /// <summary>
        /// Container for all customers and orders
        /// </summary>
        public DataSet CustomerOrdersDataSet { get; set; }
        /// <summary>
        /// Container for returning order details
        /// </summary>
        public DataTable OrderDetailsTable { get; set; }
        /// <summary>
        /// Indicates if an exception was raised in the last operation
        /// </summary>
        public bool HasError { get; set; }
        /// <summary>
        /// Exception message raised in last operation which works with HasError property.
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Connection string to connect to our backend database
        /// </summary>
        public string ConnectionString { get; set; }
        public NorthWindDataOperations()
        {

        }
        /// <summary>
        /// Create new instance, specify connection string
        /// </summary>
        /// <param name="connectionString"></param>
        public NorthWindDataOperations(string connectionString)
        {
            ConnectionString = connectionString;
        }
        /// <summary>
        /// Retrieve all customers and orders from backend database
        /// </summary>
        /// <returns></returns>
        public bool RetrieveCustomersOrdersData()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Return order details by order id
        /// * pass in order id
        /// </summary>
        /// <returns></returns>
        public bool RetrieveOrderDetails()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Cascade removal of an order
        /// * pass in order id
        /// </summary>
        /// <returns></returns>
        public bool RemoveOrder()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Remove a single detail
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Since the original database table did not have a primary key this method
        /// will have to resort to removal by multiple fields. In hindsight this is a lesson
        /// for those designing a table like this to have a key. I could have added one but
        /// decided not too as this is really past the intent of this series.
        /// </remarks>
        public bool RemoveSingleLineItem()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Add new order
        /// * pass in Customer id
        /// </summary>
        /// <returns></returns>
        public bool AddOrder()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Edit existing order
        /// * pass in order id
        /// </summary>
        /// <returns></returns>
        public bool EditOrder()
        {
            throw new NotImplementedException();
        }
    }
}
