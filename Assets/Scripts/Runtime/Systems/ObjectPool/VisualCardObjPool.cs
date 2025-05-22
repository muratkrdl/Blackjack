using Runtime.Objects;
using UnityEngine;

namespace Runtime.Systems.ObjectPool
{
    public class VisualCardObjPool : BaseObjPool<VisualCardObject>
    {
        protected override void OnGet(VisualCardObject obj)
        {
            obj.HideNumber();
            obj.transform.SetParent(null);
            Vector3 angles = obj.transform.GetChild(0).eulerAngles;
            angles.z = Random.Range(0f, -180f);
            obj.transform.GetChild(0).eulerAngles = angles;
            base.OnGet(obj);
        }

        protected override void OnRelease(VisualCardObject obj)
        {
            base.OnRelease(obj);
            obj.transform.SetParent(transform);
        }
    }
}