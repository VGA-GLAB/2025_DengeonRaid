using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private ReferenceManager _rm;
    private EventBus _eventBus;

    private void Start()
    {
        _rm = ReferenceManager.Instance;
        _eventBus = new EventBus();
        _rm.EventBus = _eventBus;
    }
}
