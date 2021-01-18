using Microsoft.Extensions.Configuration;
using Moq;
using SearchAggregator.Controllers;
using SearchAggregator.DataAccess.DTOs;
using SearchAggregator.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SearchAggregator.Tests
{
    public class SearchControllerTests
    {
        private SearchController _controller;

        private Mock<SearchService> _searchServiceMock;
        private Mock<IKeywordService> _keywordServiceMock;
        private Mock<IConfiguration> _configuration;

        public SearchControllerTests()
        {
            _searchServiceMock = new Mock<SearchService>();
            _keywordServiceMock = new Mock<IKeywordService>();
            _configuration = new Mock<IConfiguration>();

            _controller = new SearchController(_searchServiceMock.Object, _keywordServiceMock.Object, _configuration.Object);
        }

        [Fact]
        public async Task GetSearchResultsIfKeywordNotExists_SearchResources_CreateKeywordResources()
        {
            string searchText = "any string";
            List<ResourceDTO> resourceDTOs = new List<ResourceDTO>();
            _keywordServiceMock.Setup(r => r.GetResourcesByKeywordAsync(searchText))
                .Returns(Task.FromResult(resourceDTOs));

            var act = await _controller.GetSearchResultsAsync(searchText);

            _searchServiceMock.Verify(r => r.Search(searchText), Times.Once);
            _keywordServiceMock.Verify(r => r.CreateKeywordAsync(It.Is<KeywordDTO>(k => k.Word == searchText)), Times.Once);
        }

        [Fact]
        public async Task GetSearchResultsIfKeywordExists_DoesntSearchResources_DoesntCreateKeywordResources()
        {
            List<ResourceDTO> resourceDTOs = new List<ResourceDTO>() { new ResourceDTO() };
            _keywordServiceMock.Setup(r => r.GetResourcesByKeywordAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(resourceDTOs));

            var act = await _controller.GetSearchResultsAsync(It.IsAny<string>());

            _searchServiceMock.Verify(r => r.Search(It.IsAny<string>()), Times.Never);
            _keywordServiceMock.Verify(r => r.CreateKeywordAsync(It.IsAny<KeywordDTO>()), Times.Never);

            Assert.NotNull(act);
            Assert.IsAssignableFrom<List<ResourceDTO>>(act);
            Assert.Equal(resourceDTOs.Count, act.Count);
        }
    }
}
