using MVVMLightWPFSampleApp.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace MVVMLightWPFSampleApp.ViewModels
{
    public class PersonViewModel
    {
        public Person Model { get; private set; }

        [Required(ErrorMessage = "Name is required")]
        public ReactiveProperty<string> Name { get; private set; }
        [Required(ErrorMessage = "Age is required")]
        [RegularExpression("[0-9]+", ErrorMessage = "Age is integer")]
        public ReactiveProperty<string> Age { get; private set; }

        public ReactiveProperty<bool> HasErrors { get; private set; }

        public PersonViewModel(Person model)
        {
            this.Model = model;

            this.Name = model.ToReactivePropertyAsSynchronized(
                x => x.Name,
                ignoreValidationErrorValue: true)
                .SetValidateAttribute(() => this.Name);

            this.Age = model.ToReactivePropertyAsSynchronized(
                x => x.Age,
                convert: x => x.ToString(),
                convertBack: x => int.Parse(x),
                ignoreValidationErrorValue: true)
                .SetValidateAttribute(() => this.Age);

            this.HasErrors = new[]
                {
                    this.Name.ObserveHasError,
                    this.Age.ObserveHasError
                }
                .CombineLatest(x => x.Any(y => y))
                .ToReactiveProperty();
        }
    }
}