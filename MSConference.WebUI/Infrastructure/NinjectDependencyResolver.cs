using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using MSConference.Domain.Abstract;
using MSConference.Domain.Concrete;

namespace MSConference.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IGuestRepository>().To<EFGuestRepository>();
            kernel.Bind<IContactRepository>().To<EFContactRepository>();
            kernel.Bind<IBillRepository>().To<EFBillRepository>();
        }
    }
}