using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SqlLookupProxy.Models;
using SqlLookupProxy.Repositories;
using SqlLookupProxy.Services;

namespace SqlLookupProxy.Controllers
{
    [Route("lookup")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class SqlLookupController : ControllerBase
    {
        private readonly ILookupDefinitionRepository _lookupDefinitionRepository;
        private readonly ILookupService _lookupService;

        public SqlLookupController(
            ILookupDefinitionRepository lookupDefinitionRepository,
            ILookupService lookupService)
        {
            _lookupDefinitionRepository = lookupDefinitionRepository;
            _lookupService = lookupService;
        }

        [HttpGet("{name}")]
        public IDictionary<string, dynamic> Get(string name)
        {
            var lookupDefinition = _lookupDefinitionRepository.Find(name);
            var array = _lookupService.Execute(lookupDefinition, new Dictionary<string,string>());
            var index = 0;
            return array.ToDictionary<dynamic, string, dynamic>(x=> index++.ToString(), v=>v);
            //return ConvertToDictionary(_lookupService.Execute(lookupDefinition, new Dictionary<string,string>()));
        }

        [HttpPost()]
        public IDictionary<string, dynamic> Post([FromBody] LookupQuery query)
        {
            var lookupDefinition = _lookupDefinitionRepository.Find(query.Name);
            var array = _lookupService.Execute(lookupDefinition, query.ReplaceableValues);
            var index = 0;
            return array.ToDictionary<dynamic, string, dynamic>(x=> index++.ToString(), v=>v);
            //return ConvertToDictionary(_lookupService.Execute(lookupDefinition, new Dictionary<string,string>()));
        }

        
        private static JObject ConvertToDictionary(IEnumerable<dynamic> array)
        {
            var result = new JObject();
            var index = 0;
            foreach (var item in array)
            {
                result.Add(index++.ToString(), item);
            }
            return result;
        }
    }
}
