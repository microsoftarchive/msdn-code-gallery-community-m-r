namespace MVVMCalc.Common
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using Microsoft.Practices.Prism.ViewModel;

    /// <summary>
    /// ViewModelの基本クラス。
    /// INotifyPropertyChangedの実装はPrismのNotificationObjectを使用。
    /// IDataErrorInfoの実装にはPrismのErrorsContainerを使用して簡略化。
    /// </summary>
    public class ViewModelBase : NotificationObject, IDataErrorInfo
    {
        private ErrorsContainer<string> errors;

        /// <summary>
        /// 検証エラーの情報を格納するコンテナを取得する。
        /// </summary>
        protected ErrorsContainer<string> Errors
        {
            get
            {
                if (this.errors == null)
                {
                    // エラーの内容に変更があったときはHasErrorプロパティの変更通知を行う
                    this.errors = new ErrorsContainer<string>(
                        s => this.RaisePropertyChanged(() => HasError));
                }

                return errors;
            }
        }

        /// <summary>
        /// 使用しないため未実装
        /// </summary>
        string IDataErrorInfo.Error
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// プロパティのエラーメッセージを取得する。
        /// </summary>
        /// <param name="columnName">プロパティ名</param>
        /// <returns>エラーメッセージ。エラーがないときにはnullを返す。</returns>
        public string this[string columnName]
        {
            get 
            {
                return this.Errors.GetErrors(columnName).FirstOrDefault();
            }
        }

        /// <summary>
        /// エラーがある場合にtrueを返す。
        /// </summary>
        public bool HasError
        {
            get
            {
                return this.Errors.HasErrors;
            }
        }
    }
}
