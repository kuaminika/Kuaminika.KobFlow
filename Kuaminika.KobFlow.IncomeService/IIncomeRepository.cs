namespace Kuaminika.KobFlow.IncomeService
{
    public interface IIncomeRepository
    {
        IncomeModel Add(IncomeModel addMe);
        IncomeModel Delete(IncomeModel victim);
        IncomeModel Update(IncomeModel victim);
        List<IncomeModel> GetAll();
    }


}