using UnityEngine;

public class PlayerFollow : MonoBehaviour {

    public float minY = -1f;

    private Vector3 lastPos;

    [HideInInspector]
    public PlayerHealth health;

    void Awake()
    {
        health = PlayerHealth.instance;
    }

    void LateUpdate()
    {
        if (health == null)
        {
            health = PlayerHealth.instance;
            return;
        }

        if (lastPos == null)
            lastPos = transform.position;
        if(health.transform.position != lastPos)
        {
            transform.position = new Vector3(health.transform.position.x, Mathf.Clamp(health.transform.position.y, minY, Mathf.Infinity), -10f);
            lastPos = transform.position;
        }
    }

}
