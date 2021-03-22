using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultSceneInitData : ISceneInitData
{
    public GameInstance GameInstance = null;
    public float ClearTime = 0f;
}

[PrefabPath("UGUI/GameResultScene")]
public class GameResultScene : SceneBase
{
    [SerializeField]
    private GameResultSceneView view = null;

    private GameResultSceneInitData initData = null;

    public override void OnInitializeScene(ISceneInitData _initData)
    {
        initData = (GameResultSceneInitData)_initData;

        GameInstance.Inst.PlayBGM(new BgmEnum[] { BgmEnum.Result });

        view.ApplyTime(initData.ClearTime);
    }

    public override void OnDestroyScene()
    {

    }

    public void OnClickReturnToTitle()
    {
        SceneManager.Inst.SwitchScene<TitleScene>(new TitleSceneInitData()
        {
            GameInstance = initData.GameInstance
        });
    }
}
