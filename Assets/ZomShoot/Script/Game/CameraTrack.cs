using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    [SerializeField]
    private Animator ani = null;

    [SerializeField]
    private Transform cameraJnt = null;

    private Action<int> onComplete = null;
    private int onCompleteParam = 0;

    public Transform GetCameraJoint()
    {
        return cameraJnt;
    }

    public void SetSubStage(int subStageId, Action<int> onComplete = null)
    {
        ani.SetInteger("SubStageId", subStageId);
        onCompleteParam = subStageId;
        this.onComplete = onComplete;
    }

    public void OnPlayTrack()
    {

    }

    public void OnStopTrack()
    {
        onComplete?.Invoke(onCompleteParam);
        onComplete = null;
        onCompleteParam = 0;
    }
}
