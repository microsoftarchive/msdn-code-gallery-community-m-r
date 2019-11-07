using Microsoft.OData.Client;
using Microsoft.Practices.Prism.Mvvm;
using ODataReadWriteSampleApp.Models.OData;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ODataReadWriteSampleApp.Models
{
    /// <summary>
    /// Person関連のModel
    /// </summary>
    public class PeopleModel : BindableBase
    {
        /// <summary>
        /// ODataへ接続するためのサービス
        /// </summary>
        private DemoService service;

        private ObservableCollection<Person> people;

        /// <summary>
        /// 読み込んだデータ
        /// </summary>
        public ObservableCollection<Person> People
        {
            get { return this.people; }
            set { this.SetProperty(ref this.people, value); }
        }

        private Person editTarget;

        /// <summary>
        /// 編集対象
        /// </summary>
        public Person EditTarget
        {
            get { return this.editTarget; }
            set { this.SetProperty(ref this.editTarget, value); }
        }

        /// <summary>
        /// サービスを使ってModelのインスタンスを作成
        /// </summary>
        /// <param name="service"></param>
        public PeopleModel(DemoService service)
        {
            this.service = service;
        }

        /// <summary>
        /// ODataのサービスからデータを読み込む
        /// </summary>
        /// <returns></returns>
        public async Task LoadPeopleAsync()
        {
            var results = await this.service.Persons
                .Expand(x => x.PersonDetail)
                .ExecuteAsync();
            this.People = new ObservableCollection<Person>(results);
        }

        /// <summary>
        /// 指定したIDのPersonを編集対象に設定する
        /// </summary>
        /// <param name="id"></param>
        public void SetEditTarget(int id)
        {
            this.EditTarget = this.People.First(x => x.ID == id);
        }

        /// <summary>
        /// 編集対象の変更内容をサーバーに反映する
        /// </summary>
        /// <returns></returns>
        public async Task SaveEditTargetChangsAsync()
        {
            this.service.ChangeState(this.EditTarget, EntityStates.Modified);
            await this.service.SaveChangesAsync();
        }
    }
}
