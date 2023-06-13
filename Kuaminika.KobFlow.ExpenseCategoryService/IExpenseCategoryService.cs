
using System.Collections.Generic;

namespace Kuaminika.KobFlow.ExpenseCategoryService
{
    public interface IExpenseCategoryService
    {
        List<ExpenseCategoryModel> GetAll();
        ExpenseCategoryModel Add(ExpenseCategoryModel addMe);
        ExpenseCategoryModel Delete(ExpenseCategoryModel victim);
        ExpenseCategoryModel Update(ExpenseCategoryModel victim);
    }
}
