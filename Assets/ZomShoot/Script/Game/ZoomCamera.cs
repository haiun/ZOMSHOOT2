using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField]
    private Shader posteffectShader = null;
    private Material posteffectMaterial = null;

    private void Awake()
    {
        posteffectMaterial = new Material(posteffectShader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, posteffectMaterial);
    }
}
