using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Database;
using LuciaTech.Repository;

namespace Repository.Panic.interfaces
{
    public interface IPanicRepository : IGenericRepository<PanicAlerts>
    {
        bool CreatePanicAlertResolution(PanicAlertResolution model);
    }
}
