using MyWellnessApp.BusinessLayer;
using MyWellnessApp.Models;
using MyWellnessApp.PresentationLayer.Views.UserControls;
using MyWellnessApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MyWellnessApp.PresentationLayer.ViewModels
{
    public class EditProfileViewModel : ObservableObject
    {
        #region Fields

        private MyWellnessAppBusiness _myWellnessAppBusiness;
        private User _currentUser;

        private string _successMessage;
        private string _failureMessage;
        private string _newPassword;
        private string _verifyPassword;
        private string _currentPassword;

        #endregion

        #region Properties


        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public string SuccessMessage
        {
            get { return _successMessage; }
            set
            {
                _successMessage = value;
                OnPropertyChanged(nameof(SuccessMessage));
            }
        }

        public string FailureMessage
        {
            get { return _failureMessage; }
            set
            {
                _failureMessage = value;
                OnPropertyChanged(nameof(FailureMessage));
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

        public string CurrentPassword
        {
            get { return _currentPassword; }
            set
            {
                _currentPassword = value;
                OnPropertyChanged(nameof(CurrentPassword));
            }
        }

        #endregion

        #region Constructors

        public EditProfileViewModel(User user)
        {
            _myWellnessAppBusiness = new MyWellnessAppBusiness();
            _currentUser = user;
        }

        #endregion

        #region Commands

        public ICommand SaveCommand
        {
            get { return new RelayCommand(new Action<object>(EditUserProfile)); }
        }

        public ICommand ClearCommand
        {
            get { return new RelayCommand(new Action<object>(Clear)); }
        }

        public ICommand ExitCommand
        {
            get { return new RelayCommand(new Action<object>(Close)); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// edits the selected task
        /// </summary>
        private void EditUserProfile(object obj)
        {
            EditProfile editProfile = new EditProfile
            {
                DataContext = this
            };
            if (CurrentUser != null)
            {
                if (CurrentPassword != null && NewPassword != null && VerifyPassword != null)
                {
                    if (CurrentUser.Password == CurrentPassword && NewPassword == VerifyPassword)
                    {
                        CurrentUser.Password = NewPassword;
                        _myWellnessAppBusiness.UpdateUser(CurrentUser);
                        FailureMessage = null;
                        SuccessMessage = "Profile Updated";
                        CurrentPassword = null;
                        NewPassword = null;
                        VerifyPassword = null;
                        if (obj is System.Windows.Controls.UserControl)
                        {
                            (obj as System.Windows.Controls.UserControl).Content = null;
                            (obj as System.Windows.Controls.UserControl).Content = editProfile;
                        }
                    }
                    else
                    {
                        SuccessMessage = null;
                        FailureMessage = "Please Verify Your Password";
                    }
                }
                else
                {
                    _myWellnessAppBusiness.UpdateUser(CurrentUser);
                    FailureMessage = null;
                    SuccessMessage = "Profile Updated";
                    CurrentPassword = null;
                    NewPassword = null;
                    VerifyPassword = null;
                    if (obj is System.Windows.Controls.UserControl)
                    {
                        (obj as System.Windows.Controls.UserControl).Content = null;
                        (obj as System.Windows.Controls.UserControl).Content = editProfile;
                    }
                }
            }
        }

        /// <summary>
        /// clears the form
        /// </summary>
        private void Clear(object obj)
        {
            EditProfile editProfile = new EditProfile
            {
                DataContext = this
            };
            SuccessMessage = null;
            FailureMessage = null;
            CurrentPassword = null;
            NewPassword = null;
            VerifyPassword = null;
            if (obj is System.Windows.Controls.UserControl)
            {
                (obj as System.Windows.Controls.UserControl).Content = editProfile;
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
