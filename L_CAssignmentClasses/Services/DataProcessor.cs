using L_CAssignmentClasses.Interfaces;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace L_CAssignmentClasses.Services
{
    public class DataProcessor
    {
        private readonly IFileReader _reader;
        private readonly IDataParser _parser;
        private readonly IDataValidator _validator;
        private readonly IDataTransformer _transformer;
        private readonly ILogger _logger;
        private readonly IStatics _statics;
        private readonly IRecordStore _recordStore;

        public DataProcessor(
        IFileReader reader,
        IDataParser parser,
        IDataValidator validator,
        IDataTransformer transformer,
        IStatics statics,
        ILogger logger,
        IRecordStore recordStore)
        {
            _reader = reader;
            _parser = parser;
            _validator = validator;
            _transformer = transformer;
            _logger = logger;
            _statics = statics;

            _logger.Log("DataProcessor initialized");
            _recordStore = recordStore;
        }

        public void Process(string inputPath, string outputPath)
        {
            try
            {
                _logger.Log($"Reading input file: {inputPath}");
                var rawData = _reader.Read(inputPath);
                _logger.Log($"Read {rawData.Count} lines");

                _logger.Log("Parsing data...");
                _parser.Parse(rawData);

                _logger.Log("Validating data...");
                _validator.Validate();

                _logger.Log("Transforming data...");
                _transformer.Transform();

                _logger.Log("calculating Statics..");
                _statics.CalculateStatistics();

                _logger.Log($"Writing output to: {outputPath}");
                writeOutput(outputPath);
            }
            catch(Exception ex)
            {
                _recordStore.ErrorCount++;
                _recordStore.ErrorMessages.Add($"Fatal error: {ex.Message}");
                _logger.Log($"FATAL ERROR: {ex.Message}");
                Console.WriteLine($"Processing failed: {ex.Message}");
            }
        }


        public static void GenerateSampleData(string filePath, int recordCount)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(directory); 
            }

            var lines = new List<string>();
            Random random = new Random();
            for (int i = 1; i <= recordCount; i++)
            {
                string id = $"ID{i:D4}";
                string name = $"Item{i}";
                double value = random.Next(10, 1000);
                DateTime date = DateTime.Now.AddDays(-random.Next(0, 365));
                lines.Add($"{id},{name},{value},{date:yyyy-MM-dd}");
            }
            File.WriteAllLines(filePath, lines);
            Console.WriteLine($"Generated {recordCount} sample records in {filePath}");
        }

        public void writeOutput(string outputpath)
        {
            var records = _recordStore.Records;
            var errorCount = _recordStore.ErrorCount;

            var outputLines = new List<string>();

            outputLines.Add("ID,NAME,VALUE,DATE,DOUBLED_VALUE,SQUARED_VALUE");

            foreach (var record in records)
            {
                var line =
                    $"{record.GetValueOrDefault("id")}," +
                    $"{record.GetValueOrDefault("name")}," +
                    $"{record.GetValueOrDefault("value")}," +
                    $"{record.GetValueOrDefault("date")}," +
                    $"{record.GetValueOrDefault("doubled_value")}," +
                    $"{record.GetValueOrDefault("squared_value")}";

                outputLines.Add(line);
            }

            File.WriteAllLines(outputpath, outputLines);

            int recordsProcessed = records.Count;

            _logger.Log($"Output written. {recordsProcessed} records processed");

            Console.WriteLine("Processing complete!");
            Console.WriteLine($"Records processed: {recordsProcessed}");
            Console.WriteLine($"Errors: {errorCount}");
        }

        public List<Dictionary<string, object>> FilterByValue(double minValue)
        {
            var filtered = new List<Dictionary<string, object>>();
            foreach (var record in _recordStore.Records)
            {
                if (record.ContainsKey("value"))
                {
                    double value = double.Parse(record["value"].ToString());
                    if (value >= minValue)
                    {
                        filtered.Add(record);
                    }
                }
            }
            _logger.Log($"Filtered {filtered.Count} records with value >= {minValue}");
            return filtered;

        }
    }
}
