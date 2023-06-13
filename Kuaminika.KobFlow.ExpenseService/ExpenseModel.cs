namespace Kuaminika.KobFlow.ExpenseService
{
    public struct ExpenseModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }

        public decimal Amount { get; set; }

        public  int MerchantId { get; set; }    
        public string MerchantName    { get; set; }
        public DateTime CreatedDate { get; set; }

        public string KobHolderName { get; set; }
        public int KobHolderId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}