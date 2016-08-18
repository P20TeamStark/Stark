using rentMyJunkUWP.Models;
using rentMyJunkUWP.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace rentMyJunkUWP.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        private void Image_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var source = sender as FrameworkElement;
            if (source != null) {
                Category category = source.DataContext as Category;
                if (category != null)
                {
                    MainPageViewModel viewModel = this.DataContext as MainPageViewModel;
                    //&$filter=tablename/fieldname+eq+'countryName' 
                    if (viewModel != null)
                    {
                        viewModel.DashboardUrl = string.Format("{0}&$filter=District/District+eq+'FD - 01'", viewModel.DashoardBaseUrl);
                    }
                }
            }
        }
    }
}
