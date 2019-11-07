using Microsoft.Practices.Prism.Mvvm;

namespace PrismWPFSampleApp.Models
{
    public class Person : BindableBase
    {
        private long id;

        public long ID
        {
            get { return this.id; }
            set { this.SetProperty(ref this.id, value); }
        }

        private string name;

        public string Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }

        private int age;

        public int Age
        {
            get { return this.age; }
            set { this.SetProperty(ref this.age, value); }
        }
    }
}
