using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    public Rigidbody RigidBody = null;

    public float Speed = 10000f;

    private void Start()
    {
        RigidBody.AddForce(transform.forward * Speed, ForceMode.Force);

        StartCoroutine(Disapper());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogFormat("Hit : {0}", collision.gameObject.name);
        Destroy(gameObject);
    }

    IEnumerator Disapper()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
