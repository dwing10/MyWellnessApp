using MyWellnessApp.Models;
using MyWellnessApp.PresentationLayer.Views;
using MyWellnessApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MyWellnessApp.PresentationLayer.ViewModels
{
    public class ProfileViewModel : ObservableObject
    {
        #region fields

        private User _currentUser;
        private string _profileTitle;

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

        public string ProfileTitle
        {
            get { return _profileTitle; }
            set
            {
                _profileTitle = value;
                OnPropertyChanged(nameof(ProfileTitle));
            }
        }

        #endregion

        #region Constructors

        public ProfileViewModel(User user)
        {
            CurrentUser = user;
            ProfileTitle = $"{CurrentUser.Name}'s Profile";
        }

        #endregion

        #region Commands

        public ICommand ReturnCommand 
        {
            get { return new RelayCommand(new Action<object>(Return)); }
        }

        #endregion

        #region Methods

        private void Return(object obj) 
        {
            if (obj is System.Windows.Controls.UserControl)
            {
                (obj as System.Windows.Controls.UserControl).Content = null;
            }
        }

        #endregion


    }
}
