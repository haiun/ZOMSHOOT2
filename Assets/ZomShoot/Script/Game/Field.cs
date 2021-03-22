using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class SubStage
{
    public int SubStageId = 0;
    public int NextSubStageId = 0;
    public List<GameObject> ColliderList = null;
    public List<Enemy> EnemyList = null;

    public void SetColliderActive(bool active)
    {
        ColliderList.ForEach(o => o.SetActive(active));
    }
}

[PrefabPath("Field/{0}")]
public class Field : MonoBehaviour
{
    [SerializeField]
    private CameraTrack cameraTrack = null;

    [SerializeField]
    private List<SubStage> subStageList = null;

    public void ChangeStartSubStage()
    {

    }

    public void ChangeFinishSubState()
    {

    }

    public CameraTrack GetCameraTrack()
    {
        return cameraTrack;
    }

    public List<SubStage> GetSubStageList()
    {
        return subStageList.ToList();
    }
}
