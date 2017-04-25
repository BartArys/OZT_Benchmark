using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace Benchmark.Benchmark
{
    public class Settings
    {
        private readonly XmlDocument doc;

        public IList<Database> Databases { get; set; }
        public IList<Query> Queries { get; set; }
        public int frequency;

        public Settings()
        {
            Databases = new List<Database>();
            Queries = new List<Query>();
            doc = new XmlDocument();
            doc.Load(@"c:\users\Bart Arys\documents\visual studio 2015\Projects\Benchmark\Benchmark\resources\Settings.xml");
            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("b", "bench");
            XmlNode benchmarkNode = doc.SelectSingleNode("b:benchmark", nsmgr);

            XmlNode databasesNode = benchmarkNode.SelectSingleNode("b:databases", nsmgr);
            Dictionary<string, Database> dbMap = new Dictionary<string, Database>();
            foreach (XmlNode databaseNode in databasesNode.ChildNodes)
            {
                string connection = databaseNode.Attributes["connection"].InnerText;
                string process = databaseNode.Attributes["process"].InnerText;
                string identifier = databaseNode.InnerText;
                Database db = new Database() { Connection = connection, Identifier = identifier, Processname = process };
                dbMap.Add(identifier, db);
                Databases.Add(db);
            }

            XmlNode queriesNode = benchmarkNode.SelectSingleNode("b:queries", nsmgr);

            frequency = int.Parse(queriesNode.Attributes["frequency"].InnerText);

            foreach (XmlNode queryNode in queriesNode.ChildNodes) {
                string query = Regex.Replace(queryNode.LastChild.InnerText, @"\s+", " ");
                string identifier = queryNode.Attributes["identifier"].InnerText;
                XmlNode dbmsesNode = queryNode.SelectSingleNode("b:dbmses", nsmgr);
                IList<Database> dbmses = new List<Database>();
                foreach (XmlNode dbmsNode in dbmsesNode.ChildNodes) {
                    dbmses.Add(dbMap[dbmsNode.InnerText]);
                }
                Queries.Add(new Query() { Databases = dbmses, Identifier = identifier, QueryString = query });
            }
        }

    }
}
