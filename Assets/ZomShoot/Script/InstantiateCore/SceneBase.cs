using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneInitData
{

}

public abstract class SceneBase : MonoBehaviour
{
    public void InitializeScene(ISceneInitData initData)
    {
        OnInitializeScene(initData);
    }

    public void DestoryScene()
    {
        OnDestroyScene();
    }

    public abstract void OnInitializeScene(ISceneInitData initData);
    public abstract void OnDestroyScene();

    public bool IsSceneLoaded()
    {
        return true;
    }
}
