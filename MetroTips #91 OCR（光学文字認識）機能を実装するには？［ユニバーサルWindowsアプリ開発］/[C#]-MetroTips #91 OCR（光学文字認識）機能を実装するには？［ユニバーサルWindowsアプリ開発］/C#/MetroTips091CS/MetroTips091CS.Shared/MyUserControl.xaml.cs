using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// ユーザー コントロールのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234236 を参照してください


// sample01.png - 「踊る人形」(青空文庫) を「青空文庫リーダー・ライト」で表示
//     http://www.aozora.gr.jp/cards/000009/card50713.html
//     http://apps.microsoft.com/windows/ja-jp/app/b7a93ca6-1b32-4ca9-94e5-499cfc84256e

// sample02.png - 「The Adventure of the Dancing Men」(Wikisource) を Internet Explorer で表示
//     http://en.wikisource.org/wiki/The_Adventure_of_the_Dancing_Men


namespace MetroTips091CS
{
  public sealed partial class MyUserControl : UserControl
  {

    // OCRに掛ける画像
    private Windows.UI.Xaml.Media.Imaging.WriteableBitmap _bitmap;


    public MyUserControl()
    {
      this.InitializeComponent();
    }

    private async void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
      await LoadSampleImage("sample01.png");
    }

    private async void MenuSample01_Click(object sender, RoutedEventArgs e)
    {
      await LoadSampleImage("sample01.png");
    }

    private async void MenuSample02_Click(object sender, RoutedEventArgs e)
    {
      await LoadSampleImage("sample02.png");
    }

    private async void MenuSample03_Click(object sender, RoutedEventArgs e)
    {
      await LoadSampleImage("sample03.png");
    }

    private async System.Threading.Tasks.Task LoadSampleImage(string fileName)
    {
      var sampleUri = new Uri("ms-appx:///Sample/" + fileName);
      var file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(sampleUri);
      _bitmap = await LoadImageAsync(file);

      this.SampleImage.Source = _bitmap;
    }

    private async static System.Threading.Tasks.Task<Windows.UI.Xaml.Media.Imaging.WriteableBitmap>
      LoadImageAsync(Windows.Storage.StorageFile file)
    {
      Windows.Storage.FileProperties.ImageProperties imgProp = await file.Properties.GetImagePropertiesAsync();
      using (var imgStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
      {
        var bitmap = new Windows.UI.Xaml.Media.Imaging.WriteableBitmap((int)imgProp.Width, (int)imgProp.Height);
        bitmap.SetSource(imgStream);
        return bitmap;
      }
    }



    private async void JapaneseButton_Click(object sender, RoutedEventArgs e)
    {
      await ExtractTextAsync(_bitmap, WindowsPreview.Media.Ocr.OcrLanguage.Japanese);
    }

    private async void EnglishButton_Click(object sender, RoutedEventArgs e)
    {
      await ExtractTextAsync(_bitmap, WindowsPreview.Media.Ocr.OcrLanguage.English);
    }

    private async System.Threading.Tasks.Task 
      ExtractTextAsync(Windows.UI.Xaml.Media.Imaging.WriteableBitmap bitmap, 
                       WindowsPreview.Media.Ocr.OcrLanguage language)
    {
      // OcrEngineオブジェクトを生成する
      var ocrEngine = new WindowsPreview.Media.Ocr.OcrEngine(language);

      // OcrEngineに画像を渡して、文字列を認識させる
      var ocrResult = await ocrEngine.RecognizeAsync(
                              (uint)bitmap.PixelHeight,     // 画像の高さ
                              (uint)bitmap.PixelWidth,      // 画像の幅
                              bitmap.PixelBuffer.ToArray()  // 画像のデータ（バイト配列）
                            );
      if (ocrResult.Lines == null || ocrResult.Lines.Count == 0)
      {
        this.ReadText.Text = "(何も読み取れませんでした)";
        return;
      }

      // Word間の区切り（日本語では無し、英語ではスペースとする）
      var separater = string.Empty;
      if (language == WindowsPreview.Media.Ocr.OcrLanguage.English)
        separater = " ";

      // 結果を表示するテキストボックスをクリアする
      this.ReadText.Text = string.Empty;

      // 行番号（0始まり）を定義
      int lineIndex = 0;

      // 認識結果を行ごとに処理する
      foreach (var line in ocrResult.Lines)
      {
        // 1行分の文字列を格納するためのバッファ
        var sb = new System.Text.StringBuilder();

        // 認識結果の行を、Wordごとに処理する
        foreach (var word in line.Words)
        {
          // 認識した文字列をバッファに追加していく
          sb.Append(word.Text);
          sb.Append(separater);

          // word は OcrWord 型。Textプロパティ(string型)の他に、
          // その文字列を読み取った場所を表すLeft／Top／Widht／Heightプロパティ(int型)も持っている。
          // なお、OcrWord という名前だが、日本語では1文字になる。
        }

        // ここでは、読み取った1行を以下のフォーマットで表示することにした
        this.ReadText.Text += string.Format("[{0}{1}] {2}{3}", 
                                            (lineIndex++).ToString(),     // 行番号(0始まり)
                                            line.IsVertical ? "V" : "H",  // 縦書き／横書きの区別
                                            sb.ToString().TrimEnd(),      // 読み取った文字列
                                            Environment.NewLine           // 改行
                                           );
      }

      // 他に ocrResult.TextAngle (読み取った文字列群の、水平(または垂直)からの回転角度) も取得できる。
      // OcrWord 型の示す場所は、この TextAngle の分だけ回転している。
    }



  }
}
