using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI.Scopes
{
    public abstract class CustomScope : LifetimeScope
    {
        [SerializeField] private List<MonoInstaller> _defaultServicesInstallers;

        protected override void Configure(IContainerBuilder builder)
        {
            _defaultServicesInstallers.ForEach(service => service.Install(builder));
        }
    }
}