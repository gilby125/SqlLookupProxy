namespace SqlLookupProxy.Models {
    public class LookupDefinition{

        // public LookupDefinition(string connectionString, string sql)
        // {
        //     Name = connectionString;
        //     Sql = sql;
        // }

        public string Name { get; set; }
        public string Sql { get; set; }
    }
}