using L_CAssignmentClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_CAssignmentClasses.Statics
{
    public class Statics:IStatics
    {
        private readonly ILogger _logger;
        private readonly IRecordStore _recordStore;

        public Statics(ILogger logger, IRecordStore recordStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _recordStore = recordStore ?? throw new ArgumentNullException(nameof(recordStore));
        }   

        public void CalculateStatistics()
        {
            _logger.Log("Calculating statistics...");

            var records = _recordStore.Records;
            var statistics = _recordStore.Statistics;

            statistics["total_records"] = records.Count;
            statistics["error_count"] = _recordStore.ErrorCount;

            double totalValue = 0;

            foreach (var record in records)
            {
                if (record.ContainsKey("value") &&
                    double.TryParse(record["value"]?.ToString(), out double value))
                {
                    totalValue += value;
                }
            }

            statistics["total_value"] = (int)totalValue;
            statistics["average_value"] = records.Count > 0
                ? (int)(totalValue / records.Count)
                : 0;

            _logger.Log($"Statistics calculated: {statistics.Count} metrics");
        }

        public void DisplayStatistics()
        {
            Console.WriteLine("\n=== Processing Statistics ===");
            foreach (var stat in _recordStore.Statistics)
            {
                Console.WriteLine($"{stat.Key}: {stat.Value}");
            }

            if (_recordStore.ErrorMessages.Count > 0)
            {
                Console.WriteLine("\n=== Errors ===");

                foreach (var error in _recordStore.ErrorMessages)
                {
                    Console.WriteLine($"- {error}");
                }
            }
        }
    }
}
