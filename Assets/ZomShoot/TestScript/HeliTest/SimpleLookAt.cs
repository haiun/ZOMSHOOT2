using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLookAt : MonoBehaviour
{
    public Transform target = null;

    public float DisableTime = 0;

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        DisableTime = 1f;
    }

    private void OnDisable()
    {
        
    }

    void LateUpdate()
    {
        
        if (DisableTime <= 0)
        {
            transform.LookAt(target.position);
        }
        else
        {
            DisableTime -= Time.fixedDeltaTime;
            var ratio = Mathf.Clamp01(1f - DisableTime);

            var currentRot = transform.rotation;
            transform.LookAt(target.position);
            var targetRot = transform.rotation;

            transform.rotation = Quaternion.Lerp(currentRot, targetRot, ratio);
        }
    }
}
