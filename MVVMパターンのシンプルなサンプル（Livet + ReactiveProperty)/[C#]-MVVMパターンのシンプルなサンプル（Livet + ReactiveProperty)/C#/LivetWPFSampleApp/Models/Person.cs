using Livet;

namespace LivetWPFSampleApp.Models
{
    public class Person : NotificationObject
    {

        #region ID変更通知プロパティ
        private long _ID;

        public long ID
        {
            get
            { return _ID; }
            set
            { 
                if (_ID == value)
                    return;
                _ID = value;
                RaisePropertyChanged("ID");
            }
        }
        #endregion


        #region Name変更通知プロパティ
        private string _Name;

        public string Name
        {
            get
            { return _Name; }
            set
            { 
                if (_Name == value)
                    return;
                _Name = value;
                RaisePropertyChanged("Name");
            }
        }
        #endregion


        #region Age変更通知プロパティ
        private int _Age;

        public int Age
        {
            get
            { return _Age; }
            set
            { 
                if (_Age == value)
                    return;
                _Age = value;
                RaisePropertyChanged("Age");
            }
        }
        #endregion

    }
}
