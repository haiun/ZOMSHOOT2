using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.UI;

[Serializable]
public class GameSceneView
{
    public List<GameObject> NormalViewObject = null;
    public List<GameObject> ZoomViewObject = null;

    public List<Button> ShootingDelayButton = null;

    public List<Text> Time = null;
    public List<Text> LeftTargetCount = null;

    private StringBuilder timeStringBuilder = new StringBuilder();

    public void ApplyGameSceneState(GameSceneState state)
    {
        var data = state.HeliPlayerData;
        bool zoom = data == null ? false : data.Zoom;
        NormalViewObject.ForEach(o => o.SetActive(!zoom));
        ZoomViewObject.ForEach(o => o.SetActive(zoom));

        var leftTargetCountText = state.HeliPlayerData.Target == null ? "0" : state.LeftEnemyCount.ToString();
        foreach (var leftTargetCount in LeftTargetCount)
        {
            leftTargetCount.text = leftTargetCountText;
        }
    }

    public void ApplyTime(float seconds)
    {
        int sec = (int)seconds;
        int millisec = (int)((seconds - sec) * 100);

        int ret;
        int div = Math.DivRem(sec, 60, out ret);

        timeStringBuilder.Length = 0;
        timeStringBuilder.AppendFormat("{0:00}:{1:00}:{2:00}", div, ret, millisec);
        foreach (var time in Time)
        {
            time.text = timeStringBuilder.ToString();
        }
    }

    public void ApplyShootingDelayButton(bool active)
    {
        ShootingDelayButton.ForEach(b => b.interactable = active);
    }
}

