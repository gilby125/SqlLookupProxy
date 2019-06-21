using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SqlLookupProxy.Models;

namespace SqlLookupProxy.Repositories
{
    public interface ILookupDefinitionRepository 
    {
        LookupDefinition Find(string name);
    }

    public class LookupDefinitionRepository : ILookupDefinitionRepository
    {
        private readonly IConfiguration _configuration;

        public LookupDefinitionRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public LookupDefinition Find(string name)
        {
            return _configuration.GetSection("Lookups").GetSection(name).Get<LookupDefinition>();
        }
    }
}