using System.Windows;

namespace ENSEK.TestHarness
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public HarnessViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
             
            this.ViewModel = new HarnessViewModel();
            this.DataContext = ViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ImportCustomers();
        }

        private void ImportCustomers()
        {
            this.ViewModel.ImportCustomers();
        }
    }
}