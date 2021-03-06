﻿using MyWellnessApp.BusinessLayer;
using MyWellnessApp.Models;
using MyWellnessApp.PresentationLayer.Views;
using MyWellnessApp.PresentationLayer.Views.UserControls;
using MyWellnessApp.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyWellnessApp.PresentationLayer.ViewModels
{
    public class DashboardWindowViewModel : ObservableObject
    {
        #region fields

        private Window _window;
        private ObservableCollection<User> _users;
        private User _currentUser;
        private UserControl _leftUserControl;
        private UserControl _rightUserControl;

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
            BuildWelcomeMessage();
        }

        #endregion

        #region Commands

        public ICommand TaskViewCommand
        {
            get { return new RelayCommand(new Action<object>(ViewTasks)); }
        }

        public ICommand AddTaskCommand
        {
            get { return new RelayCommand(new Action<object>(AddTasks)); }
        }

        public ICommand EditTaskCommand
        {
            get { return new RelayCommand(new Action<object>(EditTasks)); }
        }

        public ICommand WorkoutViewCommand
        {
            get { return new RelayCommand(new Action<object>(ViewWorkouts)); }
        }

        public ICommand AddWorkoutCommand
        {
            get { return new RelayCommand(new Action<object>(AddWorkout)); }
        }

        public ICommand EditWorkoutCommand
        {
            get { return new RelayCommand(new Action<object>(EditWorkout)); }
        }

        public ICommand ProfileViewCommand
        {
            get { return new RelayCommand(new Action<object>(ViewProfilePage)); }
        }

        public ICommand EditProfileCommand
        {
            get { return new RelayCommand(new Action<object>(EditUserProfile)); }
        }

        public ICommand UnregisterCommand
        {
            get { return new RelayCommand(new Action<object>(UnregisterUser)); }
        }

        public ICommand LogoutCommand
        {
            get { return new RelayCommand(new Action<object>(Logout)); }
        }

        public ICommand HelpCommand
        {
            get { return new RelayCommand(new Action<object>(Help)); }
        }

        public ICommand CloseHelpCommand
        {
            get { return new RelayCommand(new Action<object>(CloseHelp)); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// view current user's tasks
        /// </summary>
        private void ViewTasks(object obj)
        {
            RightUserControl = null;

            if (RightUserControl == null)
            {
                UserTaskViewModel userTaskViewModel = new UserTaskViewModel(CurrentUser);
                UserTask userTask = new UserTask();
                userTask.DataContext = userTaskViewModel;

                RightUserControl = userTask;
                WelcomeMessage = null;
            }
            else
            {
                RightUserControl = null;
            }
        }

        /// <summary>
        /// shows the add task user control
        /// </summary>
        private void AddTasks(object obj)
        {
            LeftUserControl = null;

            if (LeftUserControl == null)
            {
                AddTaskViewModel addTaskViewModel = new AddTaskViewModel(CurrentUser);
                AddTask addTasks = new AddTask
                {
                    DataContext = addTaskViewModel
                };

                LeftUserControl = addTasks;
                WelcomeMessage = null;
            }
            else
            {
                LeftUserControl = null;
            }
        }

        /// <summary>
        /// edit current user's tasks
        /// </summary>
        private void EditTasks(object obj)
        {
            RightUserControl = null;

            if (RightUserControl == null)
            {
                EditTaskViewModel editTaskViewModel = new EditTaskViewModel(CurrentUser);
                EditTask editTask = new EditTask
                {
                    DataContext = editTaskViewModel
                };

                RightUserControl = editTask;
                WelcomeMessage = null;
            }
            else
            {
                RightUserControl = null;
            }
        }

        /// <summary>
        /// view current user's workouts
        /// </summary>
        private void ViewWorkouts(object obj)
        {
            RightUserControl = null;

            if (RightUserControl == null)
            {
                WorkoutViewModel workoutViewModel = new WorkoutViewModel(CurrentUser, this);
                UserWorkouts userWorkouts = new UserWorkouts();
                userWorkouts.DataContext = workoutViewModel;

                RightUserControl = userWorkouts;
                WelcomeMessage = null;
            }
            else
            {
                RightUserControl = null;
            }
        }

        /// <summary>
        /// shows the add workout user control
        /// </summary>
        private void AddWorkout(object obj)
        {
            LeftUserControl = null;

            if (LeftUserControl == null)
            {
                AddWorkoutViewModel addWorkoutViewModel = new AddWorkoutViewModel(CurrentUser);
                AddWorkout addWorkout = new AddWorkout();
                addWorkout.DataContext = addWorkoutViewModel;

                LeftUserControl = addWorkout;
                WelcomeMessage = null;
            }
            else
            {
                LeftUserControl = null;
            }
        }

        /// <summary>
        /// edit current user's workout
        /// </summary>
        private void EditWorkout(object obj)
        {
            PhysicalActivity activity = new PhysicalActivity();
            activity = null;
            SetProgressBar(activity);

            RightUserControl = null;

            if (RightUserControl == null)
            {
                EditWorkoutViewModel editWorkoutViewModel = new EditWorkoutViewModel(CurrentUser, this);
                EditWorkout editWorkout = new EditWorkout
                {
                    DataContext = editWorkoutViewModel
                };

                RightUserControl = editWorkout;
                WelcomeMessage = null;
            }
            else
            {
                RightUserControl = null;
            }
        }

        /// <summary>
        /// sets content of user control to user's profile
        /// </summary>
        private void ViewProfilePage(object obj)
        {
            LeftUserControl = null;

            if (LeftUserControl == null)
            {
                ProfileViewModel profileViewModel = new ProfileViewModel(CurrentUser);
                UserProfile userProfile = new UserProfile();
                userProfile.DataContext = profileViewModel;

                LeftUserControl = userProfile;
                WelcomeMessage = null;
            }
            else
            {
                LeftUserControl = null;
            }
        }

        private void EditUserProfile(object obj)
        {
            LeftUserControl = null;

            if (LeftUserControl == null)
            {
                EditProfileViewModel editProfileViewModel = new EditProfileViewModel(CurrentUser);
                EditProfile editProfile = new EditProfile
                {
                    DataContext = editProfileViewModel
                };

                LeftUserControl = editProfile;
                WelcomeMessage = null;
            }
            else
            {
                LeftUserControl = null;
            }
        }

        /// <summary>
        /// unregisters user
        /// </summary>
        private void UnregisterUser(object obj)
        {
            if (obj is Window)
            {
                _window = (obj as Window);
            }

            RightUserControl = null;
            if (RightUserControl == null)
            {
                UnregisterUserViewModel unregisterUserViewModel = new UnregisterUserViewModel(CurrentUser, _window);
                UnregisterUser unregister = new UnregisterUser
                {
                    DataContext = unregisterUserViewModel
                };

                RightUserControl = unregister;
            }
            else
            {
                RightUserControl = null;
            }
        }

        /// <summary>
        /// logs out of the app
        /// </summary>
        private void Logout(object obj)
        {
            MyWellnessAppBusiness myWellnessAppBusiness = new MyWellnessAppBusiness();
            LoginWindowViewModel loginWindowViewModel = new LoginWindowViewModel(myWellnessAppBusiness);
            LoginWindow loginWindow = new LoginWindow
            {
                DataContext = loginWindowViewModel
            };
            loginWindow.Show();
            if (obj is Window)
            {
                (obj as Window).Close();
            }
        }

        /// <summary>
        /// opens help window
        /// </summary>
        private void Help(object obj) 
        {
            HelpWindow helpWindow = new HelpWindow { DataContext = this };

            helpWindow.Show();
        }

        /// <summary>
        /// closes help window
        /// </summary>
        private void CloseHelp(object obj)
        {
            if (obj is Window)
            {
                (obj as Window).Close();
            }
        }

        /// <summary>
        /// sets the progress bar user control
        /// </summary>
        public void SetProgressBar(PhysicalActivity activity)
        {
            LeftUserControl = null;

            if (LeftUserControl == null && activity != null)
            {
                WorkoutProgressBarViewModel workoutProgressBar = new WorkoutProgressBarViewModel(CurrentUser, activity);
                WorkoutProgressBar progressBar = new WorkoutProgressBar()
                {
                    DataContext = workoutProgressBar
                };

                LeftUserControl = progressBar;
                WelcomeMessage = null;
            }
            else if (activity == null)
            {
                LeftUserControl = null;
            }
            else
            {
                LeftUserControl = null;
            }
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
