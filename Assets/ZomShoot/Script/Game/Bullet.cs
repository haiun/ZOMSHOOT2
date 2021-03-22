using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody RigidBody = null;

    public float Speed = 2500f;

    private void Start()
    {
        RigidBody.AddForce(transform.forward * Speed, ForceMode.Force);

        StartCoroutine(Disapper());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Met"))
        {
            GameInstance.Inst?.PlaySfx(SfxEnum.BulletHit_Metal);
        }
        else if (collision.gameObject.CompareTag("Con"))
        {
            GameInstance.Inst?.PlaySfx(SfxEnum.BulletHit_Con);
        }
        else
        {
            GameInstance.Inst?.PlaySfx(SfxEnum.BulletHit_Dirt);
        }
        Destroy(gameObject);
    }

    IEnumerator Disapper()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
