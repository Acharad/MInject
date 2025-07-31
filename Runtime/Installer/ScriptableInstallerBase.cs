using MInject.Runtime.Context;
using UnityEngine;

namespace MInject.Runtime.Installer
{
    public abstract class ScriptableInstallerBase : ScriptableObject, IInstaller 
    {
        [Inject] 
        private ContextBase _contextBase;

        protected ContextBase ContextBase => _contextBase;

        public abstract void InstallBindings();
    }
}