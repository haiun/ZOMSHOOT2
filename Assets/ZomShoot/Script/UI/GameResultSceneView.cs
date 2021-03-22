using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.UI;

[Serializable]
public class GameResultSceneView
{
    public List<Text> Time = null;

    private StringBuilder timeStringBuilder = new StringBuilder();

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
}
