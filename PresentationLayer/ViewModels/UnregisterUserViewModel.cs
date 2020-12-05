using System;
using System.Collections.Generic;
using System.Text;
using MyWellnessApp.Utilities;
using MyWellnessApp.BusinessLayer;
using MyWellnessApp.Models;
using System.Windows.Input;
using System.Windows;

namespace MyWellnessApp.PresentationLayer.ViewModels
{
    public class UnregisterUserViewModel : ObservableObject
    {
        #region Fields

        private Window _window;
        private MyWellnessAppBusiness _myWellnessAppBusiness;
        private User _currentUser;

        private string _password;
        private string _message;

        #endregion

        #region Properties

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        #endregion

        #region Constructors

        public UnregisterUserViewModel(User user, Window window)
        {
            _myWellnessAppBusiness = new MyWellnessAppBusiness();
            _window = window;
            _currentUser = user;
        }

        #endregion

        #region Commands

        public ICommand UnregisterCommand
        {
            get { return new RelayCommand(new Action<object>(Unregister)); }
        }

        public ICommand ExitCommand
        {
            get { return new RelayCommand(new Action<object>(Close)); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// removes user's account
        /// </summary>
        private void Unregister(object obj)
        {
            Message = null;

            LoginWindowViewModel loginWindowViewModel = new LoginWindowViewModel(_myWellnessAppBusiness);
            LoginWindow loginWindow = new LoginWindow
            {
                DataContext = loginWindowViewModel
            };
            if (!String.IsNullOrEmpty(Password))
            {
                if (Password == _currentUser.Password)
                {
                    _myWellnessAppBusiness.DeleteUser(_currentUser.ID);

                    _window.Close();

                    loginWindow.Show();
                    MessageBoxResult message = MessageBox.Show("You Have Successfully Unregistered!");
                }
                else
                {
                    Message = "PASSWORD DID NOT MATCH";
                }
            }
            else
            {
                Message = "YOU MUST ENTER YOUR PASSWORD TO UNREGISTER";
            }

        }

        /// <summary>
        /// resets the UserControl's content
        /// </summary>
        private void Close(object obj)
        {
            if (obj is System.Windows.Controls.UserControl)
            {
                (obj as System.Windows.Controls.UserControl).Content = null;
            }
        }

        #endregion
    }
}
