using Audio;
using MInject.Runtime;
using MInject.Runtime.Signal;
using UnityEngine;

public class ServiceDemo : MonoBehaviour
{
    private AudioService _audioService;
    private SignalBus _signalBus;
    

    [Inject]
    public void Construct(AudioService audioService, SignalBus signalBus)
    {
        _audioService = audioService;
        _signalBus = signalBus;
        
        _signalBus.Subscribe<AudioClipPlayedSignal>(OnClipPlayed);
        
        _audioService.Test();
    }

    private void OnClipPlayed(AudioClipPlayedSignal obj)
    {
        Debug.Log("Played clip name : " + obj.ClipName);
    }
}
