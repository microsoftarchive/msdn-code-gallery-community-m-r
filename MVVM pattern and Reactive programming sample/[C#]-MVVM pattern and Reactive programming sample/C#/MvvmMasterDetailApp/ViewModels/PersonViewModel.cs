using GalaSoft.MvvmLight;
using MvvmMasterDetailApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MvvmMasterDetailApp.ViewModels
{
    public class PersonViewModel : ViewModelBase
    {
        private Person person;

        public Person Person
        {
            get { return this.person; }
            private set { this.Set(ref this.person, value); }
        }

        [Required(ErrorMessage = "Name is required.")]
        public ReactiveProperty<string> Name { get; private set; }

        [Required(ErrorMessage = "Age is required.")]
        [RegularExpression("[0-9]+", ErrorMessage = "Age is number.")]
        public ReactiveProperty<string> Age { get; private set; }

        public ReactiveProperty<bool> HasError { get; private set; }

        public PersonViewModel(Person person)
        {
            this.Person = person;

            this.Name = this.Person.ObserveProperty(x => x.Name)
                .ToReactiveProperty()
                .SetValidateAttribute(() => this.Name);

            this.Age = this.Person.ObserveProperty(x => x.Age)
                .Select(x => x.ToString())
                .ToReactiveProperty()
                .SetValidateAttribute(() => this.Age);

            this.HasError = new[]
                {
                    this.Name.ObserveHasErrors,
                    this.Age.ObserveHasErrors
                }
                .CombineLatest(x => x.Any(y => y))
                .ToReactiveProperty(mode: ReactivePropertyMode.RaiseLatestValueOnSubscribe);

            // update source
            this.HasError
                .Where(x => !x)
                .Subscribe(_ =>
                {
                    this.Person.Name = this.Name.Value;
                    this.Person.Age = int.Parse(this.Age.Value);
                });
        }
    }
}
