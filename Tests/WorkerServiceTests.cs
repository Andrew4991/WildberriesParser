using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Parser.BL.Data.Interfaces;
using Parser.BL.Data.Models;
using Parser.BL.Data.Models.Options;
using Parser.BL.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests
    {
        private Mock<IParserService> _mockParserService;
        private Mock<IOptions<ProjectOptions>> _mockProjectOptions;

        [SetUp]
        public void Setup()
        {
            _mockParserService = new Mock<IParserService>();
            _mockParserService
                .Setup(x => x.GetProductsAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((new List<ProductInfo>
                {
                    new ProductInfo
                    {
                        Id = 1,
                        Brand = "Test",
                        Feedbacks = 1,
                        Price = 100,
                        Title = "Name"
                    }
                }).AsEnumerable()));

            _mockProjectOptions = new Mock<IOptions<ProjectOptions>>();
            _mockProjectOptions.Setup(x => x.Value).Returns(new ProjectOptions
            {
                AdsName = "Промо товар",
                ApiOptions = new ApiOptions(),
                SearchProductsUrl = string.Empty,
                HtmlParserOptions = new HtmlParserOptions(),
                InputFileName = "Inputs\\SearchKeys.txt",
                OutputFileName = "ouput.xlsx",
                UseApiParser = true
            });
        }

        [Test]
        public void GoodTest()
        {
            try
            {
                var worker = new WorkerService(_mockParserService.Object, _mockProjectOptions.Object, new FileService());
                worker.RunAsync().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Assert.Fail();
            }

            Assert.Pass();
        }
    }
}