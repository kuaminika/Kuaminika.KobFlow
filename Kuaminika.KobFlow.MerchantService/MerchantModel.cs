namespace Kuaminika.KobFlow.MerchantService
{
    public class MerchantModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
    }


    public class MerchantModel_Assigned :MerchantModel
    {
        public int ExpenseCount { get; set; }

    }
}