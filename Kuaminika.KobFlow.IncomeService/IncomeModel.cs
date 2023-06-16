namespace Kuaminika.KobFlow.IncomeService
{
    public struct IncomeModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }

        public decimal Amount { get; set; }

        public int SourceId { get; set; }
        public string SourceName { get; set; }
        public DateTime CreatedDate { get; set; }

        public string KobHolderName { get; set; }
        public int KobHolderId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}