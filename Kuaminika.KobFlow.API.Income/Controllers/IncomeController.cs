using Kuaminika.KobFlow.IncomeService;
using Kuaniminka.KobFlow.ToolBox;
using Microsoft.AspNetCore.Mvc;

namespace Kuaminika.KobFlow.API.Income.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomeController : ControllerBase
    {
        IIncomeService incomeService;
        IKLogTool logTool;

        public IncomeController(IConfiguration configs)
        {
            KContainer container = new KContainer(configs);
            incomeService = container.Service;
            logTool = container.LogTool;
        }

        [HttpGet(Name = "Get")]
        public KRequestReceipt<List<IncomeModel>> Get()
        {
            List<IncomeModel> merchants = new List<IncomeModel>();
            try
            {
                merchants = incomeService.GetAll();
                return new KRequestReceipt<List<IncomeModel>>(merchants);
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);
                var result = new KRequestReceipt<List<IncomeModel>>(merchants);
                return handleError(result, ex);
            }
        }

        private KRequestReceipt<T> handleError<T>(KRequestReceipt<T> result, Exception ex)
        {
            result.Error = true;
            result.Message = $"something went wrong - {ex.Message}";
            return result;
        }

        [HttpPost(Name = "Add")]
        public KRequestReceipt<IncomeModel> Add(IncomeModel addMe)
        {
            try
            {
                IncomeModel recorded = incomeService.Add(addMe);

                KRequestReceipt<IncomeModel> receipt = new KRequestReceipt<IncomeModel>(recorded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);
                var result = new KRequestReceipt<IncomeModel>(addMe);
                return handleError(result, ex);

            }
        }


        [HttpPost(Name = "Delete")]
        public KRequestReceipt<IncomeModel> Delete(IncomeModel victim)
        {
            try
            {
                IncomeModel recorded = incomeService.Delete(victim);

                KRequestReceipt<IncomeModel> receipt = new KRequestReceipt<IncomeModel>(recorded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);

                var result = new KRequestReceipt<IncomeModel>(victim);
                return handleError(result, ex);
            }
        }

        [HttpPost(Name = "Update")]
        public KRequestReceipt<IncomeModel> Update(IncomeModel victim)
        {
            try
            {
                IncomeModel updatded = incomeService.Update(victim);

                KRequestReceipt<IncomeModel> receipt = new KRequestReceipt<IncomeModel>(updatded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);

                var result = new KRequestReceipt<IncomeModel>(victim);
                return handleError(result, ex);
            }
        }
    }
}