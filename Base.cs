using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace simicon.automation;

    public static  class Base
    {
            /// <summary>
        /// Get Output Prefix for COnsolr.writeline
        /// </summary>
        /// <returns>DateTime prefix string ro use in Console.writeline</returns>
        public static string GOP()// Get Ouyput Prefix
        {
            string op = ":) " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss ");
            return op;
        }
    }
