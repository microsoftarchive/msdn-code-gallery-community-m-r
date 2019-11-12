using GalaSoft.MvvmLight;

namespace MVVMLightWPFSampleApp.Models
{
    public class Person : ObservableObject
    {
        private long id;

        public long ID
        {
            get { return this.id; }
            set { this.Set(ref this.id, value); }
        }

        private string name;

        public string Name
        {
            get { return this.name; }
            set { this.Set(ref this.name, value); }
        }

        private int age;

        public int Age
        {
            get { return this.age; }
            set { this.Set(ref this.age, value); }
        }
    }
}
