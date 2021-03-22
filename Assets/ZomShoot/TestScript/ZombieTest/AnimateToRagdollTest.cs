using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimateToRagdollTest : MonoBehaviour
{
    public Animator ani = null;
    public Collider targetCollider = null;

    public Rigidbody[] rigidbodyList = null;

    private void Start()
    {
        rigidbodyList = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(true);
    }

    private void ToggleRagdoll(bool isAnimating)
    {
        Debug.Log("ToggleRagdoll");

        ani.enabled = isAnimating;

        targetCollider.enabled = isAnimating;

        foreach (var rigidbody in rigidbodyList)
        {
            rigidbody.isKinematic = isAnimating;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");

        if (collision.gameObject.CompareTag("Projectile"))
        {
            ToggleRagdoll(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ToggleRagdoll(false);
        }
    }
}
