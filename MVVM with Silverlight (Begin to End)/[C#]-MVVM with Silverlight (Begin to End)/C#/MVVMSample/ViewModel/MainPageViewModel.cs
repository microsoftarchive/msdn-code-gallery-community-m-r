using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MVVMSample.PersonDataService;
using MVVMSample.ServiceAgent;
using Microsoft.Practices.Prism.Commands;

namespace MVVMSample.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {

        private PersonService _personService;
        public MainPageViewModel()
        {
            if (IsDesignTime != true)
            {
                this._personService = new PersonService() ;
                this.FindPersonCommand = new DelegateCommand<object>(this.FindPersonByID);
            }
        }

        private PersonData _personData;
        public PersonData PersonData
        { 
            get
            {
                return _personData;
            }
            set
            {
                if (_personData != value)
                {
                    _personData = value;
                    OnPropertyChanged("PersonData");
                }
            }
        }

        private int _personID;
        public int PersonID
        {
            get 
            {
                return _personID;
            } 
            set
            {
                if (_personID != value)
                {
                    _personID = value;
                    OnPropertyChanged("PersonID");
                }
            } 
        }

        public ICommand FindPersonCommand{get;set;}


        private void FindPersonByID(object obj)
        {
            if (PersonID != 0)
            {
                _personService.FindPerson(PersonID, (s, e) => PersonData = e.Result);
            }
        }

    }
}
