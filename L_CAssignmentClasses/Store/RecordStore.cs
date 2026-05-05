using L_CAssignmentClasses.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_CAssignmentClasses.Store
{
    public class RecordStore : IRecordStore
    {
        public List<Dictionary<string, object>> Records { get; } = new();
        public int ErrorCount { get; set; }
        public List<string> ErrorMessages { get; } = new();
        public Dictionary<string, int> Statistics { get; } = new();
        public string DateFormat { get; set; } = "yyyy-MM-dd";
        public void AddRecord(Dictionary<string, object> record)
        {
            if (record != null)
            {
                Records.Add(record);
            }
        }

        public object Get(string key)
        {
            return Records.FirstOrDefault(r => r.ContainsKey(key))?.GetValueOrDefault(key);
        }

        public void AddError(string message)
        {
            ErrorCount++;
            ErrorMessages.Add(message);
        }
    }
}
