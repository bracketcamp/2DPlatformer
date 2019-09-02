using UnityEngine;

public class Gun : MonoBehaviour {

    public Weapon gun;

    private Transform firePoint;

    public Transform muzzleFlash;

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

        Transform bullet = Instantiate(gun.bullet, firePoint.position, transform.parent.rotation);
        Transform muzzleflash = Instantiate(muzzleFlash, firePoint.position, transform.parent.rotation);

        Destroy(bullet.gameObject, 3f);

        float size = Random.Range(0.4f, 0.8f);

        muzzleflash.localScale = new Vector3(size, size, muzzleflash.localScale.z);

        Destroy(muzzleflash.gameObject, 0.05f);

        if (hit.collider != null)
        {
            Debug.Log("We hit " + hit.collider.name);
        }
    }

}
