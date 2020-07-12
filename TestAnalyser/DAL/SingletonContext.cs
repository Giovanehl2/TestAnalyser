using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace TestAnalyser.DAL
{
    public class SingletonContext
    {
        private SingletonContext() { }

        private static Context ctx;

        public static Context GetInstance()
        {
            if (ctx == null)
            {
                ctx = new Context();
            }
            return ctx;
        }
    }
}