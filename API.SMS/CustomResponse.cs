namespace API.SMS
{
    public class CustomResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
