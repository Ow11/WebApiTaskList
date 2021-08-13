using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTaskList.Services
{
    public static class Utils
    {
        public static string DateTimeNow()
        {
            return DateTime.Now.ToString();
        }
    }
}
