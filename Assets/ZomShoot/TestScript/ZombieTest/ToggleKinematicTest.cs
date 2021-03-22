using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleKinematicTest : MonoBehaviour
{
    public Rigidbody Rigidbody = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Rigidbody.isKinematic = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Rigidbody.AddForce(transform.forward * 100f, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
