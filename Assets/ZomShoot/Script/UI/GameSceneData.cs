using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameSceneInitData : ISceneInitData
{
    public GameInstance GameInstance = null;
    public int FieldId = 1;
}

public class GameSceneState
{
    public List<Enemy> TargetEnemyList = null;

    public HeliPlayerData HeliPlayerData = null;

    public int SubStageId = 0;
    public int NextSubStageId = 0;

    public int LeftEnemyCount = 0;
    public int CurrentEnemyIndex = 0;

    public void InvalidTarget()
    {
        if (LeftEnemyCount <= CurrentEnemyIndex)
            CurrentEnemyIndex = 0;

        if (CurrentEnemyIndex < TargetEnemyList.Count)
        {
            HeliPlayerData.Target = TargetEnemyList[CurrentEnemyIndex].TargetJoint;
        }
        else
        {
            HeliPlayerData.Target = null;
        }
    }

    public void OnRemoveEnemy(Enemy enemy)
    {
        var enemyList = TargetEnemyList;
        enemyList.Remove(enemy);

        LeftEnemyCount = enemyList.Count;
        InvalidTarget();
    }

    public void SetNextEnemy()
    {
        CurrentEnemyIndex++;
        InvalidTarget();
    }

    public void SetZoom(bool zoom)
    {
        HeliPlayerData.Zoom = zoom;
    }
}
