using L_CAssignmentClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace L_CAssignmentClasses.Transform
{
    public class DataValidator: IDataValidator
    {
        private readonly ILogger _logger;
        private readonly IRecordStore _recordStore;
        public DataValidator(ILogger logger,IRecordStore recordStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _recordStore = recordStore ?? throw new ArgumentNullException(nameof(recordStore));
        }
        public void Validate()
        {
            var validRecords = new List<Dictionary<string, object>>();

            foreach (var record in _recordStore.Records)
            {
                bool isValid = true;
                bool hasnotId = !record.ContainsKey("id") || string.IsNullOrWhiteSpace(record["id"].ToString());
                bool hasnotName = !record.ContainsKey("name") || string.IsNullOrWhiteSpace(record["name"].ToString());
                

                if (hasnotId)
                {
                    isValid = false;
                    _recordStore.ErrorMessages.Add($"Record missing ID (Name='{record["name"].ToString() ?? "<null>"}')");
                }

                if (hasnotName)
                {
                    isValid = false;
                    _recordStore.ErrorMessages.Add($"Record {record["id"].ToString() ?? "<no-id>"} missing name");
                }

                if (record.ContainsKey("value"))
                {
                    if (!double.TryParse(record["value"].ToString(), out _))
                    {
                        isValid = false;
                        _recordStore.ErrorMessages.Add($"Record {record.GetValueOrDefault("id")} has invalid value");
                    }
                }

                if (isValid)
                {
                    validRecords.Add(record);
                }
                else
                {
                    _recordStore.ErrorCount++;
                }

            }
            _recordStore.Records.Clear();
            _recordStore.Records.AddRange(validRecords);

        }
    }
}
