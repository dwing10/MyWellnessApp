using MyWellnessApp.Models;
using MyWellnessApp.PresentationLayer.Views;
using MyWellnessApp.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyWellnessApp.PresentationLayer.ViewModels
{
    public class DashboardWindowViewModel : ObservableObject
    {
        #region fields

        private ObservableCollection<User> _users;
        private User _currentUser;
        private UserControl _leftUserControl;
        private UserControl _rightUserControl;

        private List<string> _currentUserTasks;
        private string _welcomeMessage;

        #endregion

        #region properties

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public UserControl LeftUserControl 
        { 
            get { return _leftUserControl; }
            set 
            {
                _leftUserControl = value;
                OnPropertyChanged(nameof(LeftUserControl));
            }
        }

        public UserControl RightUserControl
        {
            get { return _rightUserControl; }
            set
            {
                _rightUserControl = value;
                OnPropertyChanged(nameof(RightUserControl));
            }
        }

        public List<string> CurrentUserTasks
        {
            get { return _currentUserTasks; }
            set
            {
                _currentUserTasks = value;
                OnPropertyChanged(nameof(CurrentUserTasks));
            }
        }

        public string WelcomeMessage
        {
            get { return _welcomeMessage; }
            set
            {
                _welcomeMessage = value;
                OnPropertyChanged(nameof(WelcomeMessage));
            }
        }

        #endregion

        #region Constructors

        public DashboardWindowViewModel(User user)
        {
            _currentUser = user;
            _currentUserTasks = user.Task;
            BuildWelcomeMessage();
        }

        #endregion

        #region Commands

        public ICommand WorkoutViewCommand
        {
            get { return new RelayCommand(new Action<object>(ViewWorkouts)); }
        }

        public ICommand ProfileViewCommand 
        { 
            get { return new RelayCommand(new Action<object>(ViewProfilePage)); }
        }

        #endregion

        #region Methods

        private void ViewWorkouts(object obj) 
        {
            WorkoutViewModel workoutViewModel = new WorkoutViewModel(CurrentUser);
            UserWorkouts userWorkouts = new UserWorkouts();
            userWorkouts.DataContext = workoutViewModel;

            RightUserControl = userWorkouts;
        }

        /// <summary>
        /// sets content of user control to user's profile
        /// </summary>
        private void ViewProfilePage(object obj) 
        {
            ProfileViewModel profileViewModel = new ProfileViewModel(CurrentUser);
            UserProfile userProfile = new UserProfile();
            userProfile.DataContext = profileViewModel;

            LeftUserControl = userProfile;
        }

        /// <summary>
        /// builds out the title on the main dashboard
        /// </summary>
        private void BuildWelcomeMessage() 
        {
            WelcomeMessage = $"Welcome {CurrentUser.Name}!";
        }

        #endregion
    }
}
