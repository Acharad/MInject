using System.Collections.Generic;
using MInject.Runtime.Service;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipNamesSO", menuName = "Scriptable Objects/AudioClipNamesSO")]
public class AudioClipNamesSO : ScriptableObject, IService
{
    public List<string> AudioClipNames;
}
