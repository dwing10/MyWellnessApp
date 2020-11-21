using System;
using System.Collections.Generic;
using System.Text;

namespace MyWellnessApp.Models
{
    public enum FileIoMessage
    {
        None,
        Complete,
        FileAccessError,
        RecordNotFound,
        NoRecordsFound
    }
}
