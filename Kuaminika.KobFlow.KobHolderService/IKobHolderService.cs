
using System.Collections.Generic;

namespace Kuaminika.KobFlow.KobHolderService
{
    public interface IKobHolderService
    {
        List<KobHolderModel> GetAll();
        KobHolderModel Add(KobHolderModel addMe);
        KobHolderModel Delete(KobHolderModel victim);
        KobHolderModel Update(KobHolderModel victim);
    }
}
