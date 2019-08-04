using UnityEngine;

public class PlayerFollow : MonoBehaviour {

    public Transform player;

    public float minY = -1f;

    private Vector3 lastPos;

    void LateUpdate()
    {
        if (lastPos == null)
            lastPos = transform.position;
        if(player.position != lastPos)
        {
            transform.position = new Vector3(player.position.x, Mathf.Clamp(player.position.y, minY, Mathf.Infinity), -10f);
            lastPos = transform.position;
        }
    }

}
