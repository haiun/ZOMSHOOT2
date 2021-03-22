using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBulletShooter : MonoBehaviour
{
    public GameObject BulletPrefab = null;
    public Transform Forward = null;

    public SimpleLookAt SimpleLookAt = null;
    public Animator SACharacter = null;
    public bool Shoot = false;

    void Update()
    {
        if (Shoot)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire()
    {
        if (SACharacter == null)
        {
            var go = GameObject.Instantiate(BulletPrefab) as GameObject;
            go.transform.position = transform.position;
            go.transform.LookAt(Forward);
            yield break;
        }

        Shoot = true;
        SACharacter.SetBool("Shoot_b", Shoot);

        yield return new WaitForSeconds(0.16f);

        {
            var go = GameObject.Instantiate(BulletPrefab) as GameObject;
            go.transform.position = transform.position;
            go.transform.LookAt(Forward);
        }

        if (SimpleLookAt != null)
            SimpleLookAt.enabled = false;

        yield return new WaitForSeconds(1f);

        Shoot = false;
        SACharacter.SetBool("Shoot_b", Shoot);

        yield return new WaitForSeconds(1f);

        if (SimpleLookAt != null)
            SimpleLookAt.enabled = true;
    }
}
