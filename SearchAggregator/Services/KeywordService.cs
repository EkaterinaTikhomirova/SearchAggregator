using AutoMapper;
using SearchAggregator.DataAccess.DTOs;
using SearchAggregator.DataAccess.Models;
using SearchAggregator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchAggregator.Services
{
    public class KeywordService : IKeywordService
    {
        private readonly IKeywordRepository _keywordRepository;
        private readonly IResourceRepository _resourceRepository;
        private readonly SearchAggregatorContext _context;
        private readonly IMapper _mapper;

        public KeywordService(IKeywordRepository keywordRepository, IResourceRepository resourceRepository, SearchAggregatorContext context, IMapper mapper)
        {
            _keywordRepository = keywordRepository;
            _resourceRepository = resourceRepository;
            _context = context;
            _mapper = mapper;
        }
        
        public async Task CreateKeywordAsync(KeywordDTO keywordDTO)
        {           
            var keyword = _mapper.Map<Keyword>(keywordDTO);
            var resources = _mapper.Map<List<Resource>>(keywordDTO.Resources);
            keyword.KeywordResources = new List<KeywordResource>();
            foreach (Resource resource in resources)
            {
                var existedResourse = await _resourceRepository.UrlExists(resource.UrlAddress);
                if (existedResourse != default)
                    _keywordRepository.AddResourceToKeyword(new KeywordResource { Resource = existedResourse, Keyword = keyword });
                else
                    _keywordRepository.AddResourceToKeyword(new KeywordResource { Resource = resource, Keyword = keyword });
            }
            _keywordRepository.Add(keyword);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ResourceDTO>> GetResourcesByKeywordAsync(string word)
        {
            return _mapper.Map<List<ResourceDTO>>(await _keywordRepository.GetResourcesAsync(word));
        }
    }
}
