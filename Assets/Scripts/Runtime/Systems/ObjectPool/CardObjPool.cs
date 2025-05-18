using Runtime.Objects;
using UnityEngine;

namespace Runtime.Systems.ObjectPool
{
    public class CardObjPool : BaseObjPool<CardObject>
    {
        protected override void OnGet(CardObject obj)
        {
            obj.transform.SetParent(null);
            Vector3 angles = obj.transform.GetChild(0).eulerAngles;
            angles.z = Random.Range(0f, -180f);
            obj.transform.GetChild(0).eulerAngles = angles;
            base.OnGet(obj);
        }

        protected override void OnRelease(CardObject obj)
        {
            base.OnRelease(obj);
            obj.transform.SetParent(transform);
        }
    }
}