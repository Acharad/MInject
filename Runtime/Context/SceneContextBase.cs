namespace MInject.Runtime.Context
{
    public class SceneContextBase : ContextBase
    {
        protected override void Awake()
        {
            base.Awake();
            
            Initialize();
        }
    }
}