namespace MedicalBillingApp.HelperMethod
{
    public class ApiResponce<T>
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }

        public ApiResponce(T data, bool isSuccess = true, string errorMessage = "")
        {
            this.Data = data;
            this.IsSuccess = isSuccess;
            this.ErrorMessage = errorMessage;
        }
    }


}
