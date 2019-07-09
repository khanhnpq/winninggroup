using System.Collections;

namespace WinningGroup.Utility
{
    public class CommonJsonDataFactory
    {
        public static CommonJsonDataModel Create(JsonResponseType responseType, object payload = null, IEnumerable errors = null)
        {
            CommonJsonDataModel json = new CommonJsonDataModel();

            switch (responseType)
            {
                case JsonResponseType.Ok:
                    json.Payload = payload;
                    json.Status = CommonJsonStatusModel.GetSuccessfulStatus();
                    break;
                case JsonResponseType.BadRequest:
                    json.Payload = payload;
                    json.Status = CommonJsonStatusModel.GetNonSuccessfulStatus(400, null, errors ?? new object());
                    break;
            }

            return json;
        }
    }

    public class CommonJsonDataModel
    {
        public object Payload { get; set; }

        public CommonJsonStatusModel Status { get; set; }

        public string RedirectUrl { get; set; }
    }

    public class CommonJsonStatusModel
    {
        public CommonJsonStatusModel(bool isSuccess, int code, string message, object validationErrors)
        {
            IsSuccess = isSuccess;
            Code = code;
            Message = message;
            ValidationErrors = validationErrors;
        }

        public static CommonJsonStatusModel GetNonSuccessfulStatus(int code, string message, object validationErrors)
        {
            return new CommonJsonStatusModel(false, code, message, validationErrors);
        }

        public static CommonJsonStatusModel GetNonSuccessfulStatus(int code, string message)
        {
            return new CommonJsonStatusModel(false, code, message, null);
        }

        public static CommonJsonStatusModel GetSuccessfulStatus()
        {
            return new CommonJsonStatusModel(true, 200, null, null);
        }

        public bool IsSuccess { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }

        public object ValidationErrors { get; set; }
    }

    public enum JsonResponseType
    {
        Ok,
        BadRequest
    }
}
