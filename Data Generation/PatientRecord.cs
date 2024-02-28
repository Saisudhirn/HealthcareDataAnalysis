using System;
using System.Collections.Generic;

namespace HealthcareDataAnalysis
{
    internal class PatientRecord
    {
        private static Random random = new Random();
        public int PatientID { get; private set; }
        public int Age { get; private set; }
        public string Gender { get; private set; }
        public string DiagnosisCode { get; set; } 
        public string TreatmentCode { get; set; } 
        public DateTime DateOfAdmission { get; private set; }
        public DateTime DateOfDischarge { get; private set; }
        public List<string> Comorbidities { get; private set; }
        public string TreatmentType { get; private set; }
        public decimal CostOfTreatment { get; private set; }

        // Constructor
        public PatientRecord()
        {
            AssignGeneralFields();
            AssignTreatmentDetails();
            AssignCodes();
        }

        // Method to assign general fields randomly
        private void AssignGeneralFields()
        {
            PatientID = random.Next(1000, 9999); // Assuming a range for Patient IDs
            Age = random.Next(1, 100); // Assuming Age range from 1 to 99
            Gender = random.Next(2) == 0 ? "Male" : "Female";
        }

        // Method to assign treatment details appropriately
        public void AssignTreatmentDetails()
        {
            // Example comorbidities and treatments
            string[] possibleComorbidities = new string[] { "Myocardial Infarction", "Stroke", "Pneumonia", "Appendicitis", "Fractures" };
            Comorbidities = new List<string> { possibleComorbidities[random.Next(possibleComorbidities.Length)] };

            switch (Comorbidities[0])
            {
                case "Myocardial Infarction":
                    TreatmentType = "Surgical";
                    CostOfTreatment = random.Next(20000, 50001); // $20,000 - $50,000
                    break;
                case "Stroke":
                    TreatmentType = "Pharmaceutical";
                    CostOfTreatment = random.Next(20000, 40001); // $20,000 - $40,000
                    break;
                case "Pneumonia":
                    TreatmentType = "Pharmaceutical";
                    CostOfTreatment = random.Next(10000, 20001); // $10,000 - $20,000
                    break;
                case "Appendicitis":
                    TreatmentType = "Surgical";
                    CostOfTreatment = random.Next(10000, 30001); // $10,000 - $30,000
                    break;
                case "Fractures":
                    TreatmentType = "Surgical";
                    CostOfTreatment = random.Next(15000, 30001); // $15,000 - $30,000
                    break;
                default:
                    TreatmentType = "Observation";
                    CostOfTreatment = 5000; // Default or minimal cost
                    break;
            }

            // Set admission and discharge dates within 2023
            int year = 2023;
            int month = random.Next(1, 13);
            DateOfAdmission = new DateTime(year, month, random.Next(1, DateTime.DaysInMonth(year, month) + 1));
            // Assuming a duration of stay from 1 to 14 days for simplicity
            DateOfDischarge = DateOfAdmission.AddDays(random.Next(1, 15));

            var comorbiditiesToTreatment = new Dictionary<string, (string Treatment, decimal Cost, int StayDuration)>
    {
        {"Myocardial Infarction", ("Surgical", 35000m, 14)},
        {"Stroke", ("Pharmaceutical", 25000m, 10)},
        {"Pneumonia", ("Observation", 15000m, 7)},
        {"Appendicitis", ("Surgical", 18000m, 5)},
        {"Fracture", ("Surgical", 20000m, 10)}
    };

            // Randomly select a comorbidity
            var comorbidityKeys = comorbiditiesToTreatment.Keys.ToList();
            var selectedComorbidity = comorbidityKeys[random.Next(comorbidityKeys.Count)];

            Comorbidities = new List<string> { selectedComorbidity };
            var treatmentInfo = comorbiditiesToTreatment[selectedComorbidity];

            TreatmentType = treatmentInfo.Treatment;
            CostOfTreatment = treatmentInfo.Cost;

            // Assign admission and discharge dates based on stay duration
            DateOfAdmission = new DateTime(2023, random.Next(1, 13), random.Next(1, 29)); // Random date in 2023
            DateOfDischarge = DateOfAdmission.AddDays(treatmentInfo.StayDuration);
        }

        
        private void AssignCodes()
        {
            // Updated dictionaries to include actual example codes
            var diagnosisCodes = new Dictionary<string, string>
    {
        {"Myocardial Infarction", "I21.9"}, // Acute myocardial infarction, unspecified
        {"Stroke", "I63.9"}, // Cerebral infarction, unspecified
        {"Pneumonia", "J18.9"}, // Pneumonia, unspecified organism
        {"Appendicitis", "K35.80"}, // Unspecified acute appendicitis
        {"Fracture", "S82.001A"} // Fracture of patella, unspecified knee, initial encounter for closed fracture
    };

            var treatmentCodes = new Dictionary<string, string>
    {
        // Using CPT codes as examples
        {"Surgical", new List<string> {"33533", "44970", "27530"}.OrderBy(_ => random.Next()).First()}, // Examples: Coronary artery bypass, Laparoscopy, surgical, appendectomy, Closed treatment of tibial fracture
        {"Pharmaceutical", new List<string> {"J0696", "J3301", "J1030"}.OrderBy(_ => random.Next()).First()}, // Examples: Injection, ceftriaxone sodium, Injection, triamcinolone acetonide, Injection, methylprednisolone acetate
        {"Observation", new List<string> {"99217", "99220", "99235"}.OrderBy(_ => random.Next()).First()} // Examples: Observation care discharge, Hospital observation service, Comprehensive observation services
    };

            // Assign a diagnosis code based on the primary comorbidity
            if (Comorbidities.Count > 0 && diagnosisCodes.ContainsKey(Comorbidities[0]))
            {
                DiagnosisCode = diagnosisCodes[Comorbidities[0]];
            }

            // Assign a treatment code based on the treatment type
            if (treatmentCodes.ContainsKey(TreatmentType))
            {
                TreatmentCode = treatmentCodes[TreatmentType];
            }
        }


    }
}
