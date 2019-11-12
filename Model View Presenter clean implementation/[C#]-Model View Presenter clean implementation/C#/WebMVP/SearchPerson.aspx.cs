using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presenter;
using EModel;

namespace WebMVP
{
    public partial class _Default : System.Web.UI.Page, ISearchPerson
    {
        #region Private variables

        private List<Person> persons = new List<Person>();
        private PersonPresenter presenter;

        #endregion

        /// <summary>
        /// page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //This method can be used to instantiate presenter and inject view which is calling it.
            InitPresenter();
        }

        /// <summary>
        /// Check if presenter is there initialize
        /// </summary>
        private void InitPresenter()
        {
            if (presenter == null)
                presenter = new PersonPresenter(this);
        }

        #region ISearchPerson Members

        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get { return txtName.Text; }
        }

        /// <summary>
        /// Age
        /// </summary>
        public string Age
        {
            get { return txtAge.Text; }
        }

        /// <summary>
        /// Department
        /// </summary>
        public string Dept
        {
            get { return txtDept.Text; }
        }

        /// <summary>
        /// Event
        /// </summary>
        public event VoidHandler Search;

        /// <summary>
        /// Persons returned by Presenter
        /// </summary>
        public List<EModel.Person> Persons
        {
            set { persons = value; }
        }

        #endregion

        /// <summary>
        /// Search button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void _Search_Click(object sender, EventArgs e)
        {
            //Initialize presenter
            InitPresenter();

            //Raise event 
            if (Search != null)
                Search();

            //Bind view controls with contract properties/variables set by contract
            GridView1.DataSource = persons;
            GridView1.DataBind();
        }
    }
}
