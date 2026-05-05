using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_CAssignmentClasses.Services
{
    public class ExportService
    {
        private readonly ExporterFactory _factory;

        public ExportService(ExporterFactory factory)
        {
            _factory = factory;
        }

        public void Export(string path, string format)
        {
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(directory); 
            }

            var exporter = _factory.Get(format);
            exporter.Export(path);
        }
    }
}
