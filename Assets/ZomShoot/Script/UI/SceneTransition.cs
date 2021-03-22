using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[PrefabPath("UI/SceneTransition")]
public class SceneTransition : MonoBehaviour
{
    /*
    [SerializeField]
    private Animator anim = null;

    Func<Action, IEnumerator> onFadeOutFinish = null;
    Action onFadeInFinish = null;

    public static void StartTransition(Func<Action, IEnumerator> onFadeOutFinish, Action onFadeInFinish)
    {
        var transition = GenericPrefab.Instantiate<SceneTransition>(UICamera.currentCamera.transform);
        transition.Initialize(onFadeOutFinish, onFadeInFinish);
    }

    private void Initialize(Func<Action, IEnumerator> onFadeOutFinish, Action onFadeInFinish)
    {
        this.onFadeOutFinish = onFadeOutFinish;
        this.onFadeInFinish = onFadeInFinish;
        anim.ResetTrigger("LoadFinished");
    }

    public void FameOutFinish()
    {
        StartCoroutine(onFadeOutFinish(() =>
        {
            anim.SetTrigger("LoadFinished");
        }));
    }

    public void FadeInFinish()
    {
        onFadeInFinish?.Invoke();
        Destroy(gameObject);
    }
    */
}
