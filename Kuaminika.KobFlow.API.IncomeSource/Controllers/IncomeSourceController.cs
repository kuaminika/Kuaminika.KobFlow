using Kuaminika.KobFlow.IncomeSourceService;
using Kuaniminka.KobFlow.ToolBox;
using Microsoft.AspNetCore.Mvc;

namespace Kuaminika.KobFlow.API.IncomeSource.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomeSourceController : ControllerBase
    {
        IIncomeSourceService service;
        IKLogTool logTool;

        public IncomeSourceController(IConfiguration configs)
        {
            KContainer container = new KContainer(configs);
            service = container.Service;
            logTool = container.LogTool;
        }

        [HttpGet(Name = "GetKobHolder")]
        public KRequestReceipt<List<IncomeSourceModel>> Get()
        {
            List<IncomeSourceModel> merchants = new List<IncomeSourceModel>();
            try
            {
                merchants = service.GetAll();
                return new KRequestReceipt<List<IncomeSourceModel>>(merchants);
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);
                var result = new KRequestReceipt<List<IncomeSourceModel>>(merchants);
                return handleError(result, ex);
            }
        }

        private KRequestReceipt<T> handleError<T>(KRequestReceipt<T> result, Exception ex)
        {
            result.Error = true;
            result.Message = $"something went wrong - {ex.Message}";
            return result;
        }

        [HttpPost(Name = "AddKobHolder")]
        public KRequestReceipt<IncomeSourceModel> Add(IncomeSourceModel addMe)
        {
            try
            {
                IncomeSourceModel recorded = service.Add(addMe);

                KRequestReceipt<IncomeSourceModel> receipt = new KRequestReceipt<IncomeSourceModel>(recorded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);
                var result = new KRequestReceipt<IncomeSourceModel>(addMe);
                return handleError(result, ex);

            }
        }


        [HttpPost(Name = "DeleteKobHolder")]
        public KRequestReceipt<IncomeSourceModel> Delete(IncomeSourceModel victim)
        {
            try
            {
                IncomeSourceModel recorded = service.Delete(victim);

                KRequestReceipt<IncomeSourceModel> receipt = new KRequestReceipt<IncomeSourceModel>(recorded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);

                var result = new KRequestReceipt<IncomeSourceModel>(victim);
                return handleError(result, ex);
            }
        }

        [HttpPost(Name = "UpdateKobHolder")]
        public KRequestReceipt<IncomeSourceModel> Update(IncomeSourceModel victim)
        {
            try
            {
                IncomeSourceModel updatded = service.Update(victim);

                KRequestReceipt<IncomeSourceModel> receipt = new KRequestReceipt<IncomeSourceModel>(updatded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);

                var result = new KRequestReceipt<IncomeSourceModel>(victim);
                return handleError(result, ex);
            }
        }
    }
}