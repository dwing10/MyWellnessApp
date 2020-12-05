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
    public class AddTaskViewModel : ObservableObject
    {
        #region Fields

        private MyWellnessAppBusiness _myWellnessAppBusiness;

        private User _currentUser;

        private string _taskToAdd;
        private string _message;
        private string _errorMessage;

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

        public string TaskToAdd
        {
            get { return _taskToAdd; }
            set
            {
                _taskToAdd = value;
                OnPropertyChanged(nameof(TaskToAdd));
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

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        #endregion

        #region Constructors

        public AddTaskViewModel(User user)
        {
            _myWellnessAppBusiness = new MyWellnessAppBusiness();
            _currentUser = user;
        }

        #endregion

        #region Commands

        public ICommand AddCommand
        {
            get { return new RelayCommand(new Action<object>(AddTaskToUser)); }
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
        /// adds a task to user 
        /// </summary>
        private void AddTaskToUser(object obj)
        {
            ErrorMessage = null;
            Message = null;
            AddTask addTask = new AddTask
            {
                DataContext = this
            };
            Task task = CreateTask();
            try
            {
                if (task != null)
                {
                    if (!String.IsNullOrEmpty(TaskToAdd))
                    {
                        _myWellnessAppBusiness.AddTaskToUser(CurrentUser, task);
                        _currentUser.Task = _myWellnessAppBusiness.GetCurrentUserTasks(CurrentUser);
                        ResetInputBoxes();
                        ErrorMessage = null;
                        Message = "SUCCESS!";
                        if (obj is System.Windows.Controls.UserControl)
                        {
                            (obj as System.Windows.Controls.UserControl).Content = addTask;
                        }
                    }
                    else
                    {
                        Message = null;
                        ErrorMessage = "SORRY, NOTHING TO ADD";
                    }
                }
            }
            catch (Exception e)
            {
                string m = e.Message;
                throw;
            }
        }

        /// <summary>
        /// creates Task to be added
        /// </summary>
        private Task CreateTask()
        {
            return new Task
            {
                UserId = CurrentUser.ID,
                Content = TaskToAdd,
                Date = DateTime.Now
            };
        }

        /// <summary>
        /// clears the form
        /// </summary>
        private void Clear(object obj)
        {
            AddTask addTask = new AddTask
            {
                DataContext = this
            };
            ErrorMessage = null;
            Message = null;
            ResetInputBoxes();
            if (obj is System.Windows.Controls.UserControl)
            {
                (obj as System.Windows.Controls.UserControl).Content = addTask;
            }
        }

        /// <summary>
        /// resets the input fields
        /// </summary>
        private void ResetInputBoxes()
        {
            TaskToAdd = null;
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
