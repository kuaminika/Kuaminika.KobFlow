namespace Kuaminika.KobFlow.KobHolderService
{
    public interface IKobHolderRepository
    {
        KobHolderModel Add(KobHolderModel addMe);
        KobHolderModel Delete(KobHolderModel victim);
        KobHolderModel Update(KobHolderModel victim);
        List<KobHolderModel> GetAll();
        KobHolderModel_Count UsedHolderLikeThis(KobHolderModel victim);
    }


}