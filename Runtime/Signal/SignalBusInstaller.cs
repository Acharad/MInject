using MInject.Runtime.Installer;

namespace MInject.Runtime.Signal
{
    public class SignalBusInstaller : MonoInstallerBase
    {
        public override void InstallBindings()
        {
            ContextBase.RegisterService(new SignalBus());
        }
    }
}