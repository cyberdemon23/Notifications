using Autofac;
using Autofac.Builder;
using Autofac.Core;
using SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notifications.Web
{
    public class AutofacSignalRDependencyResolver : DefaultDependencyResolver, IRegistrationSource
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AutofacSignalRDependencyResolver(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
            _lifetimeScope.ComponentRegistry.AddRegistrationSource(this);
        }

        public override object GetService(Type serviceType)
        {
            object result;
            if (_lifetimeScope.TryResolve(serviceType, out result))
            {
                return result;
            }

            return null;
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            object result;
            if (_lifetimeScope.TryResolve(typeof(IEnumerable<>).MakeGenericType(serviceType), out result))
            {
                return (IEnumerable<object>)result;
            }

            return Enumerable.Empty<object>();
        }

        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            var typedService = service as TypedService;
            if (typedService != null)
            {
                var instances = base.GetServices(typedService.ServiceType);

                if (instances != null)
                {
                    return instances
                            .Select(i => RegistrationBuilder.ForDelegate(i.GetType(), (c, p) => i).As(typedService.ServiceType)
                            .InstancePerMatchingLifetimeScope(_lifetimeScope.Tag)
                            .PreserveExistingDefaults()
                            .CreateRegistration());
                }
            }

            return Enumerable.Empty<IComponentRegistration>();
        }

        bool IRegistrationSource.IsAdapterForIndividualComponents
        {
            get { return false; }
        }
    }
}
