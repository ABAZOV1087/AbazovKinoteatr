using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbazovKinoteatr
{
    public static class Core
    {
        public static AbazovCinemaDBEntities5 Context = new AbazovCinemaDBEntities5();
        public static Users CurrentUser = null;
    }
}