using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;

using BlazorApp.Models;

using CsvHelper;
using CsvHelper.Configuration;

namespace BlazorApp.Services
{
    public class ProjectService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<Project>> GetProjectsFromCsv(string filePath)
        {
            var response = await _httpClient.GetAsync(filePath);
            response.EnsureSuccessStatusCode();

            var csvContent = await response.Content.ReadAsStringAsync();
            using var reader = new StringReader(csvContent);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<ProjectMap>();
            var records = csv.GetRecords<Project>().ToList();
            return records;
        }
    }

    public class ProjectMap : ClassMap<Project>
    {
        public ProjectMap()
        {
            Map(m => m.Stage).Name("stage");
            Map(m => m.Title).Name("title");
            Map(m => m.Description).Name("description");
            Map(m => m.Url).Name("url");
            Map(m => m.Languages).Name("languages");
            Map(m => m.Role).Name("role");
            Map(m => m.Completed).Name("completed").TypeConverterOption.Format("MM/dd/yyyy");
        }
    }
}