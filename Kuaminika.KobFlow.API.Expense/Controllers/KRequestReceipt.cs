namespace Kuaminika.KobFlow.API.Expense.Controllers
{
    public class KRequestReceipt<T>
    {
        public KRequestReceipt(T element)
        {
            Subject = element;
            Message = "This is a receipt";
            Error = false;
        }
        public bool Error { get; set; }
        public string Message { get; set; }
        public T Subject { get; set; }
    }
}