using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SqlLookupProxy.Models;

namespace SqlLookupProxy.Services
{
    public interface ILookupService
    {
        IEnumerable<dynamic> Execute(LookupDefinition lookup, IDictionary<string, string> replaceableValues);
    }

    public class LookupService : ILookupService
    {
        private readonly IConfiguration _configuration;

        public LookupService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IEnumerable<dynamic> Execute(LookupDefinition lookup, IDictionary<string, string> replaceableValues)
        {            
            var sqlToExecute = Replace(lookup.Sql, replaceableValues);
            using(var connection = new SqlConnection(_configuration.GetConnectionString(lookup.Name)))
            {
                connection.Open();
                return connection.Query(sqlToExecute);
            }
        }

        private static string Replace(string sql, IDictionary<string, string> replaceableValues)
        {
             var r = new Regex("{(.*?})");
            var matches = r.Matches(sql);
            var sb = new StringBuilder("");
            var start = 0;
            foreach (Match match in matches)
            {
                sb.Append(sql, start, match.Index - start);
                var expression = match.Value.Substring(1, match.Value.Length - 2);
                if (!replaceableValues.TryGetValue(expression, out var result))
                    throw new InvalidOperationException($"Unable to find replaceable value for \"{expression}\"");
                sb.Append(result); 
                start = match.Index + match.Length;
            }
            sb.Append(sql, start, sql.Length - start);

            return sb.ToString();
        }
    }
}