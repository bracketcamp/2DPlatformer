using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 100f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rb.AddForce(new Vector2(h * speed * Time.deltaTime, 0f));

        if (h == 1)
            sr.flipX = false;
        else if (h == -1)
            sr.flipX = true;
    }

}
