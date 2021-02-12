using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Database;
using Helper.Request;
using LuciaTech.Repository;
using Repository.Panic.interfaces;
using LuciaTech.Helper.Provider;

namespace Repository.Panic.implementation
{
    public class PanicRepository : GenericRepository<PanicAlerts>, IPanicRepository
    {
        public PanicRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public bool CreatePanicAlertResolution(PanicAlertResolutionRequest model)
        {
            var context = (ApplicationDbContext)DbContext;
            var resolution = new PanicAlertResolution()
            {
                alertResolutionId = 0,
                alertId = model.alertId.DeCryptId(),
                supportId = model.supportId.DeCryptId(),
                resolutionStatement = model.resolutionStatement,
                create_at = DateTime.Now
            };
            context.PanicAlertResolution.Add(resolution);
            context.SaveChanges();
            return true;
        }
    }
}
