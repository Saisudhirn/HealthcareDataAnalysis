using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HealthcareDataAnalysis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Generate patient records
            List<PatientRecord> patientRecords = GeneratePatientRecords(1000);

            // File path to save the CSV
            string filePath = @"C:\Users\saisu\Desktop\Mis\newproject\patient_records.csv";

            // Save the records to a CSV file
            SaveRecordsToCsv(patientRecords, filePath);

            // Print the first 10 patient records to the console for a quick check
            foreach (var record in patientRecords.Take(10))
            {
                Console.WriteLine($"Patient ID: {record.PatientID}, Age: {record.Age}, Gender: {record.Gender}, " +
                                  $"Diagnosis Code: {record.DiagnosisCode}, Treatment Code: {record.TreatmentCode}, " +
                                  $"Date Of Admission: {record.DateOfAdmission.ToShortDateString()}, " +
                                  $"Date Of Discharge: {record.DateOfDischarge.ToShortDateString()}, " +
                                  $"Comorbidities: {String.Join(", ", record.Comorbidities)}, " +
                                  $"Treatment Type: {record.TreatmentType}, " +
                                  $"Cost Of Treatment: {record.CostOfTreatment}");
            }
        }

        private static List<PatientRecord> GeneratePatientRecords(int numberOfRecords)
        {
            var patientRecords = new List<PatientRecord>();
            for (int i = 0; i < numberOfRecords; i++)
            {
                patientRecords.Add(new PatientRecord()); // Constructor initializes all fields
            }
            return patientRecords;
        }

        private static void SaveRecordsToCsv(List<PatientRecord> records, string filePath)
        {
            var csvContent = new StringBuilder();
            csvContent.AppendLine("PatientID,Age,Gender,DiagnosisCode,TreatmentCode,DateOfAdmission,DateOfDischarge,Comorbidities,TreatmentType,CostOfTreatment");

            foreach (var record in records)
            {
                var comorbidities = string.Join(";", record.Comorbidities);
                csvContent.AppendLine($"{record.PatientID},{record.Age},{record.Gender},{record.DiagnosisCode},{record.TreatmentCode},{record.DateOfAdmission:yyyy-MM-dd},{record.DateOfDischarge:yyyy-MM-dd},\"{comorbidities}\",{record.TreatmentType},{record.CostOfTreatment}");
            }

            File.WriteAllText(filePath, csvContent.ToString());
        }
    }
}
