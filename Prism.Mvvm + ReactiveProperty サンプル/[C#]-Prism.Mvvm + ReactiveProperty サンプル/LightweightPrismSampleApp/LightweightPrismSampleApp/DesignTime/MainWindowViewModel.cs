using Codeplex.Reactive;
using LightweightPrismSampleApp.Utils;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System.ComponentModel.DataAnnotations;

namespace LightweightPrismSampleApp.DesignTime
{
    public class MainWindowViewModel
    {
        [Required(ErrorMessage = "入力してください")]
        [CanConvert(typeof(double), ErrorMessage = "実数を入力してください")]
        public ReactiveProperty<string> Lhs { get; private set; }

        [Required(ErrorMessage = "入力してください")]
        [CanConvert(typeof(double), ErrorMessage = "実数を入力してください")]
        public ReactiveProperty<string> Rhs { get; private set; }

        public ReactiveProperty<string> Answer { get; private set; }

        public ReactiveCommand CalcCommand { get; private set; }

        public ReactiveCommand ResetCommand { get; private set; }

        public InteractionRequest<Confirmation> ConfirmRequest { get; private set; }

    }
}
