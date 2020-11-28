using MyWellnessApp.Models;
using MyWellnessApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace MyWellnessApp.PresentationLayer.ViewModels
{
    public class WorkoutProgressBarViewModel : ObservableObject
    {

        #region Fields

        private PhysicalActivity _physicalActivity;
        private User _currentUser;

        private double _progress;

        private string _displayProgress;

        private bool _isIndeterminate;

        #endregion

        #region Properties

        public PhysicalActivity PhysicalActivity
        {
            get { return _physicalActivity; }
            set { _physicalActivity = value; }
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

        public double Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        public string DisplayProgress 
        {
            get { return _displayProgress; }
            set 
            {
                _displayProgress = value;
                OnPropertyChanged(nameof(DisplayProgress));
            }
        }

        public bool IsIndeterminate 
        {
            get { return _isIndeterminate; }
            set 
            {
                _isIndeterminate = value;
                OnPropertyChanged(nameof(IsIndeterminate));
            }
        }

        #endregion

        #region Constructors

        public WorkoutProgressBarViewModel(User user, PhysicalActivity activity)
        {
            _currentUser = user;
            _physicalActivity = activity;
            SetProgress();
        }

        #endregion

        #region Commands


        #endregion

        #region Methods

        private void SetProgress()
        {
            double weight = PhysicalActivity.Weight;
            double duration = PhysicalActivity.Duration;
            double goal = PhysicalActivity.Goal;

            double calculatedProgress;

            if (weight != 0 && goal != 0)
            {
                calculatedProgress = weight / goal;
                DisplayProgress = calculatedProgress.ToString("P0");
                Progress = calculatedProgress * 100;
                IsIndeterminate = false;
            }
            else if (weight == 0 && duration != 0 && goal != 0)
            {
                calculatedProgress = duration / goal;
                DisplayProgress = calculatedProgress.ToString("P0");
                Progress = calculatedProgress * 100;
                IsIndeterminate = false;
            }
            else if (goal == 0)
            {
                calculatedProgress = 0;
                DisplayProgress = calculatedProgress.ToString("P0");
                Progress = calculatedProgress * 100;
                IsIndeterminate = true;
            }
            else
            {
                calculatedProgress = 0;
                DisplayProgress = calculatedProgress.ToString("P0");
                Progress = calculatedProgress * 100;
                IsIndeterminate = true;
            }
        }

        #endregion

    }
}
