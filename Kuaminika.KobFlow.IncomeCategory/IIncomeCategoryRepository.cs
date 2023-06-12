namespace Kuaminika.KobFlow.IncomeCategory
{
    public interface IIncomeCategoryRepository
    {
        IncomeCategoryModel Add(IncomeCategoryModel addMe);
        IncomeCategoryModel Delete(IncomeCategoryModel victim);
        IncomeCategoryModel Update(IncomeCategoryModel victim);
        List<IncomeCategoryModel> GetAll();
    }


}