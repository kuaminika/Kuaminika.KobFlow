
using System.Collections.Generic;

namespace Kuaminika.KobFlow.MerchantService
{
    public interface IMerchhantService
    {
        List<MerchantModel> GetAllMerchants();
        MerchantModel AddMerchant(MerchantModel addMe);
        MerchantModel DeleteMerchant(MerchantModel victim);
        MerchantModel UpdateMerchant(MerchantModel victim);
    }
}
