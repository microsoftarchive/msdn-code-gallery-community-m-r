namespace DialogAction
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
    using Okazuki.MVVM.PrismSupport.Utils;
    using Okazuki.MVVM.PrismSupport.ViewModels;

    [MetadataType(typeof(Metadata))]
    public partial class MainWindowViewModel : ViewModelBase
    {
        // ファイルを開くリクエストをViewに通知するためのInteractionRequest
        [AutoInit]
        public InteractionRequest<OpenFileDialogConfirmation> OpenFileRequest { get; private set; }

        // コマンドの処理
        partial void OpenFileExecute()
        {
            this.OpenFileRequest.Raise(
                // タイトルだけ指定してファイルを開く要求をViewに通知する
                new OpenFileDialogConfirmation
                {
                    Title = "ViewModel経由で開く"
                },
                conf =>
                {
                    // キャンセルされたら未選択をFileNameに設定
                    if (!conf.Confirmed)
                    {
                        this.FileName = "未選択";
                        return;
                    }

                    // ファイルが選択されてたら名前をFileNameに設定
                    this.FileName = conf.FileName;
                });
        }

        class Metadata
        {
        }
    }
}
