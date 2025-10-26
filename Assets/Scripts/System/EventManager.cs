using Unity.VisualScripting;
using UnityEngine;

public class GameEventController : MonoBehaviour
{
    private ReferenceManager _rm;

    private void Start()
    {
        _rm = ReferenceManager.Instance;
    }


}
