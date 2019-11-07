namespace MVVMCalc.ViewModel
{
    using System.Collections.Generic;
    using System.Linq;
    using MVVMCalc.Common;
    using MVVMCalc.Model;

    /// <summary>
    /// MainViewのViewModel
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private string lhs;

        private string rhs;

        private double answer;

        private CalculateTypeViewModel selectedCalculateType;

        private DelegateCommand calculateCommand;

        private Messenger errorMessenger = new Messenger();

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
        public CalculateTypeViewModel SelectedCalculateType
        {
            get
            {
                return this.selectedCalculateType;
            }

            set
            {
                this.selectedCalculateType = value;
                this.RaisePropertyChanged("SelectedCalculateType");
            }
        }

        /// <summary>
        /// 計算の左辺値
        /// </summary>
        public string Lhs
        {
            get
            {
                return this.lhs;
            }

            set
            {
                this.lhs = value;
                if (!this.IsDouble(value))
                {
                    this.SetError("Lhs", "数字を入力してください");
                }
                else
                {
                    this.ClearError("Lhs");
                }

                this.RaisePropertyChanged("Lhs");
            }
        }

        /// <summary>
        /// 計算の右辺値
        /// </summary>
        public string Rhs
        {
            get
            {
                return this.rhs;
            }

            set
            {
                this.rhs = value;
                if (!this.IsDouble(value))
                {
                    this.SetError("Rhs", "数字を入力してください");
                }
                else
                {
                    this.ClearError("Rhs");
                }

                this.RaisePropertyChanged("Rhs");
            }
        }

        /// <summary>
        /// 計算結果
        /// </summary>
        public double Answer
        {
            get
            {
                return this.answer;
            }

            set
            {
                this.answer = value;
                this.RaisePropertyChanged("Answer");
            }
        }

        /// <summary>
        /// 計算処理のコマンド
        /// </summary>
        public DelegateCommand CalculateCommand
        {
            get
            {
                if (this.calculateCommand == null)
                {
                    this.calculateCommand = new DelegateCommand(CalculateExecute, CanCalculateExecute);
                }

                return this.calculateCommand;
            }
        }

        /// <summary>
        /// 計算結果にエラーがあったことを通知するメッセージを送信するメッセンジャーを取得する。
        /// </summary>
        public Messenger ErrorMessenger
        {
            get
            {
                return this.errorMessenger;
            }
        }

        /// <summary>
        /// 計算処理のコマンドの実行を行います。
        /// </summary>
        private void CalculateExecute()
        {
            // 現在の入力値を元に計算を行う
            var calc = new Calculator();
            this.Answer = calc.Execute(
                double.Parse(this.Lhs), 
                double.Parse(this.Rhs), 
                this.SelectedCalculateType.CalculateType);

            if (IsInvalidAnswer())
            {
                // 計算結果が実数の範囲から外れてる場合はViewに通知する
                this.ErrorMessenger.Raise(
                    new Message("計算結果が実数の範囲を超えました。入力値を初期化しますか？"),
                    m =>
                    {
                        // Viewから入力を初期化すると指定された場合はプロパティの初期化を行う
                        if (!(bool)m.Response)
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
        private bool CanCalculateExecute()
        {
            // 現在選択されている計算方法がNone以外かつ入力にエラーがなければコマンドの実行が可能
            return this.SelectedCalculateType.CalculateType != CalculateType.None 
                && !this.HasError;
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
