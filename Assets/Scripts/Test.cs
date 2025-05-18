using Runtime.Events;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        CoreGameEvents.Instance.OnTourStart?.Invoke();
    }
}
