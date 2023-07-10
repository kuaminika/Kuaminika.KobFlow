namespace Kuaminika.KobFlow.MerchantService
{
    public interface IMerchantRepository
    {
        MerchantModel AddMerchant(MerchantModel addMe);
        MerchantModel DeleteMerchant(MerchantModel victim);
        MerchantModel UpdateMerchant(MerchantModel victim);
        List<MerchantModel> GetAllMerchants();
        List<MerchantModel_Assigned> GetAllAssignedRecords();
    }


}