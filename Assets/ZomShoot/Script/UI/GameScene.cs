using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[PrefabPath("UGUI/GameScene")]
public class GameScene : SceneBase
{
    [SerializeField]
    private GameSceneView view = null;

    private GameSceneInitData initData = null;

    private Field field = null;

    private CameraTrack cameraTrack = null;
    private HeliPlayer heliPlayer = null;
    private List<SubStage> subStageList = null;

    private float startTime = 0f;
    private GameSceneState gameSceneState = null;

    #region SceneBase
    public override void OnInitializeScene(ISceneInitData _initData)
    {
        initData = (GameSceneInitData)_initData;

        GameInstance.Inst.PlayBGM(new BgmEnum[] { BgmEnum.Game, BgmEnum.GameAmbient });

        field = GenericPrefab.InstantiatePathFormat<Field>(initData.FieldId.ToString());
        cameraTrack = field.GetCameraTrack();
        heliPlayer = GenericPrefab.Instantiate<HeliPlayer>(cameraTrack.GetCameraJoint());
        subStageList = field.GetSubStageList();

        //GameSceneState
        {
            int enemyCount = 0;
            var fistStage = subStageList.FirstOrDefault();
            Transform firstTarget = null;
            if (fistStage != null)
            {
                var firstEnemy = fistStage.EnemyList.FirstOrDefault();
                enemyCount = fistStage.EnemyList.Count;
                if (firstEnemy != null)
                {
                    firstTarget = firstEnemy.TargetJoint;
                }
            }

            gameSceneState = new GameSceneState()
            {
                TargetEnemyList = fistStage.EnemyList.ToList(),
                HeliPlayerData = new HeliPlayerData()
                {
                    Zoom = false,
                    Target = firstTarget
                },
                SubStageId = fistStage.SubStageId,
                NextSubStageId = fistStage.NextSubStageId,
                LeftEnemyCount = enemyCount,
                CurrentEnemyIndex = 0
            };
            fistStage.SetColliderActive(true);

            gameSceneState.InvalidTarget();
            heliPlayer.ApplyHeliPlayerData(gameSceneState.HeliPlayerData);
            view.ApplyGameSceneState(gameSceneState);
        }

        //init enemy
        {
            var enemyInitData = new EnemyInitData()
            {
                OnKillEnemy = OnKillEnemy,
                OnRemoveEnemy = OnRemoveEnemy
            };

            foreach (var subStage in subStageList)
            {
                foreach (var enemy in subStage.EnemyList)
                {
                    enemy.Initialize(enemyInitData);
                }
            }
        }

        //Init CameraTrack
        cameraTrack.SetSubStage(1);

        startTime = Time.time;
        view.ApplyTime(Time.time - startTime);
    }

    public override void OnDestroyScene()
    {
        gameSceneState = null;

        subStageList = null;
        heliPlayer = null;
        cameraTrack = null;

        Destroy(field.gameObject);
    }
    #endregion

    public void Update()
    {
        view.ApplyTime(Time.time - startTime);
    }

    private IEnumerator ZoomProcess(Action onFinish)
    {
        heliPlayer.ApplyHeliPlayerData(gameSceneState.HeliPlayerData);
        view.ApplyGameSceneState(gameSceneState);

        onFinish();
        yield break;
    }

    public void OnClickSelectEnemy()
    {
        gameSceneState.SetNextEnemy();

        heliPlayer.ApplyHeliPlayerData(gameSceneState.HeliPlayerData);
    }

    public void OnClickZoomIn()
    {
        if (gameSceneState.HeliPlayerData.Target == null)
            return;

        if (heliPlayer.Reloading)
            return;

        gameSceneState.SetZoom(true);

        //SceneTransition.StartTransition(ZoomProcess, () => { });
        StartCoroutine(ZoomProcess(() => { }));
    }

    public void OnClickZoomOut()
    {
        gameSceneState.SetZoom(false);
        gameSceneState.InvalidTarget();
        heliPlayer.ApplyHeliPlayerData(gameSceneState.HeliPlayerData);
        view.ApplyGameSceneState(gameSceneState);

        //SceneTransition.StartTransition(ZoomProcess, () => { });
    }

    public void OnClickShoot()
    {
        if (heliPlayer.Reloading)
            return;

        view.ApplyShootingDelayButton(false);
        heliPlayer.Shoot(() =>
        {
            view.ApplyShootingDelayButton(true);
        });
    }

    public void OnKillEnemy(Enemy enemy)
    {
        gameSceneState.OnRemoveEnemy(enemy);
        gameSceneState.SetZoom(false);
        heliPlayer.ApplyHeliPlayerData(gameSceneState.HeliPlayerData);
        view.ApplyGameSceneState(gameSceneState);

        if (gameSceneState.TargetEnemyList.Count == 0)
        {
            var currentSubStage = subStageList.FirstOrDefault(s => s.SubStageId == gameSceneState.SubStageId);
            var nextSubStage = subStageList.FirstOrDefault(s => s.SubStageId == gameSceneState.NextSubStageId);
            if (gameSceneState.SubStageId == nextSubStage.NextSubStageId)
            {
                SceneManager.Inst.SwitchScene<GameResultScene>(new GameResultSceneInitData()
                {
                    GameInstance = initData.GameInstance,
                    ClearTime = Time.time - startTime
                });
                return;
            }

            currentSubStage.SetColliderActive(false);
            cameraTrack.SetSubStage(gameSceneState.NextSubStageId, OnChangeStage);
        }
    }

    public void OnChangeStage(int subStageId)
    {
        var subStage = subStageList.FirstOrDefault(s => s.SubStageId == subStageId);

        int enemyCount = 0;
        Transform firstTarget = null;
        if (subStage != null)
        {
            var firstEnemy = subStage.EnemyList.FirstOrDefault();
            enemyCount = subStage.EnemyList.Count;
            if (firstEnemy != null)
            {
                firstTarget = firstEnemy.TargetJoint;
            }
        }

        gameSceneState = new GameSceneState()
        {
            TargetEnemyList = subStage.EnemyList.ToList(),
            HeliPlayerData = new HeliPlayerData()
            {
                Zoom = false,
                Target = firstTarget
            },
            SubStageId = subStage.SubStageId,
            NextSubStageId = subStage.NextSubStageId,
            LeftEnemyCount = enemyCount,
            CurrentEnemyIndex = 0
        };
        subStage.SetColliderActive(true);

        gameSceneState.InvalidTarget();
        heliPlayer.ApplyHeliPlayerData(gameSceneState.HeliPlayerData);
        view.ApplyGameSceneState(gameSceneState);
    }

    public void OnRemoveEnemy(Enemy enemy)
    {

    }
}
