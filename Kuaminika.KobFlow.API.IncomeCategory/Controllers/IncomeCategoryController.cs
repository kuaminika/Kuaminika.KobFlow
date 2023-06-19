using Kuaminika.KobFlow.IncomeCategory;
using Kuaniminka.KobFlow.ToolBox;
using Microsoft.AspNetCore.Mvc;

namespace Kuaminika.KobFlow.API.IncomeCategory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomeCategoryController : ControllerBase
    {
        IIncomeCategoryService incomeCategoryService;
        IKLogTool logTool;

        public IncomeCategoryController(IConfiguration configs)
        {
            KContainer container = new KContainer(configs);
            incomeCategoryService = container.Service;
            logTool = container.LogTool;
        }

        [HttpGet(Name = "Get")]
        public KRequestReceipt<List<IncomeCategoryModel>> Get()
        {
            List<IncomeCategoryModel> merchants = new List<IncomeCategoryModel>();
            try
            {
                merchants = incomeCategoryService.GetAll();
                return new KRequestReceipt<List<IncomeCategoryModel>>(merchants);
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);
                var result = new KRequestReceipt<List<IncomeCategoryModel>>(merchants);
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
        [Route("Add")]
        public KRequestReceipt<IncomeCategoryModel> Add(IncomeCategoryModel addMe)
        {
            try
            {
                IncomeCategoryModel recorded = incomeCategoryService.Add(addMe);

                KRequestReceipt<IncomeCategoryModel> receipt = new KRequestReceipt<IncomeCategoryModel>(recorded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);
                var result = new KRequestReceipt<IncomeCategoryModel>(addMe);
                return handleError(result, ex);

            }
        }


        [HttpPost(Name = "Delete")]
        [Route("Delete")]
        public KRequestReceipt<IncomeCategoryModel> Delete(IncomeCategoryModel victim)
        {
            try
            {
                IncomeCategoryModel recorded = incomeCategoryService.Delete(victim);

                KRequestReceipt<IncomeCategoryModel> receipt = new KRequestReceipt<IncomeCategoryModel>(recorded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);

                var result = new KRequestReceipt<IncomeCategoryModel>(victim);
                return handleError(result, ex);
            }
        }

        [HttpPost(Name = "Update")]
        [Route("Update")]
        public KRequestReceipt<IncomeCategoryModel> Update(IncomeCategoryModel victim)
        {
            try
            {
                IncomeCategoryModel updatded = incomeCategoryService.Update(victim);

                KRequestReceipt<IncomeCategoryModel> receipt = new KRequestReceipt<IncomeCategoryModel>(updatded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);

                var result = new KRequestReceipt<IncomeCategoryModel>(victim);
                return handleError(result, ex);
            }
        }
    }
}