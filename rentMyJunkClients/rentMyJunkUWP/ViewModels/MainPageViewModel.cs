using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using rentMyJunkUWP.Models;

namespace rentMyJunkUWP.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
    //https://localhost:44316/home/dashboard
    private string dashboardBaseUrl = "http://rentmyjunk-staging.azurewebsites.net/home/dashboard";
    //private string dashboardBaseUrl = "https://localhost:44316/home/dashboard";

    public string DashoardBaseUrl
        {
            get { return dashboardBaseUrl; }
            set { Set(ref dashboardBaseUrl,value); }
        }


        private string dashboardUrl;

        public string DashboardUrl
        {
            get { return dashboardUrl; }
            set { Set(ref dashboardUrl, value); }
        }


        private List<Category> categories = new List<Category> {
            new Category() { id="Equipment", imageUri="ms-appx:///Assets/CategoryImages/equipment.jpg" },
            new Category() { id="Houseware", imageUri="ms-appx:///Assets/CategoryImages/housewares.jpg" },
            new Category() { id="Recreational", imageUri="ms-appx:///Assets/CategoryImages/recreationalequipment.jpg" },
            new Category() { id="Vehicles", imageUri="ms-appx:///Assets/CategoryImages/vehicles.jpg" },
            new Category() { id="Electronics", imageUri="ms-appx:///Assets/CategoryImages/electronics.jpg" },
            new Category() { id="Tools", imageUri="ms-appx:///Assets/CategoryImages/handtools.jpg" },
        };

        public List<Category> Categories
        {
            get { return categories; }
            set { Set(ref categories, value); }
        }

        public MainPageViewModel()
        {
            DashboardUrl = DashoardBaseUrl;
        }

    }
}

