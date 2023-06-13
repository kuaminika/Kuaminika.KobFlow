
using System.Collections.Generic;

namespace Kuaminika.KobFlow.ExpenseService
{
    public interface IExpenseService
    {
        List<ExpenseModel> GetAll();
        ExpenseModel Add(ExpenseModel addMe);
        ExpenseModel Delete(ExpenseModel victim);
        ExpenseModel Update(ExpenseModel victim);
    }
}
