using GalaSoft.MvvmLight;
using Reactive.Bindings;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace MVVMLightWPFSampleApp.Models
{
    public class PeopleMaster : ObservableObject
    {
        private readonly PeopleRepository repository = new PeopleRepository();

        public ObservableCollection<Person> People { get; private set; }

        private Person inputPerson = new Person();

        public Person InputPerson
        {
            get { return this.inputPerson; }
            set { this.Set(ref this.inputPerson, value); }
        }

        public PeopleMaster(ISubject<object> interaction)
        {
            // 置き換えイベントが飛んできたらオンメモリ上の該当データを書き換える
            interaction.OfType<PersonChanged>()
                .Subscribe(x =>
                {
                    var target = this.People.First(y => y.ID == x.Person.ID);
                    target.Name = x.Person.Name;
                    target.Age = x.Person.Age;
                });

            this.People = new ObservableCollection<Person>();
        }

        /// <summary>
        /// データをリポジトリから読み込む
        /// </summary>
        public void Load()
        {
            this.People.Clear();
            var results = this.repository.Load();
            foreach (var i in results)
            {
                this.People.Add(i);
            }
        }

        /// <summary>
        /// 対象のIDのデータを削除する
        /// </summary>
        /// <param name="id"></param>
        public void Delete(long id)
        {
            this.repository.Delete(id);
            this.People.Remove(this.People.Single(x => x.ID == id));
        }

        /// <summary>
        /// データを追加する
        /// </summary>
        public void AddPerson()
        {
            this.repository.Insert(this.InputPerson);
            this.People.Add(this.InputPerson);
            this.InputPerson = new Person();
        }
    }
}
