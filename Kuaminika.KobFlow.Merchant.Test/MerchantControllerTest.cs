using Kuaminika.KobFlow.API.Merchant;
using Kuaminika.KobFlow.API.Merchant.Controllers;
using Kuaminika.KobFlow.MerchantService;
using Kuaniminka.KobFlow.ToolBox;
using Microsoft.Extensions.Configuration;

namespace Kuaminika.KobFlow.Merchant.Test
{

    public class MerchantControllerTest
    {
        MerchantController specimen;
        private IKLogTool logTool;

        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("testconfig.json", optional: false, reloadOnChange: false).Build();
            KContainer container = new KContainer(config);
            logTool = container.LogTool;
            specimen = new MerchantController(config);
       
        }

        [Test]
        public void TestGetAllMerchants()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;

            logTool.Log("starting test",methodName);


            var result =  specimen.Get();

            logTool.logObject(result, methodName);

            logTool.Log("end test", methodName);

            Assert.IsTrue(result.Error == false);
        }


        [Test]
        public void TestAddingMerchants()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;

            logTool.Log("starting test", methodName);


            var result = specimen.Get();
            int amount = result.Subject.Count();

            logTool.Log($"amount before:{amount}", methodName);

            var addResult = specimen.Add(new MerchantModel { Name =$"from{methodName}-{amount}"});

            logTool.logObject(addResult);


            result = specimen.Get();
            int afterAmount = result.Subject.Count;
            logTool.Log($"amount after:{afterAmount}", methodName);

            logTool.logObject(result, methodName);

            logTool.Log("end test", methodName);

            Assert.IsTrue(afterAmount > amount && addResult.Error == false);
        }

        [Test]
        public void TestGetRemovingMerchants()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;

            logTool.Log("starting test", methodName);

            var result = specimen.Get();
            int amount = result.Subject.Count();

            logTool.Log($"amount before:{amount}", methodName);

         
                specimen.Add(new MerchantModel { Name = $"from{methodName}-{amount}" });
              result = specimen.Get();
                amount = result.Subject.Count();
                if (amount == 0) Assert.Fail("Cannot proceed with this test, Add did not work for:TestGetRemovingMerchants");
   
            var transactionResult = specimen.Delete(result.Subject[amount-1]);

            logTool.logObject(transactionResult);


            result = specimen.Get();
            int afterAmount = result.Subject.Count;
            logTool.Log($"amount after:{afterAmount}", methodName);

            logTool.logObject(result, methodName);

            logTool.Log("end test", methodName);
            Assert.IsTrue(afterAmount < amount && transactionResult.Error == false);
        }

        [Test]
        public void TestGetUpdateMerchant()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;

            logTool.Log("starting test", methodName);


            var result = specimen.Get();
            int amount = result.Subject.Count();

            logTool.Log($"amount before:{amount}", methodName);

            if (amount == 0)
            {
                specimen.Add(new MerchantModel { Name = $"from{methodName}-{amount}" });
                amount = result.Subject.Count();
                if (amount == 0) Assert.Fail("Cannot proceed with this test, Add did not work for:TestGetUpdateMerchant");
            }
            var victim = result.Subject[0];
            string nameBefore = victim.Name;
            victim.Name = victim.Name+ "updated";

            var transactionResult = specimen.Update(victim);

            logTool.logObject(transactionResult);


            result = specimen.Get();
            int afterAmount = result.Subject.Count;
            logTool.Log($"amount after:{afterAmount}", methodName);

            logTool.logObject(result, methodName);

            logTool.Log("end test", methodName);
            Assert.IsTrue(victim.Name!=nameBefore && transactionResult.Error == false);
        }
    }
}