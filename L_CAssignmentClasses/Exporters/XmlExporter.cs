using L_CAssignmentClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_CAssignmentClasses.Exporters
{
    public class XmlExporter:IExporter
    {
        private readonly IRecordStore _recordStore;
        private readonly ILogger _logger;

        public XmlExporter(IRecordStore recordStore, ILogger logger)
        {
            _recordStore = recordStore;
            _logger = logger;
        }
        public void Export(string path)
        {
            var lines = new List<string>
        {
            "<Records>"
        };

            foreach (var r in _recordStore.Records)
            {
                lines.Add($"<Record><Id>{r["id"]}</Id><Name>{r["name"]}</Name></Record>");
            }

            lines.Add("</Records>");

            File.WriteAllLines(path, lines);
            _logger.Log("XML export complete");
        }
    }
}
