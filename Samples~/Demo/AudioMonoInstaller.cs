using MInject.Runtime.Installer;
namespace Audio
{
    public class AudioMonoInstaller : MonoInstallerBase
    {
        public override void InstallBindings()
        {
            ContextBase.RegisterService(new AudioService());
        }
    }
}