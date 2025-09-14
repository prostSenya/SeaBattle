using UnityEngine;
using VContainer;

namespace Infrastructure.DI
{
    public abstract class MonoInstaller  : MonoBehaviour
    {
        public abstract void Install(IContainerBuilder builder);
    }
}