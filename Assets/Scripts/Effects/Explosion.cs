using UnityEngine;

public class Explosion : MonoBehaviour
{
    public void ExplosionFinished()
    {
        Destroy(gameObject);
    }
}
