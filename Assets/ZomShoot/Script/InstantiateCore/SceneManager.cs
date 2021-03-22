using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager
{
    #region Singleton
    private static SceneManager _inst = null;
    public static SceneManager Inst
    {
        get
        {
            if (_inst == null)
                _inst = new SceneManager();

            return _inst;
        }
    }
    #endregion

    private SceneBase currentScene = null;
    private Action initializeNestScene = null;

    public void initialize<T>(ISceneInitData initData) where T : SceneBase
    {
        currentScene = GenericPrefab.Instantiate<T>();
        currentScene.InitializeScene(initData);
    }

    public void SwitchScene<T>(ISceneInitData initData) where T : SceneBase
    {
        if (currentScene != null)
        {
            currentScene.DestoryScene();
            GameObject.Destroy(currentScene.gameObject);
            currentScene = null;
        }
        initializeNestScene = () => { initialize<T>(initData); };
        //SceneTransition.StartTransition(SwitchSceneProcess, () => { });

        //FIXME:
        initializeNestScene();
    }

    private IEnumerator SwitchSceneProcess(Action onFinish)
    {
        initializeNestScene?.Invoke();

        while (!currentScene.IsSceneLoaded())
            yield return new WaitForEndOfFrame();

        yield return new WaitForEndOfFrame();
        onFinish();
    }
}
