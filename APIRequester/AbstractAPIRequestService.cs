using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APIRequester
{
    abstract class AbstractAPIRequestService
    {
        // HTTPクライアントは使い回す
        protected readonly HttpClient httpClient;

        // リクエストメッセージ
        public abstract HttpRequestMessage? request { get; set; }

        protected ResponsResult SendRequest()
        {
            // リクエストがない場合はエラー
            if (request is null) { return new ResponsResult(false); };

            Task<HttpResponseMessage> _response;

            // 結果保存クラス
            ResponsResult _result;

            try
            {
                _response = httpClient.SendAsync(request);
                // 結果を格納
                _result = new ResponsResult(
                    _response.Result.StatusCode,
                    _response.Result.Content.ReadAsStringAsync().Result
                    );
            }
            // 通信エラーの場合
            catch (HttpRequestException)
            {
                _result = new ResponsResult(false);
            }

            return _result;
        }
    }
}
