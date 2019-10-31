using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Services.Events
{
    public class OnServiceExceptionArgs : EventArgs
    {
        public Exception Exception;
    }
}
