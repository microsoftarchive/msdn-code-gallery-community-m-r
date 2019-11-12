using Livet;
using LivetWPFSampleApp.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;

namespace LivetWPFSampleApp.ViewModels
{
    /// <summary>
    /// Personに対するViewModel
    /// </summary>
    public class PersonViewModel : ViewModel
    {
        /// <summary>
        /// Model
        /// </summary>
        public Person Model { get; private set; }

        /// <summary>
        /// 名前
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        public ReactiveProperty<string> Name { get; private set; }

        /// <summary>
        /// 年齢
        /// </summary>
        [Required(ErrorMessage = "Age is required")]
        [RegularExpression("[0-9]+", ErrorMessage = "Age is integer")]
        public ReactiveProperty<string> Age { get; private set; }

        /// <summary>
        /// 検証エラーの有無
        /// </summary>
        public ReactiveProperty<bool> HasErrors { get; private set; }

        public PersonViewModel(Person model)
        {
            this.Model = model;

            // ModelのNameプロパティをRxProp化
            this.Name = model.ToReactivePropertyAsSynchronized(
                x => x.Name,
                ignoreValidationErrorValue: true)
                .SetValidateAttribute(() => this.Name)
                .AddTo(this.CompositeDisposable);

            // ModelのAgeプロパティをRxProp化
            this.Age = model.ToReactivePropertyAsSynchronized(
                x => x.Age,
                convert: x => x.ToString(),
                convertBack: x => int.Parse(x),
                ignoreValidationErrorValue: true)
                .SetValidateAttribute(() => this.Age)
                .AddTo(this.CompositeDisposable);

            // いずれかのプロパティの値がFalseならFalse
            this.HasErrors = new[]
                {
                    this.Name.ObserveHasError,
                    this.Age.ObserveHasError
                }
                .CombineLatest(x => x.Any(y => y))
                .ToReactiveProperty()
                .AddTo(this.CompositeDisposable);
        }
    }
}
