using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnningOutTest : MonoBehaviour
{
    public List<Material> materialList = null;
    public bool blockInput = false;
    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (blockInput)
            return;

        if (Input.GetKeyDown(KeyCode.B))
        {
            blockInput = true;
            StartCoroutine(BurnningOut());
        }
    }

    IEnumerator BurnningOut()
    {
        float startTime = Time.time;

        var deltaSec = Time.time - startTime;
        while (deltaSec < 3.1f)
        {
            deltaSec = Time.time - startTime;

            var alpha = (3f - deltaSec) / 3f;
            Debug.Log(alpha);
            foreach (var mat in materialList)
            {
                mat.SetFloat("_BurnningAlpha", alpha);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
