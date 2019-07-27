using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 100f;
    public float jumpSpeed = 5f;

    private Rigidbody2D rb;

    private SpriteRenderer sr;

    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rb.AddForce(new Vector2(h * speed * Time.deltaTime, 0f));

        if (h == 1)
            sr.flipX = false;
        else if (h == -1)
            sr.flipX = true;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(0f, jumpSpeed);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
