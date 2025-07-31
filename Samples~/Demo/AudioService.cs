using MInject.Runtime;
using MInject.Runtime.Service;
using MInject.Runtime.Signal;
using UnityEngine;

namespace Audio
{
    public class AudioService : ServiceBase
    {
        private SignalBus _signalBus;
        private AudioClipNamesSO _audioClipNamesSO;
        
        [Inject]
        public void Construct(SignalBus signalBus, AudioClipNamesSO audioClipNamesSo)
        {
            _signalBus = signalBus;
            _audioClipNamesSO = audioClipNamesSo;
        }

        private void PlayClip()
        {
            _signalBus.Fire(new AudioClipPlayedSignal { ClipName = _audioClipNamesSO.AudioClipNames[0] });
        }
        
        public void Test()
        {
            PlayClip();
        }
    }
}
