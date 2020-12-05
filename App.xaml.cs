using MyWellnessApp.BusinessLayer;
using MyWellnessApp.PresentationLayer;
using MyWellnessApp.PresentationLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MyWellnessApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sneder, StartupEventArgs e) 
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzYxNzQ0QDMxMzgyZTMzMmUzME02bVBzTHJpQTRVaHBDTFV6R3VjZktwNGJWZG1JUnZCVzBSWHp6djlDd2s9");
            MyWellnessAppBusiness myWellnessAppBusiness = new MyWellnessAppBusiness();

            LoginWindowViewModel loginWindowViewModel = new LoginWindowViewModel(myWellnessAppBusiness);
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.DataContext = loginWindowViewModel;
            loginWindow.Show();
        }
    }
}
