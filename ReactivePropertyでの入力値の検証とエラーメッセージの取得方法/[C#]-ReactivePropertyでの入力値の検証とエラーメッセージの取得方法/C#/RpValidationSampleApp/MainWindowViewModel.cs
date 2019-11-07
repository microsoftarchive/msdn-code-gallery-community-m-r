using Codeplex.Reactive;
using Codeplex.Reactive.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpValidationSampleApp
{
    public class MainWindowViewModel
    {
        public ReactiveProperty<IEnumerable<string>> ErrorMessages { get; private set; }

        public ReactiveProperty<string> ErrorMessage { get; private set; }

        [Required(ErrorMessage = "その１ 入力してください")]
        public ReactiveProperty<string> AttrValidation { get; private set; }

        public ReactiveProperty<string> NotifyValidation { get; private set; }

        public ReactiveCommand AlertCommand { get; private set; }

        public ReactiveProperty<string> OutputMessage { get; private set; }

        public MainWindowViewModel()
        {
            // 属性による入力値の検証
            this.AttrValidation = new ReactiveProperty<string>()
                .SetValidateAttribute(() => this.AttrValidation);

            // ラムダ式による入力値の検証
            this.NotifyValidation = new ReactiveProperty<string>()
                .SetValidateNotifyError(x => string.IsNullOrEmpty(x) ? "その２ 入力してください" : null);

            // エラーがなくなった時に押せるコマンド
            this.AlertCommand = new[]
                {
                    // エラーがあるときにになるObserveHasErrorを束ねて
                    this.AttrValidation.ObserveHasError,
                    this.NotifyValidation.ObserveHasError
                }
                // 全てFalseだったら
                .CombineLatestValuesAreAllFalse()
                // コマンドに変換
                .ToReactiveCommand();
            this.AlertCommand.Subscribe(_ =>
                {
                    this.OutputMessage.Value = "OK!!";
                });

            // コマンドから設定するためのプロパティ
            this.OutputMessage = new ReactiveProperty<string>();

            // とりあえず最初になおしてほしいエラーを出す
            this.ErrorMessage = new[]
                {
                    // エラーを束ねて
                    this.AttrValidation.ObserveErrorChanged,
                    this.NotifyValidation.ObserveErrorChanged
                }
                .CombineLatest(x =>
                {
                    // null(エラーなし）を省いて
                    var r = x.Where(y => y != null)
                        // 最初のエラーを返す
                        .Select(y => y.OfType<string>())
                        .FirstOrDefault(y => y.Any());
                    // 無ければ無し、エラーがあれば最初のものを返す
                    return r == null ? null : r.FirstOrDefault();
                })
                .ToReactiveProperty();

            // 全部のエラーを出す
            this.ErrorMessages = new[]
                {
                    // エラーを束ねて
                    this.AttrValidation.ObserveErrorChanged,
                    this.NotifyValidation.ObserveErrorChanged
                }
                .CombineLatest(x =>
                {
                    // null(エラーなし)を省いて
                    return x.Where(y => y != null)
                        // IE<string>を平らに慣らす
                        .SelectMany(y => y.OfType<string>());
                })
                .ToReactiveProperty();
        }
    }
}
