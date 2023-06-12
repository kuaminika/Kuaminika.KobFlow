using Kuaminika.KobFlow.KobHolderService;
using Kuaniminka.KobFlow.ToolBox;
using Microsoft.AspNetCore.Mvc;

namespace Kuaminika.KobFlow.API.KobHolder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KobHolderController : ControllerBase
    {
        IKobHolderService service;
        IKLogTool logTool;

        public KobHolderController(IConfiguration configs)
        {
            KContainer container = new KContainer(configs);
            service = container.Service;
            logTool = container.LogTool;
        }

        [HttpGet(Name = "GetKobHolder")]
        public KRequestReceipt<List<KobHolderModel>> Get()
        {
            List<KobHolderModel> merchants = new List<KobHolderModel>();
            try
            {
                merchants = service.GetAll();
                return new KRequestReceipt<List<KobHolderModel>>(merchants);
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);
                var result = new KRequestReceipt<List<KobHolderModel>>(merchants);
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
        public KRequestReceipt<KobHolderModel> Add(KobHolderModel addMe)
        {
            try
            {
                KobHolderModel recorded = service.Add(addMe);

                KRequestReceipt<KobHolderModel> receipt = new KRequestReceipt<KobHolderModel>(recorded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);
                var result = new KRequestReceipt<KobHolderModel>(addMe);
                return handleError(result, ex);

            }
        }


        [HttpPost(Name = "DeleteKobHolder")]
        public KRequestReceipt<KobHolderModel> Delete(KobHolderModel victim)
        {
            try
            {
                KobHolderModel recorded = service.Delete(victim);

                KRequestReceipt<KobHolderModel> receipt = new KRequestReceipt<KobHolderModel>(recorded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);

                var result = new KRequestReceipt<KobHolderModel>(victim);
                return handleError(result, ex);
            }
        }

        [HttpPost(Name = "UpdateKobHolder")]
        public KRequestReceipt<KobHolderModel> Update(KobHolderModel victim)
        {
            try
            {
                KobHolderModel updatded = service.Update(victim);

                KRequestReceipt<KobHolderModel> receipt = new KRequestReceipt<KobHolderModel>(updatded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);

                var result = new KRequestReceipt<KobHolderModel>(victim);
                return handleError(result, ex);
            }
        }
    }
}