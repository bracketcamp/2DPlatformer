using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    #region Singleton
    public static CameraShake instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    [HideInInspector]
    public bool shaking = false;

    public IEnumerator Shake(float duration, float magnitude)
    {
        shaking = true;

        Vector3 pos = transform.position;

        float countDown = duration;

        while (countDown >= 0f)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.Translate(x, y, pos.z);

            countDown -= Time.deltaTime;

            yield return null;
        }

        transform.position = pos;

        shaking = false;
    }

}
