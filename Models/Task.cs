using System;
using System.Collections.Generic;
using System.Text;

namespace MyWellnessApp.Models
{
    public class Task
    {
        #region Fields

        private int _userID;
        private string _content;
        private DateTime _date;

        #endregion

        #region Properties
        public int UserId
        {
            get { return _userID; }
            set { _userID = value; }
        }

        public string Content 
        {
            get { return _content; }
            set { _content = value; }
        }

        public DateTime Date 
        {
            get { return _date; }
            set { _date = value; }
        }

        #endregion

        #region Constructors



        #endregion

        #region Methods

        #endregion
    }
}
