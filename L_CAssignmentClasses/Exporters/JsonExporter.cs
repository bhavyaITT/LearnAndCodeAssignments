using L_CAssignmentClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_CAssignmentClasses.Exporters
{
    public class JsonExporter:IExporter
    {
        private readonly IRecordStore _recordStore;
        private readonly ILogger _logger;
        public JsonExporter(IRecordStore recordStore, ILogger logger)
        {
            _recordStore = recordStore ?? throw new ArgumentNullException(nameof(recordStore));
            _logger = logger;
        }   
        public void Export(string path)
        {
            var lines = new List<string> { "[" };

            for (int i = 0; i < _recordStore.Records.Count; i++)
            {
                var r = _recordStore.Records[i];

                var line = $"{{ \"Id\": \"{r["id"]}\", \"Name\": \"{r["name"]}\", \"Value\": {r["value"]} }}";

                if (i < _recordStore.Records.Count - 1) line += ",";

                lines.Add(line);
            }

            lines.Add("]");

            File.WriteAllLines(path, lines);
            _logger.Log("JSON export complete");
        }
    }
}
