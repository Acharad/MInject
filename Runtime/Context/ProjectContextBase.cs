using UnityEngine;

namespace MInject.Runtime.Context
{
    public class ProjectContextBase : SceneContextBase
    {
        protected override void Awake()
        {
            if (Application.isPlaying)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}