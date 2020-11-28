using MyWellnessApp.BusinessLayer;
using MyWellnessApp.Models;
using MyWellnessApp.PresentationLayer.Views;
using MyWellnessApp.PresentationLayer.Views.UserControls;
using MyWellnessApp.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MyWellnessApp.PresentationLayer.ViewModels
{
    public class EditWorkoutViewModel : ObservableObject
    {
        #region Fields

        private MyWellnessAppBusiness _myWellnessAppBusiness;
        private DashboardWindowViewModel _dashboardWindowViewModel;

        private User _currentUser;
        private ObservableCollection<PhysicalActivity> _currentUserWorkouts;
        private ObservableCollection<string> _category;
        private PhysicalActivity _selectedWorkout;

        private string _categoryToEdit;
        private string _workoutCategory;
        private string _message;

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

        public ObservableCollection<PhysicalActivity> CurrentUserWorkouts
        {
            get { return _currentUserWorkouts; }
            set
            {
                _currentUserWorkouts = value;
                OnPropertyChanged(nameof(CurrentUserWorkouts));
            }
        }

        public ObservableCollection<string> Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        public PhysicalActivity SelectedWorkout
        {
            get { return _selectedWorkout; }
            set
            {
                _selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout));
                ConvertCategoryToString();
                GenerateProgressBar();
            }
        }

        public string CategoryToEdit
        {
            get { return _categoryToEdit; }
            set
            {
                _categoryToEdit = value;
                OnPropertyChanged(nameof(CategoryToEdit));
            }
        }


        public string WorkoutCategory
        {
            get { return _workoutCategory; }
            set
            {
                _workoutCategory = value;
                OnPropertyChanged(nameof(WorkoutCategory));
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

        public EditWorkoutViewModel(User user, DashboardWindowViewModel dashboardWindowViewModel) 
        {
            _myWellnessAppBusiness = new MyWellnessAppBusiness();
            _currentUser = user;
            _dashboardWindowViewModel = dashboardWindowViewModel;
            _currentUserWorkouts = _currentUserWorkouts = new ObservableCollection<PhysicalActivity>(CurrentUser.PhysicalActivities);
            _category = new ObservableCollection<string>(Enum.GetNames(typeof(PhysicalActivity.ExerciseType)));
        }

        #endregion

        #region Commands

        public ICommand SaveCommand
        {
            get { return new RelayCommand(new Action<object>(EditUserWorkout)); }
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
        private void EditUserWorkout(object obj)
        {
            WorkoutViewModel workoutViewModel = new WorkoutViewModel(CurrentUser, _dashboardWindowViewModel);
            UserWorkouts userWokouts = new UserWorkouts
            {
                DataContext = workoutViewModel
            };
            //Task task = CreateTask();
            if (SelectedWorkout != null)
            {
                Enum.TryParse(CategoryToEdit, out PhysicalActivity.ExerciseType category);
                SelectedWorkout.TypeOfExercise = category;
                _myWellnessAppBusiness.DeleteExercise(CurrentUser, SelectedWorkout);
                _myWellnessAppBusiness.EditExercise(CurrentUser, SelectedWorkout);
                _currentUserWorkouts = new ObservableCollection<PhysicalActivity>(CurrentUser.PhysicalActivities);
                _dashboardWindowViewModel.SetProgressBar(SelectedWorkout = null);
                if (obj is System.Windows.Controls.UserControl)
                {
                    (obj as System.Windows.Controls.UserControl).Content = userWokouts;
                }
            }
        }

        /// <summary>
        /// clears the form
        /// </summary>
        private void Clear(object obj)
        {
            EditWorkout editWorkout = new EditWorkout
            {
                DataContext = this
            };
            Message = null;
            ResetInputBoxes();
            if (obj is System.Windows.Controls.UserControl)
            {
                (obj as System.Windows.Controls.UserControl).Content = editWorkout;
            }
        }

        /// <summary>
        /// resets the input boxes
        /// </summary>
        private void ResetInputBoxes()
        {
            SelectedWorkout = null;
        }

        /// <summary>
        /// converts the TypeOfExcercise into a string for display purposes
        /// </summary>
        private void ConvertCategoryToString()
        {
            WorkoutCategory = "";
            try
            {
                if (SelectedWorkout != null)
                {
                    WorkoutCategory = SelectedWorkout.TypeOfExercise.ToString();
                }
                else
                {
                    WorkoutCategory = "NONE";
                }
            }
            catch (Exception e)
            {
                string m = e.Message;
                throw;
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

        /// <summary>
        /// generates the progress bar
        /// </summary>
        private void GenerateProgressBar()
        {
            _dashboardWindowViewModel.SetProgressBar(SelectedWorkout);
        }

        #endregion
    }
}
