using MyWellnessApp.BusinessLayer;
using MyWellnessApp.Models;
using MyWellnessApp.PresentationLayer.Views;
using MyWellnessApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MyWellnessApp.PresentationLayer.ViewModels
{
    public class LoginWindowViewModel : ObservableObject
    {
        #region fields

        private string _inputUsername;
        private string _inputPassword;

        #endregion

        #region Properties

        public string InputUsername
        {
            get { return _inputUsername; }
            set 
            { 
                _inputUsername = value;
                OnPropertyChanged(nameof(InputUsername));
            }
        }

        public string InputPassword
        {
            get { return _inputPassword; }
            set 
            { 
                _inputPassword = value;
                OnPropertyChanged(nameof(InputPassword));
            }
        }

        #endregion

        #region Constructors

        public LoginWindowViewModel(MyWellnessAppBusiness myWellnessAppBusiness)
        {

        }

        #endregion

        #region Commands

        public ICommand LoginCommand
        {
            get { return new RelayCommand(new Action<object>(Login)); }
        }

        public ICommand ViewSelectionCommand
        {
            get { return new RelayCommand(new Action<object>(ViewSelection)); }
        }


        #endregion

        #region Methods

        private void Login(object obj)
        {
            MyWellnessAppBusiness myWellnessAppBusiness = new MyWellnessAppBusiness();
            List<User> user = myWellnessAppBusiness.RetreiveAllUserFromDataPath();

            if (!String.IsNullOrEmpty(InputUsername) && !String.IsNullOrEmpty(InputPassword))
            {
                foreach (var u in user)
                {
                    try
                    {
                        if (u.UserName == InputUsername && u.Password == InputPassword)
                        {
                            DashboardWindowViewModel dashboardWindowViewModel = new DashboardWindowViewModel(u);
                            DashboardWindow dashboardWindow = new DashboardWindow();

                            dashboardWindow.DataContext = dashboardWindowViewModel;
                            dashboardWindow.Show();

                        }
                    }
                    catch (Exception e)
                    {
                        string m = e.Message;
                        throw;
                    }

                }
            }

        }

        private void ViewSelection(object obj) 
        { 
            
        }

        #endregion

    }
}
