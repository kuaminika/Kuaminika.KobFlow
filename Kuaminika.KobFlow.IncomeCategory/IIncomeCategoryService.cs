
using System.Collections.Generic;

namespace Kuaminika.KobFlow.IncomeCategory
{
    public interface IIncomeCategoryService
    {
        List<IncomeCategoryModel> GetAll();
        IncomeCategoryModel Add(IncomeCategoryModel addMe);
        IncomeCategoryModel Delete(IncomeCategoryModel victim);
        IncomeCategoryModel Update(IncomeCategoryModel victim);
    }
}
