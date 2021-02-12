using Database;
using Helper.Request;
using LuciaTech.Repository;
using LuciaTech.Service;
using Repository.Panic.interfaces;
using Services.Panic.interfaces;


namespace Services.Panic.implementation
{
    public class PanicService : GenericService<PanicAlerts>, IPanicService
    {
        private readonly IPanicRepository PanicRepository;
        public PanicService(IPanicRepository panicRepository) : base(panicRepository)
        {
            PanicRepository = panicRepository;
        }

        public bool CreatePanicAlertResolution(PanicAlertResolutionRequest model) 
        {
            return PanicRepository.CreatePanicAlertResolution(model);
        }
    }
}
