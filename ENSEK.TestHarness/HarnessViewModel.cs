using ENSEK.Classes.Helpers;
using Microsoft.Win32;
using System.IO;

namespace ENSEK.TestHarness
{
    public class HarnessViewModel
    {
        public void ImportCustomers()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "All Files|*.*"; 

                if (openFileDialog.ShowDialog() == true)
                {
                    string filePath = openFileDialog.FileName;

                    // Or read file contents into byte array
                    byte[] fileBytes = File.ReadAllBytes(filePath);

                    if (fileBytes.Length > 0 && Utilities.IsFileValidForProcessing(openFileDialog.FileName, "Test_Accounts"))
                    {
                        Utilities.ProcessCustomerFile(openFileDialog.FileName);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
