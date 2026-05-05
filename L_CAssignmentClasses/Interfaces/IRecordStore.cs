using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_CAssignmentClasses.Interfaces
{
    public interface IRecordStore
    {
        object Get(string key);
        void AddError(string message);
        public int ErrorCount { get; set; }
        public List<string> ErrorMessages { get; } 
        List<Dictionary<string, object>> Records { get; }
        void AddRecord(Dictionary<string, object> record);
        public string DateFormat { get; set; }
        public Dictionary<string, int> Statistics { get; }
    }
}
