using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace CI_OOP_PayCalculator_SA_WPFT2.payrollclasses
{
    /// <summary>
    /// A class to define all the properties of the PaySlip Class
    /// </summary>

    public class PaySlip
    {

        //Encapsulated by auto properties
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeType { get; set; }
        public int HourlyRate { get; set; }
        public string HasTaxThreshold { get; set; }
        public double GrossPay { get; set; }
        public double TaxAmount { get; set; }
        public double SuperAmount { get; set; }
        public double NetPay { get; set; }


        //Full name 
        public string FullName => $"{FirstName} {LastName}";
        public int HoursWorked { get; set; }

        /*public PaySlip(int id, string fName, string lName, string empType, int hoursWorked, int hrRate, 
         * string hasTaxThreshold, double grossPay, double taxAmount, double superAmount, double netPay)
        {
            EmployeeId = id;
            FirstName = fName;
            LastName = lName;
            EmployeeType = empType;
            HoursWorked = hoursWorked;
            HourlyRate= hrRate;
            HasTaxThreshold = hasTaxThreshold;
            GrossPay = grossPay;
            TaxAmount = taxAmount;
            SuperAmount = superAmount;
            NetPay = netPay;

            
        }

        internal static void Add()
        {
            throw new NotImplementedException();
        }*/
    }

}
