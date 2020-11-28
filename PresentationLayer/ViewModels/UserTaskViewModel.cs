using MyWellnessApp.BusinessLayer;
using MyWellnessApp.Models;
using MyWellnessApp.PresentationLayer.Views;
using MyWellnessApp.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Windows.Input;

namespace MyWellnessApp.PresentationLayer.ViewModels
{
    public class UserTaskViewModel : ObservableObject
    {
        #region Fields

        private MyWellnessAppBusiness _myWellnessAppBusiness;

        private User _currentUser;
        private ObservableCollection<Task> _currentUserTasks;
        private Task _selectedTask;

        private bool _newest = true;
        private bool _oldest;

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

        public bool Newest
        {
            get { return _newest; }
            set
            {
                _newest = value;
                OnPropertyChanged(nameof(Newest));
            }
        }

        public bool Oldest
        {
            get { return _oldest; }
            set
            {
                _oldest = value;
                OnPropertyChanged(nameof(Oldest));
            }
        }

        #endregion

        #region Constructors

        public UserTaskViewModel(User user)
        {
            _myWellnessAppBusiness = new MyWellnessAppBusiness();
            _currentUser = user;
            _currentUserTasks = new ObservableCollection<Task>(_currentUser.Task.OrderByDescending(t=>t.Date));
        }

        #endregion

        #region Commands

        public ICommand RefreshCommand
        {
            get { return new RelayCommand(new Action<object>(RefreshList)); }
        }

        public ICommand DeleteCommand
        {
            get { return new RelayCommand(new Action<object>(DeleteTask)); }
        }

        public ICommand SortNewestCommand
        {
            get { return new RelayCommand(new Action<object>(SortListByNewest)); }
        }

        public ICommand SortOldestCommand
        {
            get { return new RelayCommand(new Action<object>(SortListByOldest)); }
        }

        public ICommand ExitCommand
        {
            get { return new RelayCommand(new Action<object>(Close)); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// refresh the list of tasks
        /// </summary>
        private void RefreshList(object obj)
        {
            UserTask userTasks = new UserTask
            {
                DataContext = this
            };
            _currentUserTasks = new ObservableCollection<Task>(CurrentUser.Task.OrderBy(t => t.Date));

            if (obj is System.Windows.Controls.UserControl)
            {
                (obj as System.Windows.Controls.UserControl).Content = userTasks;
            }
        }


        /// <summary>
        /// deletes the selected task
        /// </summary>
        private void DeleteTask(object obj)
        {
            UserTask userTasks = new UserTask
            {
                DataContext = this
            };
            if (SelectedTask != null)
            {
                _myWellnessAppBusiness.DeleteTask(CurrentUser, SelectedTask);
                _currentUserTasks = new ObservableCollection<Task>(CurrentUser.Task.OrderByDescending(t => t.Date));
                if (obj is System.Windows.Controls.UserControl)
                {
                    (obj as System.Windows.Controls.UserControl).Content = userTasks;
                }
            }
        }

        /// <summary>
        /// sorts the list of tasks by newest date
        /// </summary>
        private void SortListByNewest(object obj)
        {
            UserTask userTasks = new UserTask
            {
                DataContext = this
            };
            _currentUserTasks = new ObservableCollection<Task>(CurrentUser.Task.OrderByDescending(t => t.Date));
            Oldest = false;
            if (obj is System.Windows.Controls.UserControl)
            {
                (obj as System.Windows.Controls.UserControl).Content = userTasks;
            }
        }

        /// <summary>
        /// sorts the list of tasks by oldest date
        /// </summary>
        private void SortListByOldest(object obj)
        {
            UserTask userTasks = new UserTask
            {
                DataContext = this
            };
            _currentUserTasks = new ObservableCollection<Task>(CurrentUser.Task.OrderBy(t => t.Date));
            Newest = false;
            if (obj is System.Windows.Controls.UserControl)
            {
                (obj as System.Windows.Controls.UserControl).Content = userTasks;
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
