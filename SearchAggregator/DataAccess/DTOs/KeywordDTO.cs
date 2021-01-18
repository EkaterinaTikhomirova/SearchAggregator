using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchAggregator.DataAccess.DTOs
{
    public class KeywordDTO
    {
        public string Word { get; set; }
        public List<ResourceDTO> Resources { get; set; }
    }
}
