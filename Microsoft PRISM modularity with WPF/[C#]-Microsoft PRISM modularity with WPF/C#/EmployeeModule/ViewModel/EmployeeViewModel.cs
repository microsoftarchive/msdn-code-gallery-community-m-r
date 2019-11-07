using Microsoft.Practices.Prism.ViewModel;
using Repository.Model;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeModule.ViewModel
{
    [Export]
    public class EmployeeViewModel:NotificationObject
    {
        private IEmployeeRepository employeeRepository;

        [ImportingConstructor]
        public EmployeeViewModel(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            employeeList = new ObservableCollection<Employee>();
            Init();
        }

        private ObservableCollection<Employee> employeeList;
        public ObservableCollection<Employee> EmployeeList
        {
            get { return employeeList; }
            set
            {
                employeeList = value;
                RaisePropertyChanged("EmployeeList");
            }
        }

        private void Init()
        {
            var emps = employeeRepository.GetAllEmployees();
            foreach (var item in emps)
            {
                employeeList.Add(item);
            }
        }
    }
}
