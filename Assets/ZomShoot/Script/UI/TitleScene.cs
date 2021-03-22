using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneInitData : ISceneInitData
{
    public GameInstance GameInstance = null;
}

[PrefabPath("UGUI/TitleScene")]
public class TitleScene : SceneBase
{
    //[SerializeField]
    //private TitleSceneView view = null;

    private TitleSceneInitData initData = null;

    public override void OnInitializeScene(ISceneInitData initData)
    {
        this.initData = initData as TitleSceneInitData;

        GameInstance.Inst.PlayBGM(new BgmEnum[] { BgmEnum.Title });
    }

    public override void OnDestroyScene()
    {

    }

    public void OnClickGameStart()
    {
        GameInstance.Inst?.PlaySfx(SfxEnum.Impact);

        SceneManager.Inst.SwitchScene<GameScene>(new GameSceneInitData()
        {
            GameInstance = initData.GameInstance,
            FieldId = 1
        });
    }
}
