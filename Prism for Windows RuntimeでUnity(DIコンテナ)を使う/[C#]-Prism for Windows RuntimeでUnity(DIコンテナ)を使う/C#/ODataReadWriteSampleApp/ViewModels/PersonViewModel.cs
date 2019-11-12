using Microsoft.Practices.Prism.Mvvm;
using ODataReadWriteSampleApp.Models.OData;

namespace ODataReadWriteSampleApp.ViewModels
{
    /// <summary>
    /// Person用ViewModel
    /// </summary>
    public class PersonViewModel : BindableBase
    {
        private Person model;

        private string name;

        public string Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }

        public PersonViewModel(Person model)
        {
            this.model = model;
            this.Name = this.model.Name;
        }

        /// <summary>
        /// VMの変更をMへ反映する
        /// </summary>
        public void ApplyChanged()
        {
            this.model.Name = this.Name;
        }
    }
}
