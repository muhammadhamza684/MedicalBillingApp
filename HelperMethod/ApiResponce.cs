namespace MedicalBillingApp.HelperMethod
{
    public class ApiResponce<T>
    {
        public bool isSuccess { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }

        public List<T>? Errors { get; set; }

        public ApiResponce(bool isSuccess, string Message, T? Data, List<T>? Errors = null)
        {
            this.isSuccess = isSuccess;
            this.Message = Message;
            this.Data = Data;
            this.Errors = Errors;


        }

        public static ApiResponce<T> Success(T Data, string Message = "Request Successful")
        {
            return new ApiResponce<T>(true, Message, Data);
        }

        public static ApiResponce<T> Failure(T Data, string Message = "Request Successful")
        {
            return new ApiResponce<T>(true, Message, Data);
        }


    }


}
