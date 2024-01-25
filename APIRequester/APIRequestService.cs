using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APIRequester
{
    internal class APIRequestService
    {
        // HTTPクライアントは使い回す
        private readonly HttpClient httpClient;

        // APIの共通URL
        private readonly string baseUrl;

        // リクエストメッセージ
        private HttpRequestMessage? request;

        // 引数なしのインスタンス化はURLに空文字をセット
        public APIRequestService() : this("") { }

        // 共通URLをセット
        public APIRequestService(string _baseUrl)
        {
            baseUrl = _baseUrl;
            httpClient = new HttpClient();
        }

        public string? Get(string _api)
        {
            CreateRequest(HttpMethod.Get, $"{baseUrl}{_api}");

            Task<HttpResponseMessage> _response;
            string _result;
            HttpStatusCode _resStatusCoode;

            try
            {
                _response = httpClient.SendAsync(request);
                _result = _response.Result.Content.ReadAsStringAsync().Result;
                _resStatusCoode = _response.Result.StatusCode;
            }
            // 通信エラーの場合
            catch (HttpRequestException)
            {
                return null;
            }

            if (!_resStatusCoode.Equals(HttpStatusCode.OK))
            {
                return null;
            }
            if (string.IsNullOrEmpty(_result))
            {
                return null;
            }
            return _result;
        }

        // HTTPリクエストを作成
        private void CreateRequest(HttpMethod _httpMethod, string _url)
        {
            request = new HttpRequestMessage(_httpMethod, _url);
            // ヘッダー情報を追加
            request.Headers.Add("Accept", "application/json");
        }

        // 認証情報を追加


    }
}
