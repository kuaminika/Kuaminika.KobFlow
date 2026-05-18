namespace Kuaminika.KobFlow.ExpenseService
{
    public interface IExpenseRepository
    {
        ExpenseModel Add(ExpenseModel addMe);
        ExpenseModel Delete(ExpenseModel victim);
        ExpenseModel Update(ExpenseModel victim);
        List<ExpenseModel> GetAll();
        List<ExpenseModel> BulkAdd(List<ExpenseModel> expenses);
        ExpenseModel FindById(long id);
    }


}