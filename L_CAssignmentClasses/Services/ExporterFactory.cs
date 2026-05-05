using L_CAssignmentClasses.Exporters;
using L_CAssignmentClasses.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_CAssignmentClasses.Services
{
    public class ExporterFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public ExporterFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IExporter Get(string format)
        {
            return format.ToLower() switch
            {
                "json" => _serviceProvider.GetRequiredService<JsonExporter>(),
                "xml" => _serviceProvider.GetRequiredService<XmlExporter>(),
                "csv" => _serviceProvider.GetRequiredService<CSVExporter>(),
                _ => throw new ArgumentException("Invalid format")
            };
        }
    }
}
