namespace MVVMCalc.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using Livet.Command;
    using Livet.Messaging;
    using MVVMCalc.Common;
    using MVVMCalc.Model;

    /// <summary>
    /// MainWindowのViewModel
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        /*コマンド、プロパティの定義にはそれぞれ 
         * 
         *  ldcom   : DelegateCommand(パラメータ無)
         *  ldcomn  : DelegateCommand(パラメータ無・CanExecute無)
         *  ldcomp  : DelegateCommand(型パラメータ有)
         *  ldcompn : DelegateCommand(型パラメータ有・CanExecute無)
         *  lprop   : 変更通知プロパティ
         *  
         * を使用してください。
         */

        public MainWindowViewModel()
        {
            this.CalculateTypes = CalculateTypeViewModel.Create().ToArray();

            this.InitializeProperties();
        }


        private string _Lhs;

        public string Lhs
        {
            get
            { return _Lhs; }
            set
            {
                if (_Lhs == value)
                    return;
                _Lhs = value;

                // 入力値の検証 
                if (!this.IsDouble(value))
                {
                    this.SetError("Lhs", "数字を入力してください");
                }
                else
                {
                    this.ClearError("Lhs");
                }

                RaisePropertyChanged("Lhs");
            }
        }


        private string _Rhs;

        public string Rhs
        {
            get
            { return _Rhs; }
            set
            {
                if (_Rhs == value)
                    return;
                _Rhs = value;

                // 入力値の検証 
                if (!this.IsDouble(value))
                {
                    this.SetError("Rhs", "数字を入力してください");
                }
                else
                {
                    this.ClearError("Rhs");
                }

                RaisePropertyChanged("Rhs");
            }
        }


        private double _Answer;

         /// <summary> 
        /// 計算結果 
        /// </summary>
        public double Answer
        {
            get
            { return _Answer; }
            set
            {
                if (_Answer == value)
                    return;
                _Answer = value;
                RaisePropertyChanged("Answer");
            }
        }

        /// <summary> 
        /// 計算方式 
        /// </summary> 
        public IEnumerable<CalculateTypeViewModel> CalculateTypes { get; private set; }


        private CalculateTypeViewModel _SelectedCalculateType;

        /// <summary> 
        /// 現在選択されている計算方式 
        /// </summary>
        public CalculateTypeViewModel SelectedCalculateType
        {
            get
            { return _SelectedCalculateType; }
            set
            {
                if (_SelectedCalculateType == value)
                    return;
                _SelectedCalculateType = value;
                RaisePropertyChanged("SelectedCalculateType");
            }
        }
      
        #region CalculateCommand
        private DelegateCommand _CalculateCommand;
        
        /// <summary> 
        /// 計算処理のコマンド 
        /// </summary>
        public DelegateCommand CalculateCommand
        {
            get
            {
                if (_CalculateCommand == null)
                    _CalculateCommand = new DelegateCommand(Calculate, CanCalculate);
                return _CalculateCommand;
            }
        }

        /// <summary> 
        /// 計算処理のコマンドの実行を行います。 
        /// </summary> 
        private void Calculate()
        {
            // 現在の入力値を元に計算を行う 
            var calc = new Calculator();
            this.Answer = calc.Execute(
                double.Parse(this.Lhs),
                double.Parse(this.Rhs),
                this.SelectedCalculateType.CalculateType);

            if (this.IsInvalidAnswer())
            {
                var result = this.Messenger.GetResponse<ConfirmMessage>(
                    new ConfirmMessage(
                        "計算結果が実数の範囲を超えました。入力値を初期化しますか？",
                        "確認",
                        MessageBoxImage.None,
                        MessageBoxButton.OKCancel,
                        "Error"));

                if (result.Response)
                {
                    this.InitializeProperties();
                }
            }
        }

        /// <summary> 
        /// 計算処理が実行可能かどうかの判定を行います。 
        /// </summary> 
        /// <returns></returns> 
        private bool CanCalculate()
        {
            // 現在選択されている計算方法がNone以外かつ入力にエラーがなければコマンドの実行が可能 
            return this.SelectedCalculateType.CalculateType != CalculateType.None
                && !this.HasError;
        }
        #endregion
      
      

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

        /// <summary> 
        /// valueがdouble型に変換できるかどうか検証します。 
        /// </summary> 
        /// <param name="value"></param> 
        /// <returns>doubleに変換できる場合はtrueを返します。</returns> 
        private bool IsDouble(string value)
        {
            var temp = default(double);
            return double.TryParse(value, out temp);
        }
    }
}
