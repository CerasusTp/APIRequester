using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APIRequester
{
    public class ResponsResult
    {
        public bool IsSuccess { get; }
        public HttpStatusCode StatusCode { get; }
        public string? Body { get; }

        // 失敗の場合（通信エラー等）
        public ResponsResult(bool _isSuccess)
        {
            IsSuccess = _isSuccess;
        }

        // 成功の場合（200ステータス以外も含む）
        public ResponsResult(HttpStatusCode _statusCode, string _body)
        {
            IsSuccess = true;
            StatusCode = _statusCode;
            Body = _body;
        }
    }
}
