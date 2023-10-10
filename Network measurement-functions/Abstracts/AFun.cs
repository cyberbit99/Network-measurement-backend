using Network_measurement_database.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_measurement_functions.Abstracts
{
    public abstract class AFun
    {
        protected readonly NMContext _nMContext;

        protected AFun(NMContext nMContext)
        {
            this._nMContext = nMContext;
        }
    }
}
