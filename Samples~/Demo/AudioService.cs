using ServiceSystem;
using ServiceSystem.Service;
using ServiceSystem.Signal;
using TMPro;
using UnityEngine;

namespace Audio
{
    public class AudioService : ServiceBase
    {
        private SignalBus _signalBus;
        
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void PlayClip()
        {
            _signalBus.Fire(new AudioClipPlayedSignal { ClipName = "ahmet" });
        }
        
        public void Test()
        {
            Debug.Log("ahmet test");
            PlayClip();
        }
    }
}
