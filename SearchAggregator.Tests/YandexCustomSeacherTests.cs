using Moq;
using SearchAggregator.DataAccess.DTOs;
using SearchAggregator.Services.APIClient;
using SearchAggregator.Services.CustomSearchers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using Xunit;

namespace SearchAggregator.Tests
{
    public class YandexCustomSeacherTests
    {
        private YandexCustomSeacher _seacher;

        private Mock<IHttpClientHandler> _httpClientMock;
        public YandexCustomSeacherTests()
        {
            _httpClientMock = new Mock<IHttpClientHandler>();

            _seacher = new YandexCustomSeacher(It.IsAny<string>(), It.IsAny<string>(), _httpClientMock.Object);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public async Task SearchIsSuccessStatusCode_ReturnsCorrectCountOfResources(int countResources)
        {
            var expect = BuildTestXmlResponse(countResources).OuterXml;
            _httpClientMock.Setup(r => r.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(expect)
                });

            var act = await _seacher.Search(It.IsAny<string>());

            Assert.NotNull(act);
            Assert.Equal(countResources, act.Count);
        }

        [Theory]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.BadRequest)]
        public async Task SearchIsNotSuccessStatusCode_ThrowException(HttpStatusCode statusCode)
        {
            var expect = "<response><error></error></response>";
            _httpClientMock.Setup(r => r.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(expect)
                });

            Func<Task<List<ResourceDTO>>> act = async () => await _seacher.Search(It.IsAny<string>());
            Exception exception = await Assert.ThrowsAnyAsync<Exception>(act);
            Assert.Equal($"{nameof(YandexCustomSeacher)} doesn't work", exception.Message);
        }

        private XmlDocument BuildTestXmlResponse(int countGroup)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlElement grouping = xDoc.CreateElement("grouping");
            xDoc.AppendChild(grouping);
            XmlElement xRoot = xDoc.DocumentElement;
            for (int i = 0; i < countGroup; i++)
            {
                XmlElement groupElem = xDoc.CreateElement("group");
                XmlElement docElem = xDoc.CreateElement("doc");
                XmlElement urlElem = xDoc.CreateElement("url");
                XmlElement titleElem = xDoc.CreateElement("title");
                XmlElement headlineElem = xDoc.CreateElement("headline");

                docElem.AppendChild(urlElem);
                docElem.AppendChild(titleElem);
                docElem.AppendChild(headlineElem);
                groupElem.AppendChild(docElem);
                grouping.AppendChild(groupElem);
            }
            return xDoc;
        }
    }
}
