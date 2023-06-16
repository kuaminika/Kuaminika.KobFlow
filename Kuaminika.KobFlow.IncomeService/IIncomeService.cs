
using System.Collections.Generic;

namespace Kuaminika.KobFlow.IncomeService
{
    public interface IIncomeService
    {
        List<IncomeModel> GetAll();
        IncomeModel Add(IncomeModel addMe);
        IncomeModel Delete(IncomeModel victim);
        IncomeModel Update(IncomeModel victim);
    }
}
