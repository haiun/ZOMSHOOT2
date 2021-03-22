using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCollosionObject : MonoBehaviour
{
    [SerializeField]
    private SfxEnum sfxEnum = SfxEnum.None;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            GameInstance.Inst?.PlaySfx(sfxEnum);
        }
    }
}
