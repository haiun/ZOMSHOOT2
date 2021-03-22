using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SfxEnum
{
    None, Shoot, Reload, Mag, Kill, Impact,
    BulletHit_Con, BulletHit_Metal, BulletHit_Dirt,
    Max
}

[Serializable]
public class SfxClip
{
    public SfxEnum SfxEnum = SfxEnum.None;
    public AudioClip Clip = null;
}