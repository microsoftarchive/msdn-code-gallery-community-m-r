using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EModel;
using Service;
namespace Presenter
{
    /// <summary>
    /// Duty of presenter is to serve a contract. Presenter can serve multiple contracts. Each contract 
    /// is implemented by one or more views. 
    /// </summary>
    public class PersonPresenter
    {
        ISearchPerson _View;

        /// <summary>
        /// Constructor injection
        /// </summary>
        /// <param name="searchPerson"></param>
        public PersonPresenter(ISearchPerson searchPerson)
        {
            searchPerson.Search += new VoidHandler(_Search);
            _View = searchPerson;
        }

        /// <summary>
        /// Handler for search event
        /// </summary>
        private void _Search()
        {
            List<Person> persons = AdministrationService.GetPersons(_View.Name, _View.Age, _View.Dept);

            //Setting contract for view to get information from presenter
            _View.Persons = persons;
        }

    }
}
