using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbazovKinoteatr
{
    public static class Core
    {
        public static AbazovCinemaDBEntities3 Context = new AbazovCinemaDBEntities3();
        public static Users CurrentUser = null;
    }
}