namespace MVVMCalc.ViewModel
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
    using MVVMCalc.Common;
    using MVVMCalc.Model;
    using Okazuki.MVVM.PrismSupport.Interactivity;
    using Okazuki.MVVM.PrismSupport.Utils;
    using Okazuki.MVVM.PrismSupport.ViewModels;

    /// <summary>
    /// MainViewのViewModel
    /// </summary>
    public class MainViewModel : ValidatableViewModelBase
    {
        public MainViewModel()
        {
            this.CalculateTypes = CalculateTypeViewModel.Create().ToArray();

            // プロパティを初期化して妥当性検証を行う
            this.InitializeProperties();
        }

        /// <summary>
        /// 計算方式
        /// </summary>
        public IEnumerable<CalculateTypeViewModel> CalculateTypes { get; private set; }

        /// <summary>
        /// 現在選択されている計算方式
        /// </summary>
        public CalculateTypeViewModel SelectedCalculateType { get; set; }

        [Double(ErrorMessage = "数字を入力してください")]
        public string Lhs { get; set; }

        [Double(ErrorMessage = "数字を入力してください")]
        public string Rhs { get; set; }

        /// <summary>
        /// 計算結果
        /// </summary>
        public double Answer { get; set; }

        /// <summary>
        /// 計算処理のコマンド
        /// </summary>
        [AutoInitCommand]
        public DelegateCommand CalculateCommand { get; private set; }

        /// <summary>
        /// 計算結果にエラーがあったことを通知するメッセージを送信するメッセンジャーを取得する。
        /// </summary>
        [AutoInit]
        public InteractionRequest<Confirmation> ErrorRequest { get; private set; }

        /// <summary>
        /// 計算処理のコマンドの実行を行います。
        /// </summary>
        [CommandMethod]
        private void CalculateExecute()
        {
            // 現在の入力値を元に計算を行う
            var calc = new Calculator();
            this.Answer = calc.Execute(
                double.Parse(this.Lhs),
                double.Parse(this.Rhs),
                this.SelectedCalculateType.CalculateType);

            if (this.IsInvalidAnswer())
            {
                // 計算結果が実数の範囲から外れてる場合はViewに通知する
                this.ErrorRequest.Raise(
                    new ShowMessageBoxConfirmation
                    {
                        Title = "確認",
                        Content = "計算結果が実数の範囲を超えました。入力値を初期化しますか？",
                    },
                    r =>
                    {
                        // Viewから入力を初期化すると指定された場合はプロパティの初期化を行う
                        if (!r.Confirmed)
                        {
                            return;
                        }

                        InitializeProperties();
                    });
            }
        }

        /// <summary>
        /// 計算処理が実行可能かどうかの判定を行います。
        /// </summary>
        /// <returns></returns>
        [CommandMethod]
        private bool CanCalculateExecute()
        {
            // 現在選択されている計算方法がNone以外かつ入力にエラーがなければコマンドの実行が可能
            return this.SelectedCalculateType.CalculateType != CalculateType.None
                && !this.HasErrors;
        }

        /// <summary>
        /// Answerが有効な実装値か確認する。
        /// </summary>
        /// <returns>有効な実数の範囲にある場合はtrueを返す</returns>
        private bool IsInvalidAnswer()
        {
            return double.IsInfinity(this.Answer) || double.IsNaN(this.Answer);
        }

        /// <summary>
        /// プロパティの初期化を行う。
        /// </summary>
        private void InitializeProperties()
        {
            this.Lhs = string.Empty;
            this.Rhs = string.Empty;
            this.Answer = default(double);
            this.SelectedCalculateType = this.CalculateTypes.First();
        }
    }
}
