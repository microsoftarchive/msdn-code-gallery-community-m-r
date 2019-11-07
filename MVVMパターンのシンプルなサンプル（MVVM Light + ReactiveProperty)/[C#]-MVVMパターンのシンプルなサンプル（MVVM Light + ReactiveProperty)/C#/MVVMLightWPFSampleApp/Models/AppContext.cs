using GalaSoft.MvvmLight;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace MVVMLightWPFSampleApp.Models
{
    public class AppContext : ObservableObject
    {
        /// <summary>
        /// 唯一のModelのインスタンス
        /// </summary>
        public static readonly AppContext Instance = new AppContext();

        /// <summary>
        /// Model間で連携するためのパイプ
        /// </summary>
        private readonly Subject<object> interaction = new Subject<object>();

        /// <summary>
        /// 閲覧と追加を管理する
        /// </summary>
        public PeopleMaster Master { get; private set; }

        /// <summary>
        /// 単一項目の編集担当
        /// </summary>
        public PersonDetail Detail { get; private set; }

        public AppContext()
        {
            this.Master = new PeopleMaster(this.interaction);
            this.Detail = new PersonDetail(this.interaction);
        }
    }
}
