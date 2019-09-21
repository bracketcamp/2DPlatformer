using UnityEngine;

public class Shooting : MonoBehaviour {

    public Gun gun;

    private Transform firePoint;

    private float timeToFire = 0f;

    public LayerMask mask;

    void Start()
    {
        firePoint = transform.Find("FirePoint");
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

        GameObject bullet = ObjectPooler.instance.SpawnFromPool("Bullet", firePoint.position, transform.parent.rotation);
        GameObject muzzleflash = ObjectPooler.instance.SpawnFromPool("MuzzleFlash", firePoint.position, transform.parent.rotation);

        float size = Random.Range(0.4f, 0.8f);

        muzzleflash.transform.localScale = new Vector3(size, size, muzzleflash.transform.localScale.z);

        if (hit.collider != null)
        {
            CharacterStats stats = hit.collider.GetComponent<CharacterStats>();

            if (stats != null)
            {
                stats.TakeDamage(gun.damage);
            }
        }
    }

}
