//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.TestHost;
//using Microsoft.Extensions.Configuration;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using ChristmasMothers.Dal.EntityFramework.Extensions;

//namespace ChristmasMothers.Dal.EntityFramework.Tests
//{
//    [TestClass]
//    public class UnitTest1
//    {
//        [TestMethod]
//        public void TestMethod1()
//        {
//            var builder = new ConfigurationBuilder()
//                // .SetBasePath(env.ContentRootPath)
//                .AddJsonFile("appsettings.json", false, true)
//                .AddJsonFile($"appsettings.{EnvironmentVariables.AspNetCoreEnvironment}.json", true);
//            // .AddEnvironmentVariables("ChristmasMothers_");

//            var configuration = builder.Build();

//            var webHostBuilder = new WebHostBuilder();
//            webHostBuilder.ConfigureServices(services =>
//            {
//                services.AddDal(configuration);
//            });
//            var server = new TestServer(webHostBuilder);

            
//            //webHostBuilder.UseStartup<Startup>();
//            //var testServer = new TestServer(webHostBuilder);
//        }
//    }
//}
