namespace Codeplex.HttpClientExtensions
{
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// HttpClientがシームレスにJSONと連携するための拡張メソッドを提供します。
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// ボディ部にJSONを含むHTTPリクエストをPOSTします。
        /// </summary>
        /// <typeparam name="T">JSONにシリアライズする型</typeparam>
        /// <param name="self">拡張元のクラス</param>
        /// <param name="uri">リクエストを送信する先のURI</param>
        /// <param name="obj">ボディ部にJSONにシリアライズして含めるオブジェクト</param>
        /// <returns>レスポンス</returns>
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient self, string uri, T obj)
        {
            var content = CreateHttpContentFromObject(obj);
            return self.PostAsync(uri, content);
        }

        /// <summary>
        /// ボディ部にJSONを含むHTTPリクエストをPUTします。
        /// </summary>
        /// <typeparam name="T">JSONにシリアライズする型</typeparam>
        /// <param name="self">拡張元のクラス</param>
        /// <param name="uri">リクエストを送信する先のURI</param>
        /// <param name="obj">ボディ部にJSONにシリアライズして含めるオブジェクト</param>
        /// <returns>レスポンス</returns>
        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient self, string uri, T obj)
        {
            var content = CreateHttpContentFromObject(obj);
            return self.PutAsync(uri, content);
        }

        /// <summary>
        /// HttpResponseMessageのContentからJSONをオブジェクトにデシリアライズするメソッド
        /// </summary>
        /// <typeparam name="T">JSONをデシリアライズする型</typeparam>
        /// <param name="content">HttpContent</param>
        /// <returns>HttpContentから読み込んだJSONをデシリアライズした結果のオブジェクト</returns>
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var binary = await content.ReadAsByteArrayAsync();
            var jsonText = Encoding.UTF8.GetString(binary, 0, binary.Length);
            return JsonConvert.DeserializeObject<T>(jsonText);
        }

        /// <summary>
        /// オブジェクトからJSONを含んだHttpContentを作成する
        /// </summary>
        private static HttpContent CreateHttpContentFromObject(object obj)
        {
            var jsonText = JsonConvert.SerializeObject(obj);
            var content = new ByteArrayContent(Encoding.UTF8.GetBytes(jsonText));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }
    }
}
