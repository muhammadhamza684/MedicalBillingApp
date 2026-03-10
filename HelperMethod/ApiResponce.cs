namespace MedicalBillingApp.HelperMethod
{
    public class ApiResponce<T>
    {
        public bool isSuccess { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }

        public List<T>? Errors { get; set; }

        public int TotalRecords { get; set; }

        public ApiResponce(bool isSuccess, string message, T? data, List<T>? errors = null, int totalRecords = 0)
        {
            this.isSuccess = isSuccess;
            this.Message = message;
            this.Data = data;
            this.Errors = errors;
            this.TotalRecords = totalRecords;
        }

        public static ApiResponce<T> Success(T data, int totalRecords = 0, string message = "Request Successful")
        {
            return new ApiResponce<T>(true, message, data, null, totalRecords);
        }

        public static ApiResponce<T> Failure(string message = "Request Failed")
        {
            return new ApiResponce<T>(false, message, default);
        }
    }
}