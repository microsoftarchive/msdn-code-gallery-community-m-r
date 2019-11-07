using GalaSoft.MvvmLight;
using System.Reactive.Subjects;

namespace MVVMLightWPFSampleApp.Models
{
    public class PersonDetail : ObservableObject
    {
        private readonly PeopleRepository repository = new PeopleRepository();

        private ISubject<object> interaction;

        private Person editTarget;

        public Person EditTarget
        {
            get { return this.editTarget; }
            private set { this.Set(ref this.editTarget, value); }
        }

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
