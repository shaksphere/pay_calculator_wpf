using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CsvHelper;
using CI_OOP_PayCalculator_SA_WPFT2.csvclasses;
using CI_OOP_PayCalculator_SA_WPFT2.payrollclasses;
using System.Globalization;
using System.Dynamic;
using CsvHelper.Configuration;

namespace CI_OOP_PayCalculator_SA_WPFT2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Setting <param>fileName</param> to employee to csv for all reads
    /// creating a new list dictionary in PaySlip called PaySlipList
    /// 
    /// </summary>

    public partial class MainWindow : Window
    {

        public const string fileName = @"D:\VS 2022 Projects\CI_OOP_PayCalculator_SA_WPFT2\employee.csv";
        public List<PaySlip> importedRecords = new();
        public IDictionary<int, PaySlip> PaySlipList = new Dictionary<int, PaySlip>();



        ///<remarks>
        ///hoursworked variable created to store user input in textbox
        ///int hoursWorked;
        /// </remarks>
        

        /// <summary>
        ///  Setting the superRate as a fixed value currently 10.5%
        /// </summary>
        double superRate = 0.105;


  
        public MainWindow()
        {
            InitializeComponent();
            FillingDataGrid();

        }

        /// <summary>
        /// Method for when a row has been selected in the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmployeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            ///Current Context on THE UI
            var data = employeeDataGrid.DataContext as List<PaySlip>;
            int cellId = employeeDataGrid.SelectedIndex; // row index
            ///<remarks>
            ///MessageBox.Show(cellId.ToString());
            ///MessageBox.Show(data[cellId].name);
            ///</ remarks >

            /// <value>idUpdate.Text = data[cellId].EmployeeId.ToString();</value>
            idUpdate.Text = data[cellId].EmployeeId.ToString();

            updateFullName.Text = data[cellId].FullName;
            updateEmpType.Text = data[cellId].EmployeeType;

            ///<remarks>
            /// //var employeeId = data[cellId].EmployeeId;
            ///
            ///
            ///*var row = sender as DataGridRow;
            ///var emp = row.DataContext as Employee;*/
            ///MessageBox.Show($"The employee selected is {paySlip.EmployeeId + " " + paySlip.FirstName}");
            ///SelectEmployee():
            ///</ remarks >
        }

        /// <summary>
        /// Method to store the import of the csv so as to not show it in the main program 
        /// </summary>
        void FillingDataGrid()
        {
            importedRecords = CsvImporter.ImportSomeRecords(@"D:\VS 2022 Projects\CI_OOP_PayCalculator_SA_WPFT2\employee.csv").ToList();

            employeeDataGrid.DataContext = importedRecords;

        }


        /// <summary>
        /// 'Calculate' button logic 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_click_calculate(object sender, RoutedEventArgs e)
        {

            ///<remarks>
            ///double grossPay;
            ///double taxAmount;
            ///double superAmount;
            //double netPay;
            ///</ remarks>



            var data = employeeDataGrid.DataContext as List<PaySlip>;
            int cellId = employeeDataGrid.SelectedIndex; // row index

            data[cellId].HoursWorked = Convert.ToInt32(TextBoxHours.Text);
            MessageBox.Show($"user test input: {data[cellId].HoursWorked} hours");

            idUpdate.Text = data[cellId].EmployeeId.ToString();

            var selection = data[cellId].EmployeeId.ToString();

            /// Switch case to handle the different employees selected

            switch(selection)
            {
                /*case "0":

                    try
                    {
                       
                            double[] taxRate = new double[2];
                            data[cellId].GrossPay = PayCalculator.CalculateGrossPay(data[cellId].HourlyRate, data[cellId].HoursWorked);

                            if (data[cellId].HasTaxThreshold == "Y")
                            {
                                PayCalculatorWithThreshold withTaxThreshold = new PayCalculatorWithThreshold();
                                taxRate = PayCalculatorWithThreshold.CalculateTax(data[cellId].GrossPay);
                            ///<remarks>Should return 2 values TaxRateA and TaxRateB </remarks>
                        }
                        else if (data[cellId].HasTaxThreshold == "N")
                            {
                                PayCalculatorNoThreshold noTaxThreshold = new PayCalculatorNoThreshold();
                                taxRate = PayCalculatorNoThreshold.CalculateTax(data[cellId].GrossPay);
                            }
                            ///Tax Amount Calculation
                            data[cellId].TaxAmount = Math.Round((taxRate[0] * data[cellId].GrossPay) - taxRate[1], 2);

                            ///Super Calculation
                            data[cellId].SuperAmount = PayCalculator.CalculateSuper(data[cellId].GrossPay, superRate);

                            ///Net Pay Calculation
                            data[cellId].NetPay = PayCalculator.CalculateNetPay(data[cellId].GrossPay, data[cellId].SuperAmount, data[cellId].TaxAmount);

                            idCalc.Text = data[cellId].EmployeeId.ToString();
                            fullNameCalc.Text = data[cellId].FullName;
                            hrsWrkdCalc.Text = data[cellId].HoursWorked.ToString();
                            rateCalc.Text = data[cellId].HourlyRate.ToString();
                            taxTCalc.Text = data[cellId].HasTaxThreshold.ToString();
                            grossCalc.Text = data[cellId].GrossPay.ToString();
                            taxCalc.Text = data[cellId].TaxAmount.ToString();
                            superCalc.Text = data[cellId].SuperAmount.ToString();
                            netCalc.Text = data[cellId].NetPay.ToString();

                        ///<remarks>
                        /// unused code
                        /// 
                        // new payslip details //

                        /* data[cellId].EmployeeId,
                        data[cellId].FirstName,
                        data[cellId].LastName,
                        data[cellId].EmployeeType,
                        data[cellId].HoursWorked,
                        data[cellId].HourlyRate,
                        data[cellId].HasTaxThreshold,
                        data[cellId].GrossPay,
                        data[cellId].TaxAmount,
                        data[cellId].SuperAmount,
                        data[cellId].NetPay */



                        /* var paysliprow = new PaySlip
                         {
                              EmployeeId = data[cellId].EmployeeId,
                              FirstName = data[cellId].FirstName,
                              LastName = data[cellId].LastName,
                              EmployeeType = data[cellId].EmployeeType,
                              HoursWorked = data[cellId].HoursWorked,
                              HourlyRate = data[cellId].HourlyRate,
                              HasTaxThreshold = data[cellId].HasTaxThreshold,
                              GrossPay = data[cellId].GrossPay,
                              TaxAmount = data[cellId].TaxAmount,
                              SuperAmount = data[cellId].SuperAmount,
                              NetPay = data[cellId].NetPay,
                         };
                         PaySlipList.Add(paysliprow);*/


                        //var payslip = new Employee(data[cellId].EmployeeId, data[cellId].FirstName, data[cellId].LastName, Convert.ToInt32(TextBoxHours.Text), data[cellId].HourlyRate, data[cellId].HasTaxThreshold, grossPay, taxAmount, superAmount, netPay);

                        //PaySlipList.Add(cellId, payslip);

                        ///</ remarks >
                    /*}
                    catch (ArgumentException)
                    {
                        MessageBox.Show("A payslip for  " + data[cellId].FullName + " already exists.");

                        ///<summary>
                        ///This does not throw error and allows you to make multiple slips, even if same
                        ///MessageBox.Show("A payslip for  " + data[cellId].FullName + " already exists.");
                        /// </summary>
                    }
                    break; */
            
                case "1":
                    
                    try
                    {
                        double[] taxRate = new double[2];
                        data[cellId].GrossPay = PayCalculator.CalculateGrossPay(data[cellId].HourlyRate, data[cellId].HoursWorked);

                        if (data[cellId].HasTaxThreshold == "Y")
                        {
                            PayCalculatorWithThreshold withTaxThreshold = new PayCalculatorWithThreshold();
                            taxRate = PayCalculatorWithThreshold.CalculateTax(data[cellId].GrossPay);
                            ///Should return 2 values TaxRateA and TaxRateB
                        }
                        else if (data[cellId].HasTaxThreshold == "N")
                        {
                            PayCalculatorNoThreshold noTaxThreshold = new PayCalculatorNoThreshold();
                            taxRate = PayCalculatorNoThreshold.CalculateTax(data[cellId].GrossPay);
                        }
                        ///Tax Amount Calculation
                        data[cellId].TaxAmount = Math.Round((taxRate[0] * data[cellId].GrossPay) - taxRate[1], 2);

                        ///Super Calculation
                        data[cellId].SuperAmount = PayCalculator.CalculateSuper(data[cellId].GrossPay, superRate);

                        ///Net Pay Calculation
                        data[cellId].NetPay = PayCalculator.CalculateNetPay(data[cellId].GrossPay, data[cellId].SuperAmount, data[cellId].TaxAmount);

                        idCalc.Text = data[cellId].EmployeeId.ToString();
                        fullNameCalc.Text = data[cellId].FullName;
                        hrsWrkdCalc.Text = data[cellId].HoursWorked.ToString();
                        rateCalc.Text = data[cellId].HourlyRate.ToString();
                        taxTCalc.Text = data[cellId].HasTaxThreshold.ToString();
                        grossCalc.Text = data[cellId].GrossPay.ToString();
                        taxCalc.Text = data[cellId].TaxAmount.ToString();
                        superCalc.Text = data[cellId].SuperAmount.ToString();
                        netCalc.Text = data[cellId].NetPay.ToString();

                        ///<remarks>
                        //var payslip = new Employee(data[cellId].EmployeeId, data[cellId].FirstName, data[cellId].LastName, Convert.ToInt32(TextBoxHours.Text), data[cellId].HourlyRate, data[cellId].HasTaxThreshold, grossPay, taxAmount, superAmount, netPay);
                        //PaySlipList.Add(cellId, payslip);
                        ///</ remarks >

                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("A payslip for  " + data[cellId].FullName + " already exists.");
                    }
                    break;
                case "2":

                    try
                    {
                        double[] taxRate = new double[2];
                        data[cellId].GrossPay = PayCalculator.CalculateGrossPay(data[cellId].HourlyRate, data[cellId].HoursWorked);

                        if (data[cellId].HasTaxThreshold == "Y")
                        {
                            PayCalculatorWithThreshold withTaxThreshold = new PayCalculatorWithThreshold();
                            taxRate = PayCalculatorWithThreshold.CalculateTax(data[cellId].GrossPay);
                            ///Should return 2 values TaxRateA and TaxRateB
                        }
                        else if (data[cellId].HasTaxThreshold == "N")
                        {
                            PayCalculatorNoThreshold noTaxThreshold = new PayCalculatorNoThreshold();
                            taxRate = PayCalculatorNoThreshold.CalculateTax(data[cellId].GrossPay);
                        }
                        ///Tax Amount Calculation
                        data[cellId].TaxAmount = Math.Round((taxRate[0] * data[cellId].GrossPay) - taxRate[1], 2);

                        ///Super Calculation
                        data[cellId].SuperAmount = PayCalculator.CalculateSuper(data[cellId].GrossPay, superRate);

                        ///Net Pay Calculation
                        data[cellId].NetPay = PayCalculator.CalculateNetPay(data[cellId].GrossPay, data[cellId].SuperAmount, data[cellId].TaxAmount);

                        idCalc.Text = data[cellId].EmployeeId.ToString();
                        fullNameCalc.Text = data[cellId].FullName;
                        hrsWrkdCalc.Text = data[cellId].HoursWorked.ToString();
                        rateCalc.Text = data[cellId].HourlyRate.ToString();
                        taxTCalc.Text = data[cellId].HasTaxThreshold.ToString();
                        grossCalc.Text = data[cellId].GrossPay.ToString();
                        taxCalc.Text = data[cellId].TaxAmount.ToString();
                        superCalc.Text = data[cellId].SuperAmount.ToString();
                        netCalc.Text = data[cellId].NetPay.ToString();
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("A payslip for  " + data[cellId].FullName + " already exists.");
                    }
                    break;
                case "3":

                    try
                    {
                        double[] taxRate = new double[2];
                        data[cellId].GrossPay = PayCalculator.CalculateGrossPay(data[cellId].HourlyRate, data[cellId].HoursWorked);

                        if (data[cellId].HasTaxThreshold == "Y")
                        {
                            PayCalculatorWithThreshold withTaxThreshold = new PayCalculatorWithThreshold();
                            taxRate = PayCalculatorWithThreshold.CalculateTax(data[cellId].GrossPay);
                            ///Should return 2 values TaxRateA and TaxRateB
                        }
                        else if (data[cellId].HasTaxThreshold == "N")
                        {
                            PayCalculatorNoThreshold noTaxThreshold = new PayCalculatorNoThreshold();
                            taxRate = PayCalculatorNoThreshold.CalculateTax(data[cellId].GrossPay);
                        }
                        ///Tax Amount Calculation
                        data[cellId].TaxAmount = Math.Round((taxRate[0] * data[cellId].GrossPay) - taxRate[1], 2);

                        ///Super Calculation
                        data[cellId].SuperAmount = PayCalculator.CalculateSuper(data[cellId].GrossPay, superRate);

                        ///Net Pay Calculation
                        data[cellId].NetPay = PayCalculator.CalculateNetPay(data[cellId].GrossPay, data[cellId].SuperAmount, data[cellId].TaxAmount);

                        idCalc.Text = data[cellId].EmployeeId.ToString();
                        fullNameCalc.Text = data[cellId].FullName;
                        hrsWrkdCalc.Text = data[cellId].HoursWorked.ToString();
                        rateCalc.Text = data[cellId].HourlyRate.ToString();
                        taxTCalc.Text = data[cellId].HasTaxThreshold.ToString();
                        grossCalc.Text = data[cellId].GrossPay.ToString();
                        taxCalc.Text = data[cellId].TaxAmount.ToString();
                        superCalc.Text = data[cellId].SuperAmount.ToString();
                        netCalc.Text = data[cellId].NetPay.ToString();
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("A payslip for  " + data[cellId].FullName + " already exists.");
                    }
                    break;
                case "4":

                    try
                    {
                        double[] taxRate = new double[2];
                        data[cellId].GrossPay = PayCalculator.CalculateGrossPay(data[cellId].HourlyRate, data[cellId].HoursWorked);

                        if (data[cellId].HasTaxThreshold == "Y")
                        {
                            PayCalculatorWithThreshold withTaxThreshold = new PayCalculatorWithThreshold();
                            taxRate = PayCalculatorWithThreshold.CalculateTax(data[cellId].GrossPay);
                            ///Should return 2 values TaxRateA and TaxRateB
                        }
                        else if (data[cellId].HasTaxThreshold == "N")
                        {
                            PayCalculatorNoThreshold noTaxThreshold = new PayCalculatorNoThreshold();
                            taxRate = PayCalculatorNoThreshold.CalculateTax(data[cellId].GrossPay);
                        }
                        ///Tax Amount Calculation
                        data[cellId].TaxAmount = Math.Round((taxRate[0] * data[cellId].GrossPay) - taxRate[1], 2);

                        ///Super Calculation
                        data[cellId].SuperAmount = PayCalculator.CalculateSuper(data[cellId].GrossPay, superRate);

                        ///Net Pay Calculation
                        data[cellId].NetPay = PayCalculator.CalculateNetPay(data[cellId].GrossPay, data[cellId].SuperAmount, data[cellId].TaxAmount);

                        idCalc.Text = data[cellId].EmployeeId.ToString();
                        fullNameCalc.Text = data[cellId].FullName;
                        hrsWrkdCalc.Text = data[cellId].HoursWorked.ToString();
                        rateCalc.Text = data[cellId].HourlyRate.ToString();
                        taxTCalc.Text = data[cellId].HasTaxThreshold.ToString();
                        grossCalc.Text = data[cellId].GrossPay.ToString();
                        taxCalc.Text = data[cellId].TaxAmount.ToString();
                        superCalc.Text = data[cellId].SuperAmount.ToString();
                        netCalc.Text = data[cellId].NetPay.ToString();
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("A payslip for  " + data[cellId].FullName + " already exists.");
                    }
                    break;
                case "5":

                    try
                    {
                        double[] taxRate = new double[2];
                        data[cellId].GrossPay = PayCalculator.CalculateGrossPay(data[cellId].HourlyRate, data[cellId].HoursWorked);

                        if (data[cellId].HasTaxThreshold == "Y")
                        {
                            PayCalculatorWithThreshold withTaxThreshold = new PayCalculatorWithThreshold();
                            taxRate = PayCalculatorWithThreshold.CalculateTax(data[cellId].GrossPay);
                            ///Should return 2 values TaxRateA and TaxRateB
                        }
                        else if (data[cellId].HasTaxThreshold == "N")
                        {
                            PayCalculatorNoThreshold noTaxThreshold = new PayCalculatorNoThreshold();
                            taxRate = PayCalculatorNoThreshold.CalculateTax(data[cellId].GrossPay);
                        }
                        ///Tax Amount Calculation
                        data[cellId].TaxAmount = Math.Round((taxRate[0] * data[cellId].GrossPay) - taxRate[1], 2);

                        ///Super Calculation
                        data[cellId].SuperAmount = PayCalculator.CalculateSuper(data[cellId].GrossPay, superRate);

                        ///Net Pay Calculation
                        data[cellId].NetPay = PayCalculator.CalculateNetPay(data[cellId].GrossPay, data[cellId].SuperAmount, data[cellId].TaxAmount);

                        idCalc.Text = data[cellId].EmployeeId.ToString();
                        fullNameCalc.Text = data[cellId].FullName;
                        hrsWrkdCalc.Text = data[cellId].HoursWorked.ToString();
                        rateCalc.Text = data[cellId].HourlyRate.ToString();
                        taxTCalc.Text = data[cellId].HasTaxThreshold.ToString();
                        grossCalc.Text = data[cellId].GrossPay.ToString();
                        taxCalc.Text = data[cellId].TaxAmount.ToString();
                        superCalc.Text = data[cellId].SuperAmount.ToString();
                        netCalc.Text = data[cellId].NetPay.ToString();
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("A payslip for  " + data[cellId].FullName + " already exists.");
                    }
                    break;
                case "6":

                    try
                    {
                        double[] taxRate = new double[2];
                        data[cellId].GrossPay = PayCalculator.CalculateGrossPay(data[cellId].HourlyRate, data[cellId].HoursWorked);

                        if (data[cellId].HasTaxThreshold == "Y")
                        {
                            PayCalculatorWithThreshold withTaxThreshold = new PayCalculatorWithThreshold();
                            taxRate = PayCalculatorWithThreshold.CalculateTax(data[cellId].GrossPay);
                            ///Should return 2 values TaxRateA and TaxRateB
                        }
                        else if (data[cellId].HasTaxThreshold == "N")
                        {
                            PayCalculatorNoThreshold noTaxThreshold = new PayCalculatorNoThreshold();
                            taxRate = PayCalculatorNoThreshold.CalculateTax(data[cellId].GrossPay);
                        }
                        ///Tax Amount Calculation
                        data[cellId].TaxAmount = Math.Round((taxRate[0] * data[cellId].GrossPay) - taxRate[1], 2);

                        ///Super Calculation
                        data[cellId].SuperAmount = PayCalculator.CalculateSuper(data[cellId].GrossPay, superRate);

                        ///Net Pay Calculation
                        data[cellId].NetPay = PayCalculator.CalculateNetPay(data[cellId].GrossPay, data[cellId].SuperAmount, data[cellId].TaxAmount);

                        idCalc.Text = data[cellId].EmployeeId.ToString();
                        fullNameCalc.Text = data[cellId].FullName;
                        hrsWrkdCalc.Text = data[cellId].HoursWorked.ToString();
                        rateCalc.Text = data[cellId].HourlyRate.ToString();
                        taxTCalc.Text = data[cellId].HasTaxThreshold.ToString();
                        grossCalc.Text = data[cellId].GrossPay.ToString();
                        taxCalc.Text = data[cellId].TaxAmount.ToString();
                        superCalc.Text = data[cellId].SuperAmount.ToString();
                        netCalc.Text = data[cellId].NetPay.ToString();
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("A payslip for  " + data[cellId].FullName + " already exists.");
                    }
                    break;
                case "7":

                    try
                    {
                        double[] taxRate = new double[2];
                        data[cellId].GrossPay = PayCalculator.CalculateGrossPay(data[cellId].HourlyRate, data[cellId].HoursWorked);

                        if (data[cellId].HasTaxThreshold == "Y")
                        {
                            PayCalculatorWithThreshold withTaxThreshold = new PayCalculatorWithThreshold();
                            taxRate = PayCalculatorWithThreshold.CalculateTax(data[cellId].GrossPay);
                            ///Should return 2 values TaxRateA and TaxRateB
                        }
                        else if (data[cellId].HasTaxThreshold == "N")
                        {
                            PayCalculatorNoThreshold noTaxThreshold = new PayCalculatorNoThreshold();
                            taxRate = PayCalculatorNoThreshold.CalculateTax(data[cellId].GrossPay);
                        }
                        ///Tax Amount Calculation
                        data[cellId].TaxAmount = Math.Round((taxRate[0] * data[cellId].GrossPay) - taxRate[1], 2);

                        ///Super Calculation
                        data[cellId].SuperAmount = PayCalculator.CalculateSuper(data[cellId].GrossPay, superRate);

                        ///Net Pay Calculation
                        data[cellId].NetPay = PayCalculator.CalculateNetPay(data[cellId].GrossPay, data[cellId].SuperAmount, data[cellId].TaxAmount);

                        idCalc.Text = data[cellId].EmployeeId.ToString();
                        fullNameCalc.Text = data[cellId].FullName;
                        hrsWrkdCalc.Text = data[cellId].HoursWorked.ToString();
                        rateCalc.Text = data[cellId].HourlyRate.ToString();
                        taxTCalc.Text = data[cellId].HasTaxThreshold.ToString();
                        grossCalc.Text = data[cellId].GrossPay.ToString();
                        taxCalc.Text = data[cellId].TaxAmount.ToString();
                        superCalc.Text = data[cellId].SuperAmount.ToString();
                        netCalc.Text = data[cellId].NetPay.ToString();
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("A payslip for  " + data[cellId].FullName + " already exists.");
                    }
                    break;

                default:
                    MessageBox.Show("Please select an employee in the list");
                    break;

            }

            ///<remarks> Notes from class
            ///
            //load your employees->find selection->use this on selection on your
            //calculate btn event -> populate a textbox with that selection
            //+ calculate logic.
            //
            // Add button event logic to save any altered UI elements
            // to a new .csv file using element reference
            /// </ remarks >
        }

        ///<remarks> Unused code
        ///
        /*public static void addRecord(int id, string fName, int hoursWorked, int hrRate,
          string hasTaxThreshold, double grossPay, double taxAmount, double superAmount, double netPay)
        {
            try
            {
                using (System.)
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("Not happy Jan :", ex);
            }
        }*/
        ///</remarks>




        /// <summary>
        /// 'Save' Button logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_click_save(object sender, RoutedEventArgs e)
        {
            var data = employeeDataGrid.DataContext as List<PaySlip>;
            int cellId = employeeDataGrid.SelectedIndex; // row index

            idCalc.Text = data[cellId].EmployeeId.ToString();
            fullNameCalc.Text = data[cellId].FullName;
            hrsWrkdCalc.Text = data[cellId].HoursWorked.ToString();
            rateCalc.Text = data[cellId].HourlyRate.ToString();
            taxTCalc.Text = data[cellId].HasTaxThreshold.ToString();
            grossCalc.Text = data[cellId].GrossPay.ToString();
            taxCalc.Text = data[cellId].TaxAmount.ToString();
            superCalc.Text = data[cellId].SuperAmount.ToString();
            netCalc.Text = data[cellId].NetPay.ToString();
            
            /// Creating a new record to store the currently selected, updated employee payslip

            dynamic saveRecord = new ExpandoObject();
            saveRecord.id = idCalc.Text;
            saveRecord.fName = data[cellId].FirstName;
            saveRecord.lName = data[cellId].LastName;
            saveRecord.empType = data[cellId].EmployeeType;
            saveRecord.hrly = rateCalc.Text;
            saveRecord.htt = taxTCalc.Text;
            saveRecord.gross = grossCalc.Text;
            saveRecord.tax = taxCalc.Text;
            saveRecord.super = superCalc.Text;
            saveRecord.net = netCalc.Text;
            saveRecord.fullN = fullNameCalc.Text;
            saveRecord.hrsworked = hrsWrkdCalc.Text;

            var saveRecords = new List<dynamic>();
            saveRecords.Add(saveRecord);






            ///<remarks> Unused code
            ///
            //var myRecords = new List<PaySlip>();

            /// <summary>
            /// Saving the new payslip to a new CSV file with the employee full name and current datetime
            /// </summary>
            /// < /remarks>
            var saveFileName = @"D:\VS 2022 Projects\CI_OOP_PayCalculator_SA_WPFT2\Pay-" + idUpdate.Text + "-" + updateFullName.Text + DateTime.Now.ToFileTime() + ".csv";
            
            using (var writer = new StreamWriter(saveFileName))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                };
                using (var swriter = new StringWriter())
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteField("ID");
                    csv.WriteField("First Name");
                    csv.WriteField("Last Name");
                    csv.WriteField("Employee Type");
                    csv.WriteField("Rate/hr ($)");
                    csv.WriteField("Tax T-Hold (y/n)");
                    csv.WriteField("Gross ($)");
                    csv.WriteField("Tax ($)");
                    csv.WriteField("Super ($)");
                    csv.WriteField("Net ($)");
                    csv.WriteField("Full Name");
                    csv.WriteField("Hours Worked");

                    csv.NextRecord();
                    csv.WriteRecords(saveRecords);

                    ///<remarks> Unused code
                    ///
                    //csv.WriteRecords(PaySlipList);
                    //csv.WriteRecords(PaySlipList);

                    //csv.WriteHeader($"Emp Id","Full Name", "Hrs Wrked", "Rate/hr ($)","Tax T-Hold (y/n)","Gross ($)", "Tax ($)", "Super ($)", "Net ($)" );
                    //csv.WriteHeader<PaySlip>();
                    //writer.WriteLine(idCalc.Text + "," + fullNameCalc.Text + "," + hrsWrkdCalc.Text + "," +
                    //    rateCalc.Text + "," + taxTCalc.Text + "," + grossCalc.Text + "," + taxCalc.Text + "," + superCalc.Text + "," + netCalc.Text);
                    /// < /remarks>
                }
            }
            MessageBox.Show("New PaySlip has been saved!");

            ///<remarks> Unused code
            ///
            //var savePaySlip = new List<PaySlip>();
            //savePaySlip = list;
            //newPaySlipDataGrid.DataContext as List<PaySlip>;

            /*var saveFileName = @"D:\VS 2022 Projects\CI_OOP_PayCalculator_SA_WpfApp\Pay-" + DateTime.Now.ToFileTime() + ".csv";
            using (var writer = new StreamWriter(saveFileName))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(list);
                }
            }
            MessageBox.Show("New PaySlip has been saved!");*/
            /// < /remarks>
        }
    }
}
