using Runtime.Objects;
using UnityEngine;

namespace Runtime.Systems.ObjectPool
{
    public class FirstCardObjPool : BaseObjPool<FirstCardObject>
    {
        protected override void OnGet(FirstCardObject obj)
        {
            obj.transform.SetParent(null);
            obj.ShowNumber();
            
            Vector3 angles = obj.transform.GetChild(0).eulerAngles;
            angles.z = Random.Range(0f, -180f);
            obj.transform.GetChild(0).eulerAngles = angles;
            base.OnGet(obj);
        }

        protected override void OnRelease(FirstCardObject obj)
        {
            base.OnRelease(obj);
            obj.transform.SetParent(transform);
        }
    }
}