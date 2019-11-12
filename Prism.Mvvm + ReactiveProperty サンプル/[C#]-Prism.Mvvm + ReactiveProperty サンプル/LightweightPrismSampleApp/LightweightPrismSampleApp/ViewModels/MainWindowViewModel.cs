using Codeplex.Reactive;
using Codeplex.Reactive.Extensions;
using LightweightPrismSampleApp.Models;
using LightweightPrismSampleApp.Utils;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;

namespace LightweightPrismSampleApp.ViewModels
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

        public MainWindowViewModel(AppContext model)
        {
            // 左辺値
            this.Lhs = model.Calculator.ObserveProperty(c => c.Lhs)
                .Select(d => d.ToString())
                .ToReactiveProperty()
                .SetValidateAttribute(() => this.Lhs);
            // エラーが無いときはModelへ代入する
            this.Lhs
                .Where(_ => !this.Lhs.HasErrors)
                .Subscribe(s => model.Calculator.Lhs = double.Parse(s));

            // 右辺値
            this.Rhs = model.Calculator.ObserveProperty(c => c.Rhs)
                .Select(d => d.ToString())
                .ToReactiveProperty()
                .SetValidateAttribute(() => this.Rhs);
            // エラーが無いときはModelへ代入する
            this.Rhs
                .Where(_ => !this.Rhs.HasErrors)
                .Subscribe(s => model.Calculator.Rhs = double.Parse(s));

            // 確認ダイアログ用メッセンジャー
            this.ConfirmRequest = new InteractionRequest<Confirmation>();

            // 左辺値・右辺値にエラーがないときに実行可能なコマンド
            this.CalcCommand =
                new[]
                {
                    this.Lhs.ObserveErrorChanged.Select(errors => errors == null),
                    this.Rhs.ObserveErrorChanged.Select(errors => errors == null)
                }
                .CombineLatestValuesAreAllTrue()
                .ToReactiveCommand();

            // 計算実行コマンドから答えを導出する
            this.Answer = model.Calculator.ObserveProperty(c => c.Answer)
                .Select(d => d.ToString())
                .ToReactiveProperty();

            // 計算を実行するコマンド
            this.CalcCommand
                .SelectMany(_ => this.ConfirmRequest.RaiseAsObservable(new Confirmation
                {
                    Title = "確認",
                    Content = "計算を実行してもいいですか"
                }))
                .Where(c => c.Confirmed)
                .Subscribe(_ => model.Calculator.Div());

            // 入力をリセット
            this.ResetCommand = new ReactiveCommand();
            this.ResetCommand.Subscribe(_ => model.Reset());

            // モデルからのエラー通知を処理する
            model.ErrorEvent
                .Subscribe(ex =>
                {
                    this.Answer.Value = ex.Message;
                });
        }
    }
}
