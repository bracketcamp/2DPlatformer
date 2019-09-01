using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 5f;

    void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0f, 0f), Space.Self);
    }

}
