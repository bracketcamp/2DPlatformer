using UnityEngine;

public class Gun : MonoBehaviour {

    public Weapon gun;

    private Transform firePoint;

    private float timeToFire = 0f;

    public LayerMask mask;


    void Start()
    {
        firePoint = transform.FindChild("FirePoint");
    }

    void Update()
    {
        if (gun.fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / gun.fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 firePointPos = new Vector2(firePoint.position.x,firePoint.position.y);

        RaycastHit2D hit;

        hit = Physics2D.Raycast(firePoint.position, (mousePos - firePointPos).normalized, gun.distance, mask);
        Debug.DrawLine(firePoint.position, (mousePos - firePointPos).normalized * gun.distance, Color.green);
        if (hit.collider != null)
        {
            Debug.Log("We hit " + hit.collider.name);
        }
    }

}
