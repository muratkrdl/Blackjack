using Runtime.Objects;

namespace Runtime.Systems.ObjectPool
{
    public class CardObjPool : BaseObjPool<CardObject>
    {
        protected override void OnGet(CardObject obj)
        {
            obj.transform.SetParent(null);
            base.OnGet(obj);
        }

        protected override void OnRelease(CardObject obj)
        {
            base.OnRelease(obj);
            obj.transform.SetParent(transform);
        }
    }
}