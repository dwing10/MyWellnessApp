using MyWellnessApp.BusinessLayer;
using MyWellnessApp.Models;
using MyWellnessApp.PresentationLayer.Views;
using MyWellnessApp.PresentationLayer.Views.UserControls;
using MyWellnessApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyWellnessApp.PresentationLayer.ViewModels
{
    public class LoginWindowViewModel : ObservableObject
    {
        #region fields

        private MyWellnessAppBusiness _myWellnessAppBusiness;
        private UserControl _userControl;

        private string _inputUsername;
        private string _inputPassword;
        private string _newName;
        private string _newUsername;
        private string _newPassword;
        private string _verifyPassword;
        private string _isVisible;
        private string _message;

        #endregion

        #region Properties

        public UserControl UserControl
        {
            get { return _userControl; }
            set
            {
                _userControl = value;
                OnPropertyChanged(nameof(UserControl));
            }
        }

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

        public string NewName
        {
            get { return _newName; }
            set
            {
                _newName = value;
                OnPropertyChanged(nameof(NewName));
            }
        }

        public string NewUsername
        {
            get { return _newUsername; }
            set
            {
                _newUsername = value;
                OnPropertyChanged(nameof(NewUsername));
            }
        }

        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }

        public string VerifyPassword
        {
            get { return _verifyPassword; }
            set
            {
                _verifyPassword = value;
                OnPropertyChanged(nameof(VerifyPassword));
            }
        }

        public string IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
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

        public LoginWindowViewModel(MyWellnessAppBusiness myWellnessAppBusiness)
        {
            _myWellnessAppBusiness = myWellnessAppBusiness;
            InputPassword = "password";
            InputUsername = "dwing";
            IsVisible = "Visible";

            LoginView loginView = new LoginView
            {
                DataContext = this
            };
            _userControl = loginView;
        }

        #endregion

        #region Commands

        public ICommand LoginCommand
        {
            get { return new RelayCommand(new Action<object>(Login)); }
        }

        public ICommand LoginViewCommand
        {
            get { return new RelayCommand(new Action<object>(LoginView)); }
        }


        public ICommand RegisterCommand
        {
            get { return new RelayCommand(new Action<object>(Register)); }
        }

        public ICommand RegisterNewUserCommand
        {
            get { return new RelayCommand(new Action<object>(RegisterNewUser)); }
        }

        public ICommand ExitCommand
        {
            get { return new RelayCommand(new Action<object>(Exit)); }
        }


        #endregion

        #region Methods

        /// <summary>
        /// login to the wellness app by retrieving user
        /// </summary>
        private void Login(object obj)
        {
            MyWellnessAppBusiness myWellnessAppBusiness = new MyWellnessAppBusiness();
            //List<User> user = myWellnessAppBusiness.RetreiveAllUserFromDataPath();
            List<User> user = myWellnessAppBusiness.GetAllUsers();

            if (!String.IsNullOrEmpty(InputUsername) && !String.IsNullOrEmpty(InputPassword))
            {
                foreach (var u in user)
                {
                    try
                    {
                        if (u.UserName == InputUsername && u.Password == InputPassword)
                        {
                            DashboardWindowViewModel dashboardWindowViewModel = new DashboardWindowViewModel(myWellnessAppBusiness.GetUser(u.ID));
                            DashboardWindow dashboardWindow = new DashboardWindow();

                            dashboardWindow.DataContext = dashboardWindowViewModel;
                            dashboardWindow.Show();

                            if (obj is System.Windows.Window)
                            {
                                (obj as System.Windows.Window).Close();
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        string m = e.Message;
                        throw;
                    }

                }
            }
            else
            {
                Message = "PLEASE VERIFY THAT YOU HAVE ENTER THE CORRECT USERNAME AND PASSWORD";
            }

        }

        /// <summary>
        /// shows login user control
        /// </summary>
        private void LoginView(object obj)
        {
            Message = null;
            _userControl.Content = null;
            LoginView loginView = new LoginView
            {
                DataContext = this
            };
            IsVisible = "Visible";
            _userControl.Content = loginView;
        }

        /// <summary>
        /// displays register user control
        /// </summary>
        private void Register(object obj)
        {
            Message = null;
            _userControl.Content = null;
            RegisterView registerView = new RegisterView
            {
                DataContext = this
            };

            IsVisible = "Collapsed";
            UserControl.Content = registerView;
        }

        /// <summary>
        /// creates a new user
        /// </summary>
        private void RegisterNewUser(object obj)
        {
            User newUser = new User();

            if (!String.IsNullOrEmpty(NewName) && !String.IsNullOrEmpty(NewUsername) && !String.IsNullOrEmpty(NewPassword) && !String.IsNullOrEmpty(VerifyPassword))
            {
                newUser = CreateNewUser();
                if (newUser != null)
                {
                    if (newUser.Password == VerifyPassword)
                    {
                        _myWellnessAppBusiness.AddUser(newUser);

                        _userControl.Content = null;

                        LoginView loginView = new LoginView
                        {
                            DataContext = this
                        };
                        MessageBoxResult message = MessageBox.Show("You Have Successfully Registered!");
                        IsVisible = "Visible";
                        _userControl.Content = loginView;
                    }
                }
            }
            else
            {
                Message = "ENSURE ALL FIELDS ARE FILLED IN";
            }
        }

        /// <summary>
        /// creates new user to be added to the database
        /// </summary>
        private User CreateNewUser()
        {
            return new User
            {
                Name = NewName,
                UserName = NewUsername,
                Password = NewPassword
            };
        }

        /// <summary>
        /// exit the application
        /// </summary>
        private void Exit(object obj)
        {
            Environment.Exit(0);
        }

        #endregion

    }
}
