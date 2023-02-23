using CI_OOP_PayCalculator_SA_WPFT2.payrollclasses;
using CI_OOP_PayCalculator_SA_WPFT2.csvclasses;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using static CI_OOP_PayCalculator_SA_WPFT2.csvclasses.CsvMapper;

namespace CI_OOP_PayCalculator_SA_WPFT2.csvclasses
{
    public class CsvImporter
    {

        /// <summary>
        /// Import the payslip details file employee.csv and add to a list 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<PaySlip> ImportSomeRecords(string fileName)
        {
            var myRecords = new List<PaySlip>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                MissingFieldFound = null,
            };
            using (var reader = new StreamReader(fileName))
            {
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<PaySlipMapper>();
                    var records = csv.GetRecords<PaySlip>();
                    myRecords = records.ToList(); //adds all the rows to myRecords
                }
            }
            /* // This was adapted from APIE WPF - trying with my other wpf app to see if it reconciles the 1st Id not showing anymore 
             * var myRecords = new List<PaySlip>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                MissingFieldFound = null
            };
            using (var reader = new StreamReader(fileName))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<PaySlipMapper>();
                    int EmployeeId;
                    string FirstName;
                    string LastName;
                    string EmployeeType;
                    int HourlyRate;
                    string HasTaxThreshold;
                    //double GrossPay;
                    //double TaxAmount;
                    //double SuperAmount;
                    //double NetPay;

                    //start reading csv file
                    csv.Read();

                    while (csv.Read())
                    {
                        EmployeeId = csv.GetField<int>(0);
                        FirstName = csv.GetField<string>(1);
                        LastName = csv.GetField<string>(2);
                        EmployeeType = csv.GetField<string>(3);
                        HourlyRate = csv.GetField<int>(4);
                        HasTaxThreshold = csv.GetField<string>(5);
                        //GrossPay = csv.GetField<double>(6);
                        //TaxAmount = csv.GetField<double>(7);
                        //SuperAmount = csv.GetField<double>(8);
                        //NetPay = csv.GetField<double>(9);

                       myRecords.Add(CreateRecord(EmployeeId, FirstName, LastName, EmployeeType, HourlyRate, HasTaxThreshold));
                       // myRecords.Add(EmployeeId, FirstName, LastName, EmployeeType, HourlyRate, HasTaxThreshold, GrossPay, TaxAmount, SuperAmount, NetPay);
                    }
                }*/
        
            return myRecords;
        }

        /// <summary>
        /// Import the payslip calculator from file (csv) and add to a list 
        /// </summary>
        /// <param name="fileName">
        /// </param>
        public static List<PayCalculator> ImportPayCalculator(string fileName)
        {
            var myRecordsPc = new List<PayCalculator>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };
            using (var reader = new StreamReader(fileName))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<PayCalculatorMapper>();
                    int IncomeRangeA;
                    int IncomeRangeB;
                    double TaxRateA;
                    double TaxRateB;

                    //read csv
                    csv.Read();
                    while (csv.Read())
                    {
                        IncomeRangeA = csv.GetField<int>(0);
                        IncomeRangeB = csv.GetField<int>(1);
                        TaxRateA = csv.GetField<double>(2);
                        TaxRateB = csv.GetField<double>(3);
                        myRecordsPc.Add(CreateRecordPc(IncomeRangeA, IncomeRangeB, TaxRateA, TaxRateB));
                    }
                }
            }
            return myRecordsPc;

        }

        /// <summary>
        /// Function to create a record of PaySlip
        /// </summary>
        /// <param name="currentId"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="employeeType"></param>
        /// <param name="hourlyRate"></param>
        /// <param name="hasTaxThreshold"></param>
        /// <returns></returns>
        /// 

        public static PaySlip CreateRecord(int currentId, string firstName, string lastName, string employeeType, int hourlyRate, string hasTaxThreshold)
        // public static PaySlip CreateRecord(int currentId, string firstName, string lastName, string employeeType, int hoursWorked, int hourlyRate, string hasTaxThreshold, double grossPay, double taxAmount, double superAmount, double netPay)
        {
            PaySlip record = new PaySlip();

            record.EmployeeId = currentId;
            record.FirstName = firstName;
            record.LastName = lastName;
            record.EmployeeType = employeeType;
            //record.HoursWorked = hoursWorked;
            record.HourlyRate = hourlyRate;
            record.HasTaxThreshold = hasTaxThreshold;
            //record.GrossPay = grossPay;
            //record.TaxAmount = taxAmount;
            //record.SuperAmount = superAmount;
            //record.NetPay = netPay;


            return record;
        }


        /// <summary>
        /// Function to create a record of PaySlip
        /// </summary>
        /// <param name="irA"></param>
        /// <param name="irB"></param>
        /// <param name="trA"></param>
        /// <param name="trB"></param>
        /// <returns></returns>
        public static PayCalculator CreateRecordPc(int irA, int irB, double trA, double trB)
        {
            PayCalculator record = new PayCalculator();

            record.IncomeRangeA = irA;
            record.IncomeRangeB = irB;
            record.TaxRateA = trA;
            record.TaxRateB = trB;


            return record;
        }
    }
}
