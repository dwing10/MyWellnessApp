using MyWellnessApp.BusinessLayer;
using MyWellnessApp.Models;
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
    public class EditTaskViewModel : ObservableObject
    {
        #region Fields

        private MyWellnessAppBusiness _myWellnessAppBusiness;

        private User _currentUser;
        private ObservableCollection<Task> _currentUserTasks;
        private Task _selectedTask;

        private string _message;
        private string _inputBox;

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

        public ObservableCollection<Task> CurrentUserTasks
        {
            get { return _currentUserTasks; }
            set
            {
                _currentUserTasks = value;
                OnPropertyChanged(nameof(CurrentUserTasks));
            }
        }

        public Task SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
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

        public string InputBox
        {
            get { return _inputBox; }
            set
            {
                _inputBox = _selectedTask.Content;
                OnPropertyChanged(nameof(InputBox));
            }
        }

        #endregion

        #region Constructors

        public EditTaskViewModel(User user) 
        {
            _myWellnessAppBusiness = new MyWellnessAppBusiness();
            _currentUser = user;
            _currentUserTasks = new ObservableCollection<Task>(_currentUser.Task.OrderByDescending(t => t.Date));
        }

        #endregion

        #region Commands

        public ICommand SaveCommand
        {
            get { return new RelayCommand(new Action<object>(EditUserTask)); }
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
        private void EditUserTask(object obj) 
        {
            EditTask editTask = new EditTask
            {
                DataContext = this
            };
            //Task task = CreateTask();
            if (SelectedTask != null)
            {
                _myWellnessAppBusiness.DeleteTask(CurrentUser, SelectedTask);
                _myWellnessAppBusiness.EditTask(CurrentUser, SelectedTask);
                _currentUserTasks = new ObservableCollection<Task>(CurrentUser.Task.OrderByDescending(t => t.Date));
                if (obj is System.Windows.Controls.UserControl)
                {
                    (obj as System.Windows.Controls.UserControl).Content = editTask;
                }
            }
        }

        /// <summary>
        /// creates task to be edited
        /// </summary>
        //private Task CreateTask()

        /// <summary>
        /// clears the form
        /// </summary>
        private void Clear(object obj)
        {
            EditTask editTask = new EditTask
            {
                DataContext = this
            };
            Message = null;
            ResetInputBoxes();
            if (obj is System.Windows.Controls.UserControl)
            {
                (obj as System.Windows.Controls.UserControl).Content = editTask;
            }
        }

        /// <summary>
        /// resets the input boxes
        /// </summary>
        private void ResetInputBoxes() 
        {
            SelectedTask = null;
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
