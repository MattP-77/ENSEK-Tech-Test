using ENSEK.Classes.BusinessClasses;
using ENSEK.Classes.CompositeClasses;
using ENSEK.Classes.ResponseClasses;
using OfficeOpenXml;
using System.Text.RegularExpressions;

namespace ENSEK.Classes.Helpers
{
    public static class Utilities
    {
        public static bool IsFileValidForProcessing(string fileName, string fixedName)
        {
            var validFileExtensions = ",.xls,.xlsx";
            string[] validExtensions = validFileExtensions.Split(',');

            var isValidExtension = validExtensions.Contains(Path.GetExtension(fileName));
            var isValidFileName = Path.GetFileNameWithoutExtension(fileName) == fixedName;

            return isValidExtension && isValidFileName;
        }

        public static bool ProcessCustomerFile(string filePath)
        {
            var package = new ExcelPackage(new FileInfo(filePath));
            if (package != null)
            {
                ExcelWorksheet customerDataSheet = package.Workbook.Worksheets[0];
                if (customerDataSheet != null)
                {
                    var totalRows = 0;
                    int rowCount = 2;
                    while (customerDataSheet.Cells["A" + rowCount].Value != null && !string.IsNullOrEmpty(customerDataSheet.Cells["A" + rowCount].Value.ToString()))
                    {
                        rowCount++;
                    }

                    totalRows = rowCount - 1;

                    return ProcessCustomerData(customerDataSheet, totalRows);
                }
            }

            return false;
        }

        public static ResultReponseEntity ProcessMeterReadingFile(MemoryStream fileToProcess)
        {
            ResultReponseEntity result = new ResultReponseEntity();
            var package = new ExcelPackage(fileToProcess);
            if (package != null)
            {
                ExcelWorksheet meterReadingDataSheet = package.Workbook.Worksheets[0];
                if (meterReadingDataSheet != null)
                {
                    var totalRows = 0;
                    int rowCount = 2;
                    while (meterReadingDataSheet.Cells["A" + rowCount].Value != null && !string.IsNullOrEmpty(meterReadingDataSheet.Cells["A" + rowCount].Value.ToString()))
                    {
                        rowCount++;
                    }

                    totalRows = rowCount - 1;

                    ProcessMeterReadingData(meterReadingDataSheet, result);
                }
            }

            return result;
        }

        private static void ProcessMeterReadingData(ExcelWorksheet meterReadingDataSheet, ResultReponseEntity resultEntity)
        {
            List<MeterReadingComposite> meterReadingsToSave = new List<MeterReadingComposite>();
            var currentRow = 2;

            while (meterReadingDataSheet.Cells["A" + currentRow].Value != null && !string.IsNullOrEmpty(meterReadingDataSheet.Cells["A" + currentRow].Value.ToString()))
            {
                string? customerAccountId =
                   meterReadingDataSheet.Cells["A" + currentRow].Value != null && !string.IsNullOrEmpty(meterReadingDataSheet.Cells["A" + currentRow].Value.ToString()) ?
                   meterReadingDataSheet.Cells["A" + currentRow].Value.ToString() :
                   string.Empty;

                string? meterReadingDateTime =
                   meterReadingDataSheet.Cells["B" + currentRow].Value != null && !string.IsNullOrEmpty(meterReadingDataSheet.Cells["B" + currentRow].Value.ToString()) ?
                   meterReadingDataSheet.Cells["B" + currentRow].Value.ToString() :
                   string.Empty;

                DateTime meterReadingDate;
                if (DateTime.TryParse(meterReadingDateTime, out meterReadingDate))
                {
                    var dateResult = "Parsed date: " + meterReadingDate.ToString("yyyy-MM-dd");
                }

                string? meterReadValue =
                   meterReadingDataSheet.Cells["C" + currentRow].Value != null && !string.IsNullOrEmpty(meterReadingDataSheet.Cells["C" + currentRow].Value.ToString()) ?
                   meterReadingDataSheet.Cells["C" + currentRow].Value.ToString() :
                   string.Empty;

                // Define a regular expression pattern for the format "NNNNN"
                string pattern = @"^\d{5}$";

                // Check if the number matches the pattern
                var isValidReading = false;
                int outMeterReadValue;
                if (int.TryParse(meterReadValue, out outMeterReadValue))
                {
                    isValidReading = Regex.IsMatch(meterReadValue, pattern);
                }

                // Check if the string length is less than or equal to 5
                //var readValidation = string.Empty;
                //if (meterReadValue.Length <= 5)
                //{
                //    // Attempt to parse the string as an integer
                //    if (int.TryParse(meterReadValue, out int parsedNumber))
                //    {
                //        readValidation = "Number is in correct format (up to 5 digits and not greater than 5).";
                //    }
                //    else
                //    {
                //        readValidation = "Invalid number format.";
                //    }
                //}
                //else
                //{
                //    readValidation = "Number is greater than 5 digits.";
                //}

                var newMeterReading = 
                    NewMeterReadingDetails(
                        customerAccountId, meterReadingDate, outMeterReadValue, isValidReading, meterReadValue);

                if (newMeterReading != null)
                {
                    meterReadingsToSave.Add(newMeterReading);
                }

                currentRow++;
            }

            if (meterReadingsToSave.Count > 0)
            {
                MeterReadingBusiness.Update(meterReadingsToSave/*.Where(mr => mr.IsValid).ToList()*/);

                resultEntity.FailedReadCount = meterReadingsToSave.Where(r => !r.IsValid).Count();
                resultEntity.SuccessfulReadCount = meterReadingsToSave.Where(r => r.IsValid).Count();
                resultEntity.TotalReadCount = meterReadingsToSave.Count;
                resultEntity.MessageText =
                    resultEntity.FailedReadCount > 0 && resultEntity.SuccessfulReadCount > 0 ?
                    "File partially uploaded." :
                    "File uploaded successfully.";
            }
        }

        private static bool ProcessCustomerData(ExcelWorksheet customerDataSheet, int totalRows)
        {   
            List<CustomerComposite> customersToSave = new List<CustomerComposite>();
            var currentRow = 2;

            while (customerDataSheet.Cells["A" + currentRow].Value != null && !string.IsNullOrEmpty(customerDataSheet.Cells["A" + currentRow].Value.ToString()))
            {
                string? customerAccountId =
                   customerDataSheet.Cells["A" + currentRow].Value != null && !string.IsNullOrEmpty(customerDataSheet.Cells["A" + currentRow].Value.ToString()) ?
                   customerDataSheet.Cells["A" + currentRow].Value.ToString() :
                   string.Empty;

                string? customerFirstName =
                   customerDataSheet.Cells["B" + currentRow].Value != null && !string.IsNullOrEmpty(customerDataSheet.Cells["B" + currentRow].Value.ToString()) ?
                   customerDataSheet.Cells["B" + currentRow].Value.ToString() :
                   string.Empty;

                string? customerLastName =
                   customerDataSheet.Cells["C" + currentRow].Value != null && !string.IsNullOrEmpty(customerDataSheet.Cells["C" + currentRow].Value.ToString()) ?
                   customerDataSheet.Cells["C" + currentRow].Value.ToString() :
                   string.Empty;

                var newCustomer = NewCustomer(customerAccountId, customerFirstName, customerLastName);

                if (newCustomer != null)
                {
                    customersToSave.Add(newCustomer);
                }

                currentRow++;
            }

            if (customersToSave.Count > 0)
            {
                return CustomerBusiness.Update(customersToSave);
            }

            return false;
        }

        private static CustomerComposite NewCustomer(string? accountId, string? firstName, string? lastName) 
        {
            return
                new CustomerComposite() 
                { 
                    Id = Guid.NewGuid(),
                    AccountId = int.Parse(accountId),
                    FirstName = firstName,
                    LastName = lastName
                };
        }

        private static MeterReadingComposite NewMeterReadingDetails(
            string? accountId, DateTime? meterReadingDateTime, int meterReadValue, bool validReading, string submittedValue)
        {
            return
                new MeterReadingComposite()
                {
                    Id = Guid.NewGuid(),
                    AccountId = int.Parse(accountId),
                    MeterReadingDateTime = meterReadingDateTime,
                    MeterReadValue = validReading ? meterReadValue : 0,
                    SubmittedValue = submittedValue,
                    IsValid = validReading
                };
        }
    }
}
