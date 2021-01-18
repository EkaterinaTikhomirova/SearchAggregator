using Moq;
using Newtonsoft.Json;
using SearchAggregator.DataAccess.DTOs;
using SearchAggregator.Services.APIClient;
using SearchAggregator.Services.CustomSearchers;
using SearchAggregator.Services.CustomSearchers.SeacherResponses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SearchAggregator.Tests
{
    public class BingCustomSeacherTests
    {
        private BingCustomSeacher _seacher;

        private Mock<IHttpClientHandler> _httpClientMock;
        public BingCustomSeacherTests()
        {
            _httpClientMock = new Mock<IHttpClientHandler>();

            _seacher = new BingCustomSeacher(It.IsAny<string>(), It.IsAny<string>(), _httpClientMock.Object);
        }

        [Theory]
        [InlineData("{ \"webPages\": {\"value\": []}}")]
        [InlineData("{ \"webPages\": {\"value\": [{\"name\": \" \", \"url\": \" \",\"snippet\": \" \"}]}}")]
        [InlineData("{ \"webPages\": {\"value\": [{\"name\": \" \", \"url\": \" \",\"snippet\": \" \"}, {\"name\": \" \", \"url\": \" \",\"snippet\": \" \"}]}}")]
        public async Task SearchIsNotSuccessStatusCode_ReturnsCorrectCountOfResources(string testContent)
        {
            var expect = JsonConvert.DeserializeObject<BingCustomSearchResponse>(testContent);
            _httpClientMock.Setup(r => r.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(testContent)
                });

            var act = await _seacher.Search(It.IsAny<string>());

            Assert.NotNull(act);
            Assert.Equal(expect.webPages.value.Length, act.Count);
        }

        [Theory]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.BadRequest)]
        public async Task SearchIsNotSuccessStatusCode_ThrowException(HttpStatusCode statusCode)
        {
            var expect = "{\"error\": {}}";
            _httpClientMock.Setup(r => r.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(expect)
                });

            Func<Task<List<ResourceDTO>>> act = async () => await _seacher.Search(It.IsAny<string>());
            Exception exception = await Assert.ThrowsAnyAsync<Exception>(act);
            Assert.Equal($"{nameof(BingCustomSeacher)} doesn't work", exception.Message);
        }
    }
}
