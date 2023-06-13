namespace Kuaminika.KobFlow.IncomeSourceService
{
    public interface IIncomeSourceRepository
    {
        IncomeSourceModel Add(IncomeSourceModel addMe);
        IncomeSourceModel Delete(IncomeSourceModel victim);
        IncomeSourceModel Update(IncomeSourceModel victim);
        List<IncomeSourceModel> GetAll();
    }


}