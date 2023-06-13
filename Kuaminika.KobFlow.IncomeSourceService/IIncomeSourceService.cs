
using System.Collections.Generic;

namespace Kuaminika.KobFlow.IncomeSourceService
{
    public interface IIncomeSourceService
    {
        List<IncomeSourceModel> GetAll();
        IncomeSourceModel Add(IncomeSourceModel addMe);
        IncomeSourceModel Delete(IncomeSourceModel victim);
        IncomeSourceModel Update(IncomeSourceModel victim);
    }
}
