using Kuaminika.KobFlow.ExpenseCategoryService;
using Kuaniminka.KobFlow.ToolBox;
using Microsoft.AspNetCore.Mvc;

namespace Kuaminika.KobFlow.API.ExpenseCategory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseCategoryController : ControllerBase
    {
        IExpenseCategoryService service;
        IKLogTool logTool;

        public ExpenseCategoryController(IConfiguration configs)
        {
            KContainer container = new KContainer(configs);
            service = container.Service;
            logTool = container.LogTool;
        }

        [HttpGet(Name = "Get")]
        public KRequestReceipt<List<ExpenseCategoryModel>> Get()
        {
            List<ExpenseCategoryModel> merchants = new List<ExpenseCategoryModel>();
            try
            {
                merchants = service.GetAll();
                return new KRequestReceipt<List<ExpenseCategoryModel>>(merchants);
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);
                var result = new KRequestReceipt<List<ExpenseCategoryModel>>(merchants);
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
        public KRequestReceipt<ExpenseCategoryModel> Add(ExpenseCategoryModel addMe)
        {
            try
            {
                ExpenseCategoryModel recorded = service.Add(addMe);

                KRequestReceipt<ExpenseCategoryModel> receipt = new KRequestReceipt<ExpenseCategoryModel>(recorded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);
                var result = new KRequestReceipt<ExpenseCategoryModel>(addMe);
                return handleError(result, ex);

            }
        }


        [HttpPost(Name = "Delete")]
        public KRequestReceipt<ExpenseCategoryModel> Delete(ExpenseCategoryModel victim)
        {
            try
            {
                ExpenseCategoryModel recorded = service.Delete(victim);

                KRequestReceipt<ExpenseCategoryModel> receipt = new KRequestReceipt<ExpenseCategoryModel>(recorded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);

                var result = new KRequestReceipt<ExpenseCategoryModel>(victim);
                return handleError(result, ex);
            }
        }

        [HttpPost(Name = "Update")]
        public KRequestReceipt<ExpenseCategoryModel> Update(ExpenseCategoryModel victim)
        {
            try
            {
                ExpenseCategoryModel updatded = service.Update(victim);

                KRequestReceipt<ExpenseCategoryModel> receipt = new KRequestReceipt<ExpenseCategoryModel>(updatded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);

                var result = new KRequestReceipt<ExpenseCategoryModel>(victim);
                return handleError(result, ex);
            }
        }
    }
}