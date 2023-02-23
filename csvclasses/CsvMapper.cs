using CI_OOP_PayCalculator_SA_WPFT2.payrollclasses;
using CsvHelper.Configuration;


namespace CI_OOP_PayCalculator_SA_WPFT2.csvclasses
{
    public class CsvMapper
    {
        /// <summary>
        /// Mapping the properties of the payslip and pay calculator class to the relevant headers of the CSV Data
        /// </summary>
        public sealed class PaySlipMapper : ClassMap<PaySlip>
        {
            public PaySlipMapper()
            {
                Map(m => m.EmployeeId).Index(0);
                Map(m => m.FirstName).Index(1);
                Map(m => m.LastName).Index(2);
                Map(m => m.EmployeeType).Index(3);
                Map(m => m.HourlyRate).Index(4);
                Map(m => m.HasTaxThreshold).Index(5);
                //Map(m => m.GrossPay).Index(6);
                //Map(m => m.TaxAmount).Index(7);
                //Map(m => m.SuperAmount).Index(8);
                //Map(m => m.NetPay).Index(9);
            }
        }

        public sealed class PayCalculatorMapper : ClassMap<PayCalculator>
        {
            public PayCalculatorMapper()
            {
                Map(m => m.IncomeRangeA).Index(0);
                Map(m => m.IncomeRangeB).Index(1);
                Map(m => m.TaxRateA).Index(2);
                Map(m => m.TaxRateB).Index(3);
            }
        }
    }
}
