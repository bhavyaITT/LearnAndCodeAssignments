using L_CAssignmentClasses.Exporters;
using L_CAssignmentClasses.Interfaces;
using L_CAssignmentClasses.Logging;
using L_CAssignmentClasses.Services;
using L_CAssignmentClasses.Statics;
using L_CAssignmentClasses.Store;
using L_CAssignmentClasses.Transform;
using Microsoft.Extensions.DependencyInjection;
using System;

class Program
{ 
    public static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddSingleton<IRecordStore, RecordStore>();
        services.AddSingleton<ILogger, FileLogger>();

        services.AddTransient<IFileReader,FileReader >();
        services.AddTransient<IDataParser,DataParser>();
        services.AddTransient<IDataValidator, DataValidator>();
        services.AddTransient<IDataTransformer, DataTransformer>();
        services.AddTransient<IStatics, Statics>();

        services.AddTransient<JsonExporter>();
        services.AddTransient<XmlExporter>();
        services.AddTransient<CSVExporter>();
        services.AddTransient<ExporterFactory>();
        services.AddTransient<ExportService>();
        services.AddTransient<DataProcessor>();

        var serviceProvider = services.BuildServiceProvider();

        var processor = serviceProvider.GetRequiredService<DataProcessor>();
        var statics = serviceProvider.GetRequiredService<IStatics>();
        var exportService = serviceProvider.GetRequiredService<ExportService>();

        string folderPath = @"D:\SampleApplication"; 
        string inputFileName = "input.csv";         
        string outputFileName = "output.csv";     
        string outputJsonFileName = "output_test.json";
        string outputXMLFileName = "output_test.xml";

        string inputFullPath = Path.Combine(folderPath, inputFileName);
        string outputFullPath = Path.Combine(folderPath, outputFileName);
        string outputJsonFullPath = Path.Combine(folderPath, outputJsonFileName);
        string outputXMLFullPath = Path.Combine(folderPath, outputXMLFileName);

        DataProcessor.GenerateSampleData(inputFullPath, 50);
        processor.Process(inputFullPath, outputFullPath);
        statics.DisplayStatistics();

        try
        {
            exportService.Export(outputJsonFullPath, "json");
            exportService.Export(outputXMLFullPath, "xml");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Export error: {ex.Message}");
        }
        var filtered = processor.FilterByValue(100);
        Console.WriteLine($"\nFiltered records: {filtered.Count}");
        Console.WriteLine("Processing completed!");
    }

} 
  