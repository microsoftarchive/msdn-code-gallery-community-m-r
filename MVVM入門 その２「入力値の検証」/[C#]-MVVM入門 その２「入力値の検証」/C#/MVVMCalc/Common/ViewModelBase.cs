namespace MVVMCalc.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// ViewModelの基本クラス。INotifyPropertyChangedとIDataErrorInfoの実装を提供します。
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// プロパティに紐づいたエラーメッセージを格納します。
        /// </summary>
        private Dictionary<string, string> errors = new Dictionary<string, string>();

        /// <summary>
        /// プロパティの変更があったときに発行されます。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// PropertyChangedイベントを発行します。
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var h = this.PropertyChanged;
            if (h != null)
            {
                h(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 未使用
        /// </summary>
        string IDataErrorInfo.Error
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// columnNameで指定したプロパティのエラーを返します。
        /// </summary>
        /// <param name="columnName">プロパティ名</param>
        /// <returns>エラーメッセージ</returns>
        public string this[string columnName]
        {
            get 
            {
                if (this.errors.ContainsKey(columnName))
                {
                    return this.errors[columnName];
                }

                return null;
            }
        }

        /// <summary>
        /// プロパティにエラーメッセージを設定します。
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <param name="errorMessage">エラーメッセージ</param>
        protected void SetError(string propertyName, string errorMessage)
        {
            this.errors[propertyName] = errorMessage;
            this.RaisePropertyChanged("HasError");
        }

        /// <summary>
        /// プロパティのエラーをクリアします。
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected void ClearError(string propertyName)
        {
            if (this.errors.ContainsKey(propertyName))
            {
                this.errors.Remove(propertyName);
                this.RaisePropertyChanged("HasError");
            }
        }

        /// <summary>
        /// 全てのエラーをクリアします。
        /// </summary>
        protected void ClearErrors()
        {
            this.errors.Clear();
            this.RaisePropertyChanged("HasError");
        }

        /// <summary>
        /// エラーの有無を取得します。
        /// </summary>
        public bool HasError
        {
            get
            {
                return this.errors.Count != 0;
            }
        }
    }
}
