using Google.Apis.Customsearch.v1.Data;
using Moq;
using SearchAggregator.DataAccess.DTOs;
using SearchAggregator.Services.APIClient;
using SearchAggregator.Services.CustomSearchers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SearchAggregator.Tests
{
    public class GoogleCustomSearchTests
    {
        private GoogleCustomSearcher _seacher;

        private Mock<IGoogleCustomsearchServiceHandler> _googleHandlerMock;
        public GoogleCustomSearchTests()
        {
            _googleHandlerMock = new Mock<IGoogleCustomsearchServiceHandler>();

            _seacher = new GoogleCustomSearcher(It.IsAny<string>(), It.IsAny<string>(), _googleHandlerMock.Object);
        }

        [Fact]
        public async Task SearchIfResourcesNotFound_ThrowException()
        {
            _googleHandlerMock.Setup(r => r.ExecuteAsync())
                .ReturnsAsync(new Search());

            Func<Task<List<ResourceDTO>>> act = async () => await _seacher.Search(It.IsAny<string>());
            Exception exception = await Assert.ThrowsAnyAsync<Exception>(act);
            Assert.Equal($"{nameof(GoogleCustomSearcher)} doesn't find anything", exception.Message);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public async Task SearchReturnsCorrectCountOfResources(int countResults)
        {
            List<Result> results = BuildTestListResults(countResults);
            var expect = new Search { Items = results };
            _googleHandlerMock.Setup(r => r.ExecuteAsync())
                .ReturnsAsync(expect);

            var act = await _seacher.Search(It.IsAny<string>());

            Assert.NotNull(act);
            Assert.Equal(results.Count, act.Count);
        }

        private List<Result> BuildTestListResults(int countResults)
        {
            List<Result> results = new List<Result>();
            for (int i = 0; i < countResults; i++)
                results.Add(new Result() { Link = "", Snippet = "", Title = "" });
            return results;
        }
    }
}
