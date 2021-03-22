using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BgmEnum
{
    Title, Game, GameAmbient, Result, Max
}

[Serializable]
public class BgmSource
{
    public BgmEnum BgmEnum = BgmEnum.Title;
    public AudioSource AudioSource = null;
}
