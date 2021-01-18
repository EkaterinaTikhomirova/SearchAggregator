using AutoMapper;
using Moq;
using SearchAggregator.DataAccess.DTOs;
using SearchAggregator.DataAccess.Mapper;
using SearchAggregator.DataAccess.Models;
using SearchAggregator.Repositories;
using SearchAggregator.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SearchAggregator.Tests
{
    public class KeywordServiceTests
    {
        private IKeywordService _service;

        private Mock<SearchAggregatorContext> _dbContextMock;
        private Mock<IKeywordRepository> _keywordRepositoryMock;
        private Mock<IResourceRepository> _resourceRepositoryMock; 

        public KeywordServiceTests()
        {
            _dbContextMock = new Mock<SearchAggregatorContext>();
            _keywordRepositoryMock = new Mock<IKeywordRepository>();
            _resourceRepositoryMock = new Mock<IResourceRepository>();

            Profile myProfile = new AutoMapperProfile();
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(myProfile)));
            _service = new KeywordService(_keywordRepositoryMock.Object, _resourceRepositoryMock.Object, _dbContextMock.Object, mapper);
        }

        [Fact]
        public async Task GetResourcesByKeywordAsyncReturnsListResourceDTO()
        {
            var resources = new List<Resource>() { new Resource(), new Resource() };
            _keywordRepositoryMock.Setup(r => r.GetResourcesAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((IEnumerable<Resource>)resources));

            var resourceDTO = await _service.GetResourcesByKeywordAsync("anystring");

            Assert.NotNull(resourceDTO);
            Assert.Equal(resources.Count, resourceDTO.Count);
        }

        [Fact]
        public async Task CreateKeywordAsyncIfResourceExists_DoesntAddResources()
        {
            var keyWords = new KeywordDTO()
            {
                Resources = new List<ResourceDTO>(){ new ResourceDTO() }
            };
            var resource = new Resource();
            _resourceRepositoryMock.Setup(r => r.UrlExists(It.IsAny<string>()))
                .Returns(Task.FromResult(resource));

            await _service.CreateKeywordAsync(keyWords);

            _keywordRepositoryMock.Verify(r => 
                r.AddResourceToKeyword(It.Is<KeywordResource>(k => k.Resource == resource)), Times.Once);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task CreateKeywordAsyncIfResourceNotExists_AddResources()
        {
            ResourceDTO resourceDTO = new ResourceDTO()
            {
                UrlAddress = "anyUrl"
            };
            var keyWords = new KeywordDTO()
            {
                Resources = new List<ResourceDTO>()
                {
                    resourceDTO
                }
            };
            _resourceRepositoryMock.Setup(r => r.UrlExists(It.IsAny<string>()))
                .Returns(Task.FromResult((Resource)default));

            await _service.CreateKeywordAsync(keyWords);

            _keywordRepositoryMock.Verify(r => r.AddResourceToKeyword(It.Is<KeywordResource>(k => k.Resource.UrlAddress == resourceDTO.UrlAddress)), Times.Once);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
