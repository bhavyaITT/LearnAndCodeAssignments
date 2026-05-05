using L_CAssignmentClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_CAssignmentClasses.Transform
{
    public class DataParser:IDataParser
    {
        private readonly ILogger _logger;
        private readonly IRecordStore _recordStore;
        public DataParser(ILogger logger, IRecordStore recordStore) 
        { 
            _logger = logger;
            _recordStore = recordStore;
        }
        public void Parse(List<string> lines)
        {
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(',');

                if (parts.Length >= 3)
                {
                    var record = new Dictionary<string, object>
                    {
                        {"id", parts[0].Trim()},
                        {"name", parts[1].Trim()},
                        {"value", double.TryParse(parts[2], out var v) ? v : 0},
                        {"date", parts.Length >= 4 && DateTime.TryParse(parts[3], out var d) ? d : null }
                    };
                    _recordStore.AddRecord(record);
                }
                else
                {
                    _recordStore.AddError($"Invalid line format: {line}");
                    _logger.Log($"ERROR: Invalid line format: {line}");
                }
            }
        }
    }
}
