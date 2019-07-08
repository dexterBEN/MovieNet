using MovieNet.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNet.utils
{
   public class Singleton
   {
        private static ServiceFacade _instance = null;

        private Singleton(){ }

        public static ServiceFacade GetInstance
        {
            get
            {
                if (_instance == null)
                    _instance = new ServiceFacade();

                return _instance;
            }
        }

    }
}
