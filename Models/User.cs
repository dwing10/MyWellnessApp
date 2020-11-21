using System;
using System.Collections.Generic;
using System.Text;

namespace MyWellnessApp.Models
{
    public class User
    {
        #region Fields

        private int _id;

        private string _name;
        private string _userName;
        private string _password;
        private List<string> _task;

        private UserTheme _userTheme;
        private List<PhysicalActivity> _physicalActivities;

        #endregion

        #region Properties

        public int ID 
        { 
            get { return _id; }
            set { _id = value; }
        }

        public string Name 
        {
            get { return _name; }
            set { _name = value; }
        }

        public string UserName 
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Password 
        {
            get { return _password; }
            set { _password = value; }
        }

        public List<string> Task 
        {
            get { return _task; }
            set { _task = value; }
        }

        public UserTheme UserTheme 
        {
            get { return _userTheme; }
            set { _userTheme = value; }
        }

        public List<PhysicalActivity> PhysicalActivities 
        {
            get { return _physicalActivities; }
            set { _physicalActivities = value; }
        }

        #endregion

        #region Constructors

        public User() 
        { 
            
        }

        #endregion

        #region Methods

        #endregion
    }
}
