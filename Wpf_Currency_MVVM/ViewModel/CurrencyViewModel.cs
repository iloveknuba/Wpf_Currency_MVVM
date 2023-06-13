using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Wpf_Currency_MVVM.Model;

namespace Wpf_Currency_MVVM.ViewModel
{
    internal class CurrencyViewModel : DependencyObject
    {
        List<string> tier;
        List<string> searchElements;


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<string> tierList
        {
            get { return (List<string>)GetValue(tierListProperty); }
            set { SetValue(tierListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for tierList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty tierListProperty =
            DependencyProperty.Register("tierList", typeof(List<string>), typeof(CurrencyViewModel), new PropertyMetadata(null));

        async Task ConvertTierTaskToListAsync()
        {

            Task<List<string>> task = APIRequests.GetTopNCurrencies(10);
            var searchTask = APIRequests.GetSearchList();


            tier = await task;
           searchElements = await searchTask;

            tierList = tier;
            SearchList = searchElements;


        }

        #region CurrencyInfo

        private ObservableCollection<Market> _markets;
        public ObservableCollection<Market> MarketsCollection
        {
            get { return _markets; }
            set
            {
                _markets = value;
                OnPropertyChanged(nameof(MarketsCollection));
            }
        }

        private CurrencyInfo currencyProperties;
        public CurrencyInfo CurrencyProperties
        {
            get { return currencyProperties; }
            set
            {
                currencyProperties = value;
                OnPropertyChanged(nameof(currencyProperties));
            }
        }

        

        public ICommand ListBoxItemClickCommand { get; private set; }

      

        private async void ExecuteItemClickCommand(object item)
        {
            string currencyName = item as string;
          
            if (item != null)
            {
                
                string id = await APIRequests.GetCurrencyId(currencyName);

                CurrencyProperties = await APIRequests.GetCurrencyInfo(id);
                MarketsCollection = await APIRequests.GetCurrencyMarkets(id);
            }
        }


        #endregion

        #region Search

        public List<string> SearchList
        {
            get { return (List<string>)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("SearchList", typeof(List<string>), typeof(CurrencyViewModel), new PropertyMetadata(null));
        

        private void ApplyFilter()
        {
            FilteredSearchList = SearchList.Where(item => item.Contains(SearchFilter)).ToList();
           
        }

        private string searchFilter;
        public string SearchFilter
        {
            get { return searchFilter; }
            set
            {
                searchFilter = value;
                ApplyFilter();
                OnPropertyChanged(nameof(SearchFilter));
                OnPropertyChanged(nameof(filteredSearchList));
            }
        }

        private List<string> filteredSearchList;
        public List<string> FilteredSearchList 
        {
            get { return filteredSearchList; }
            set
            {
                filteredSearchList = value;
                OnPropertyChanged(nameof(filteredSearchList));
            }
        }


        #endregion





        public CurrencyViewModel()
        {

            ConvertTierTaskToListAsync();

            ListBoxItemClickCommand = new RelayCommand<string>(ExecuteItemClickCommand);

          


        }

      
    }
}
