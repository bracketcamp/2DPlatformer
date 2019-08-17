using UnityEngine;

public class Tiling : MonoBehaviour {

    [HideInInspector]
    public bool hasARightMountain = false;
    [HideInInspector]
    public bool hasALeftMountain = false;

    public bool reverse = false;

    public float offsetX = 10f;

    private Camera cam;

    private SpriteRenderer sr;

    void Start()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!hasALeftMountain || !hasARightMountain)
        {
            if (cam.transform.position.x + offsetX >= transform.position.x + sr.bounds.size.x / 2f && !hasARightMountain)
            {
                MakeMountain(1);
                hasARightMountain = true;
            }
            else if (cam.transform.position.x - offsetX <= transform.position.x - sr.bounds.size.x / 2f && !hasALeftMountain)
            {
                MakeMountain(-1);
                hasALeftMountain = true;
            }
        }
    }

    void MakeMountain(int right)
    {
        Vector3 pos = new Vector3(transform.position.x + sr.bounds.size.x * right, transform.position.y, transform.position.z);

        Transform mountain = Instantiate(transform, pos, transform.rotation, transform.parent);

        if (right > 0)
            mountain.GetComponent<Tiling>().hasALeftMountain = true;
        else
            mountain.GetComponent<Tiling>().hasARightMountain = true;

        if (reverse)
            mountain.GetComponent<SpriteRenderer>().flipX = !mountain.GetComponent<SpriteRenderer>().flipX;
    }
}
