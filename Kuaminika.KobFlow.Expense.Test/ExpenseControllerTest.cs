using Kuaminika.KobFlow.API.Expense;
using Kuaminika.KobFlow.API.Expense.Controllers;
using Kuaminika.KobFlow.ExpenseService;
using Kuaniminka.KobFlow.ToolBox;
using Microsoft.Extensions.Configuration;

namespace Kuaminika.KobFlow.Expense.Test
{
    public class ExpenseControllerTest
    {
        ExpenseController specimen;
        private IKLogTool logTool;

        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("testconfig.json", optional: false, reloadOnChange: false).Build();
            KContainer container = new KContainer(config);
            logTool = container.LogTool;
            specimen = new ExpenseController(config);

        }
        // test ExpenseSourceId = 57
        // test kobholder id =9
        // test ExpenseCategoryId =4
        ExpenseModel TestModel { get => new ExpenseModel { Description = $"from{GetType().Name}", CreatedDate = DateTime.Now, Amount = 100, OwnerId = 1, MerchantId = 57, KobHolderId = 9, CategoryId = 4 }; }
        [Test]
        public void TestGetAll()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;

            logTool.Log("starting test", methodName);


            var result = specimen.Get();

            logTool.logObject(result, methodName);

            logTool.Log("end test", methodName);

            Assert.IsTrue(result.Error == false);
        }


        [Test]
        public void TestAdding()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;

            logTool.Log("starting test", methodName);


            var result = specimen.Get();
            int amount = result.Subject.Count();

            logTool.Log($"amount before:{amount}", methodName);
           
            var testModel = TestModel;
            testModel.Description = $"from{methodName}-{amount}";
            var addResult = specimen.Add(testModel);

            logTool.logObject(addResult);


            result = specimen.Get();
            int afterAmount = result.Subject.Count;
            logTool.Log($"amount after:{afterAmount}", methodName);

            logTool.logObject(result, methodName);

            logTool.Log("end test", methodName);

            Assert.IsTrue(afterAmount > amount && addResult.Error == false);
        }

        [Test]
        public void TestGetRemoving()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;

            logTool.Log("starting test", methodName);

            var result = specimen.Get();
            int amount = result.Subject.Count();

            logTool.Log($"amount before:{amount}", methodName);

            var testModel = TestModel;
            testModel.Description = $"from{methodName}-{amount}";
            specimen.Add(testModel);
            result = specimen.Get();
            amount = result.Subject.Count();
            if (amount == 0) Assert.Fail("Cannot proceed with this test, Add did not work for:TestGetRemovingMerchants");

            var transactionResult = specimen.Delete(result.Subject[amount - 1]);

            logTool.logObject(transactionResult);


            result = specimen.Get();
            int afterAmount = result.Subject.Count;
            logTool.Log($"amount after:{afterAmount}", methodName);

            logTool.logObject(result, methodName);

            logTool.Log("end test", methodName);
            Assert.IsTrue(afterAmount < amount && transactionResult.Error == false);
        }

        [Test]
        public void TestGetUpdateWithMalicious()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;

            logTool.Log("starting test", methodName);


            var result = specimen.Get();
            int amount = result.Subject.Count();
            var testModel = TestModel;
            testModel.Description = $"from{methodName}-{amount}";
            logTool.Log($"amount before:{amount}", methodName);

            if (amount == 0)
            {
                specimen.Add(testModel);
                amount = result.Subject.Count();
                if (amount == 0) Assert.Fail("Cannot proceed with this test, Add did not work for:TestGetUpdateMerchant");
            }
            var victim = result.Subject[0];
            string nameBefore = victim.Description;
            victim.Description =  $"updated' where id ={victim.Id};--";

            var transactionResult = specimen.Update(victim);

            logTool.logObject(transactionResult);


            result = specimen.Get();
            int afterAmount = result.Subject.Count;
            logTool.Log($"amount after:{afterAmount}", methodName);

            logTool.logObject(result, methodName);

            logTool.Log("end test", methodName);
            Assert.IsTrue(victim.Description != nameBefore && transactionResult.Error == false);
        }

        [Test]
        public void TestGetUpdate()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;

            logTool.Log("starting test", methodName);


            var result = specimen.Get();
            int amount = result.Subject.Count();
            var testModel = TestModel;
            testModel.Description = $"from{methodName}-{amount}";
            logTool.Log($"amount before:{amount}", methodName);

            if (amount == 0)
            {
                specimen.Add(testModel);
                amount = result.Subject.Count();
                if (amount == 0) Assert.Fail("Cannot proceed with this test, Add did not work for:TestGetUpdateMerchant");
            }
            var victim = result.Subject[0];
            string nameBefore = victim.Description;
            victim.Description = victim.Description + "updated";

            var transactionResult = specimen.Update(victim);

            logTool.logObject(transactionResult);


            result = specimen.Get();
            int afterAmount = result.Subject.Count;
            logTool.Log($"amount after:{afterAmount}", methodName);

            logTool.logObject(result, methodName);

            logTool.Log("end test", methodName);
            Assert.IsTrue(victim.Description != nameBefore && transactionResult.Error == false);
        }
    }
}