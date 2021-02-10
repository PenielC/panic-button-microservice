using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Database;
using LuciaTech.Repository;
using Repository.Panic.interfaces;

namespace Repository.Panic.implementation
{
    public class PanicRepository : GenericRepository<PanicAlerts>, IPanicRepository
    {
        public PanicRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public bool CreatePanicAlertResolution(PanicAlertResolution model)
        {
            var context = (ApplicationDbContext)DbContext;
            context.PanicAlertResolution.Add(model);
            context.SaveChanges();
            return true;
        }
    }
}
