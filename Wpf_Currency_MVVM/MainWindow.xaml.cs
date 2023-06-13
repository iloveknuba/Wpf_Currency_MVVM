using System;
using System.Collections.Generic;
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
using Wpf_Currency_MVVM.ViewModel;

namespace Wpf_Currency_MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new CurrencyViewModel();
        }
        private void NavigateHome(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new TierPage();
            
        }


        private void Search(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new CurrencyPage();
        }
    }
}
