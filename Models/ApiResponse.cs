using System.Net;

namespace ApiService.Models
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            ErrorMessages = new List<string>();
            Messages = new List<string>();

        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public List<string> Messages { get; set; }
        public object Result { get; set; }

    }
}

