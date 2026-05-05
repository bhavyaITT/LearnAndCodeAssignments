using L_CAssignmentClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_CAssignmentClasses.Exporters
{
    public class CSVExporter:IExporter
    {
        private readonly IRecordStore _recordStore;
        private readonly ILogger _logger;

        public CSVExporter(IRecordStore recordStore, ILogger logger)
        {
            _recordStore = recordStore;
            _logger = logger;
        }

        public void Export(string path)
        {
            var lines = new List<string>
        {
            "ID,NAME,VALUE,DATE,DOUBLED,SQUARED"
        };

            lines.AddRange(_recordStore.Records.Select(r =>
                $"{r["id"]},{r["name"]},{r["value"]},{r["date"]},{r["DoubledValue"]},{r["SquaredValue"]}"
            ));

            File.WriteAllLines(path, lines);
            _logger.Log("CSV export complete");
        }
    }
}
