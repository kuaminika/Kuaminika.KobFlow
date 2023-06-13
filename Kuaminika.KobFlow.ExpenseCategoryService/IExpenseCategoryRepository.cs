namespace Kuaminika.KobFlow.ExpenseCategoryService
{
    public interface IExpenseCategoryRepository
    {
        ExpenseCategoryModel Add(ExpenseCategoryModel addMe);
        ExpenseCategoryModel Delete(ExpenseCategoryModel victim);
        ExpenseCategoryModel Update(ExpenseCategoryModel victim);
        List<ExpenseCategoryModel> GetAll();
    }


}