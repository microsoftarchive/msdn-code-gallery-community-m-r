using Livet;
using Reactive.Bindings;
using System.Reactive.Subjects;

namespace LivetWPFSampleApp.Models
{
    public class PersonDetail : NotificationObject
    {
        private readonly PeopleRepository repository = new PeopleRepository();

        private ISubject<object> interaction;


        #region EditTarget変更通知プロパティ
        private Person _EditTarget;

        /// <summary>
        /// 編集対象
        /// </summary>
        public Person EditTarget
        {
            get
            { return _EditTarget; }
            private set
            { 
                if (_EditTarget == value)
                    return;
                _EditTarget = value;
                RaisePropertyChanged("EditTarget");
            }
        }
        #endregion


        public PersonDetail(ISubject<object> interaction)
        {
            this.interaction = interaction;
        }

        /// <summary>
        /// 編集を永続化
        /// </summary>
        public void Update()
        {
            // リポジトリに書き込む
            this.repository.Update(this.EditTarget);
            // Subjectを通じて変更があったことを外部に通知する
            this.interaction.OnNext(new PersonChanged(this.EditTarget));
        }

        /// <summary>
        /// 変更対象を指定する
        /// </summary>
        /// <param name="id"></param>
        public void SetEditTarget(long id)
        {
            this.EditTarget = this.repository.Find(id);
        }
    }
}
