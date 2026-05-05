using L_CAssignmentClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_CAssignmentClasses.Transform
{
    public class DataTransformer:IDataTransformer
    {
        private readonly IRecordStore _recordStore;
        private readonly ILogger _logger;
        public DataTransformer(IRecordStore recordStore, ILogger logger)
        {
            _recordStore = recordStore ?? throw new ArgumentNullException(nameof(recordStore));
            _logger = logger;
        }
        public void Transform()
        {
            var records = _recordStore.Records;
            var dateFormat = _recordStore.DateFormat;

            foreach (var record in records)
            {
                try
                {
                    if (record.ContainsKey("name") && record["name"] != null)
                    {
                        record["name"] = record["name"].ToString().ToUpper();
                    }

                    if (record.ContainsKey("date") && record["date"] != null)
                    {
                        if (DateTime.TryParse(record["date"].ToString(), out DateTime date))
                        {
                            record["date"] = date.ToString(dateFormat);
                        }
                    }

                    if (record.ContainsKey("value") && record["value"] != null)
                    {
                        if (double.TryParse(record["value"].ToString(), out double value))
                        {
                            record["doubled_value"] = value * 2;
                            record["squared_value"] = value * value;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _recordStore.AddError($"Transformation error: {ex.Message}");
                }
            }

            _logger.Log("Transformation complete");

        }
    }
}
