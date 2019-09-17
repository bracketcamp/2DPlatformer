using UnityEngine;

public class Particles : MonoBehaviour, IPooledObject
{

    public float disableDelay;

    public void OnObjectSpawn()
    {
        GetComponent<ParticleSystem>().Play();
        Invoke("Disable", disableDelay);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

}
