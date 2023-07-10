namespace Kuaminika.KobFlow.KobHolderService
{
    public class KobHolderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
    }

    public class KobHolderModel_Count: KobHolderModel
    {
        public int Count { get; set; }   
    }

}