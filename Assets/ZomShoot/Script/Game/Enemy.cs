using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInitData
{
    public Action<Enemy> OnKillEnemy = null;
    public Action<Enemy> OnRemoveEnemy = null;
}

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform targetJoint = null;

    [SerializeField]
    private Light burnningLight = null;

    private Animator ani = null;
    private Collider targetCollider = null;
    private Rigidbody[] rigidbodyList = null;

    private EnemyInitData initData = null;

    public Transform TargetJoint { get { return targetJoint; } }

    public List<Material> materialList = null;

    private readonly int burnningColorId = Shader.PropertyToID("_BurnningColor");
    private readonly Color burnningColorValue = new Color(1f, 0.5f, 0, 0);

    public void Initialize(EnemyInitData initData)
    {
        this.initData = initData;

        ani = GetComponent<Animator>();
        targetCollider = GetComponent<Collider>();
        rigidbodyList = GetComponentsInChildren<Rigidbody>();

        {
            var meshRendererList = GetComponentsInChildren<MeshRenderer>();
            foreach (var renderer in meshRendererList)
            {
                foreach (var mat in renderer.materials)
                {
                    if (mat != null)
                    {
                        materialList.Add(mat);
                    }
                }
            }
        }
        {
            var meshRendererList = GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var renderer in meshRendererList)
            {
                foreach (var mat in renderer.materials)
                {
                    if (mat != null)
                    {
                        materialList.Add(mat);
                    }
                }
            }
        }

        foreach (var mat in materialList)
        {
            mat.SetColor(burnningColorId, burnningColorValue);
        }
        burnningLight.gameObject.SetActive(false);

        ToggleRagdoll(true);
    }

    private void ToggleRagdoll(bool isAnimating)
    {
        ani.enabled = isAnimating;

        targetCollider.enabled = isAnimating;

        foreach (var rigidbody in rigidbodyList)
        {
            rigidbody.isKinematic = isAnimating;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            //kill precess
            ToggleRagdoll(false);
            StartCoroutine(RemoveDirection());

            GameInstance.Inst?.PlaySfx(SfxEnum.Kill);

            initData.OnKillEnemy?.Invoke(this);
        }
    }

    IEnumerator RemoveDirection()
    {
        yield return new WaitForSeconds(2f);

        burnningLight.gameObject.SetActive(true);
        {
            Color color = new Color(1f, 0.5f, 0, 0);

            float startTime = Time.time;
            var deltaSec = Time.time - startTime;
            while (deltaSec < 0.5f)
            {
                deltaSec = Time.time - startTime;
                var alpha = deltaSec / 0.5f;
                color.a = alpha;
                foreach (var mat in materialList)
                {
                    mat.SetColor("_BurnningColor", color);
                }
                burnningLight.intensity = alpha * 10f;

                yield return new WaitForEndOfFrame();
            }

            color.a = 1f;
            foreach (var mat in materialList)
            {
                mat.SetColor("_BurnningColor", color);
            }
            burnningLight.intensity = 5f;
        }

        {
            float startTime = Time.time;
            var deltaSec = Time.time - startTime;
            while (deltaSec < 3.1f)
            {
                deltaSec = Time.time - startTime;

                var alpha = (3f - deltaSec) / 3f;
                foreach (var mat in materialList)
                {
                    mat.SetFloat("_BurnningAlpha", alpha);
                }
                burnningLight.intensity = alpha * 10f;

                yield return new WaitForEndOfFrame();
            }
        }
        burnningLight.gameObject.SetActive(false);

        initData.OnRemoveEnemy?.Invoke(this);
    }
}
