using MInject.Runtime.Installer;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipNamesSOInstaller", menuName = "Scriptable Objects/AudioClipNamesSOInstaller")]
public class AudioClipNamesSOInstaller : ScriptableInstallerBase
{
    
    [SerializeField] private AudioClipNamesSO AudioClipNamesSO;
    public override void InstallBindings()
    {
        ContextBase.RegisterService(AudioClipNamesSO);
    }
}
