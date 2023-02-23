using CI_OOP_PayCalculator_SA_WPFT2.csvclasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_OOP_PayCalculator_SA_WPFT2.payrollclasses
{

    /// <summary>
    /// A Class defining all properties of the PayCalculator and methods for calculating gross, super and tax amounts
    /// </summary>
    public class PayCalculator
    {
        public int IncomeRangeA { get; set; }
        public int IncomeRangeB { get; set; }
        public double TaxRateA { get; set; }
        public double TaxRateB { get; set; }



        // calculates gross income for each employee

        public static double CalculateGrossPay(int hourlyRate, double hourWorked)
        {
            double grossPay = Math.Round(hourlyRate * hourWorked, 2);
            return grossPay;
        }

        // Calculate super amount for each employee

        public static double CalculateSuper(double grossPay, double superRate)
        {
            double superAmount = Math.Round(grossPay * superRate, 2);
            return superAmount;
        }

        // Calculate super amount for each employee

        public static double CalculateNetPay(double grossPay, double superAmount, double taxAmount)
        {
            double netPay = Math.Round(grossPay - (superAmount + taxAmount), 2);
            return netPay;
        }

    }

    // PayCalculator class  With tax threshold 

    // and PayCalculator class for No tax threshold

    public class PayCalculatorNoThreshold : PayCalculator
    {
        public static double[] CalculateTax(double grossPay)
        {
            double[] taxRate = new double[2];

            string fileName = @"D:\VS 2022 Projects\CI_OOP_PayCalculator_SA_WPFT2\taxrate-withthreshold.csv";
            List<PayCalculator> importedThreshold = new List<PayCalculator>();
            importedThreshold = CsvImporter.ImportPayCalculator(fileName).ToList();

            for (int i = 0; i <= importedThreshold.Count; i++)
            {
                if (grossPay > importedThreshold[i].IncomeRangeA && grossPay < importedThreshold[i].IncomeRangeB)
                {
                    taxRate[0] = importedThreshold[i].TaxRateA;
                    taxRate[1] = importedThreshold[i].TaxRateB;
                    return taxRate;
                }
            }
            return taxRate;
        }
    }


    // Extends PayCalculator class handling With tax threshold
    public class PayCalculatorWithThreshold : PayCalculator
    {
        // Tax Calculation

        public static double[] CalculateTax(double grossPay)
        {
            double[] taxRate = new double[2];
            string fileName = @"D:\VS 2022 Projects\CI_OOP_PayCalculator_SA_WPFT2\taxrate-nothreshold.csv";
            List<PayCalculator> importedThreshold = new List<PayCalculator>();
            importedThreshold = CsvImporter.ImportPayCalculator(fileName).ToList();

            for (int i = 0; i <= importedThreshold.Count; i++)
            {
                if (grossPay > importedThreshold[i].IncomeRangeA && grossPay < importedThreshold[i].IncomeRangeB)
                {
                    taxRate[0] = importedThreshold[i].TaxRateA;
                    taxRate[1] = importedThreshold[i].TaxRateB;
                    return taxRate;
                }
            }
            return taxRate;
        }
    }
}
