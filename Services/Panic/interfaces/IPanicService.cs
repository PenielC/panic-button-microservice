using Database;
using LuciaTech.Repository;
using LuciaTech.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Panic.interfaces
{
    public interface IPanicService : IGenericService<PanicAlerts>
    {
        bool CreatePanicAlertResolution(PanicAlertResolution model);
    }
}
