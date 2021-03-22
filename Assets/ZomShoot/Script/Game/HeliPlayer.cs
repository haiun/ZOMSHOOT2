using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliPlayerData
{
    public bool Zoom = false;
    public Transform Target = null;
}

[PrefabPath("Game/HeliPlayer")]
public class HeliPlayer : MonoBehaviour 
{
    [SerializeField]
    private Camera normalCamera = null;

    [SerializeField]
    private Camera zoomCamera = null;

    [SerializeField]
    private Animator humanAni = null;

    [SerializeField]
    private Transform muzzle = null;

    [SerializeField]
    private GameObject BulletPrefab = null;

    private HeliPlayerData heliPlayerData = null;

    private bool muzzleTracking = true;
    public bool Reloading { get { return !muzzleTracking; } }

    private readonly int weaponType_int = Animator.StringToHash("WeaponType_int");
    private readonly int body_Horizontal_f = Animator.StringToHash("Body_Horizontal_f");
    private readonly int body_Vertical_f = Animator.StringToHash("Body_Vertical_f");
    private readonly int shoot_b = Animator.StringToHash("Shoot_b");

    public void ApplyHeliPlayerData(HeliPlayerData data)
    {
        heliPlayerData = data;

        bool zoom = data == null ? false : data.Zoom;
        normalCamera.enabled = !zoom;
        zoomCamera.enabled = zoom;

        bool aim = data == null ? false : data.Target != null;
        humanAni.SetFloat(weaponType_int, (zoom && aim) ? 5 : 0);
        if (!aim)
        {
            humanAni.SetFloat(body_Horizontal_f, 0);
            humanAni.SetFloat(body_Vertical_f, 0);
        }
    }

    public void LateUpdate()
    {
        if (heliPlayerData == null)
        {
            return;
        }

        var target = heliPlayerData.Target;
        if (target == null)
        {
            return;
        }

        var rightAngle = Mathf.PI * 0.5f;
        var worldToLocalMat = humanAni.transform.worldToLocalMatrix;
        var localPosition = worldToLocalMat.MultiplyPoint3x4(target.position);

        {
            var forward = Vector3.forward;
            var targetLocalPos = localPosition;
            forward.y = targetLocalPos.y = 0;

            forward = Vector3.Normalize(forward);
            targetLocalPos = Vector3.Normalize(targetLocalPos);

            var cross = Vector3.Cross(forward, targetLocalPos);
            var sin = cross.magnitude;
            var degree = (Mathf.Asin(sin) / rightAngle) * (cross.y >= 0 ? 1 : -1);

            humanAni.SetFloat(body_Horizontal_f, degree);
        }

        {
            var ignoreXForward = Vector3.forward;
            var ignoreXVector = localPosition;
            ignoreXForward.x = ignoreXVector.x = 0;

            ignoreXForward = Vector3.Normalize(ignoreXForward);
            ignoreXVector = Vector3.Normalize(ignoreXVector);

            var cross = Vector3.Cross(ignoreXForward, ignoreXVector);
            var sin = cross.magnitude;
            var degree = Mathf.Min((Mathf.Asin(sin) / rightAngle), 0.5f) * (cross.x >= 0 ? -2 : 2);

            humanAni.SetFloat(body_Vertical_f, degree);
        }

        if (muzzleTracking)
            muzzle.LookAt(target);
    }

    public void Shoot(Action onComplete)
    {
        StartCoroutine(Fire(onComplete));
    }

    IEnumerator Fire(Action onComplete)
    {
        if (humanAni == null)
        {
            var go = GameObject.Instantiate(BulletPrefab) as GameObject;
            go.transform.position = muzzle.position;
            go.transform.LookAt(heliPlayerData.Target);
            yield break;
        }

        if (!muzzleTracking)
            yield break;

        muzzleTracking = false;

        humanAni.SetBool(shoot_b, true);
        GameInstance.Inst?.PlaySfx(SfxEnum.Shoot);

        yield return new WaitForSeconds(0.16f);

        {
            var go = GameObject.Instantiate(BulletPrefab) as GameObject;
            go.transform.position = muzzle.position;
            go.transform.LookAt(heliPlayerData.Target);
        }

        yield return new WaitForSeconds(0.5f);
        GameInstance.Inst?.PlaySfx(SfxEnum.Reload);
        yield return new WaitForSeconds(0.5f);

        humanAni.SetBool(shoot_b, false);

        float fixAimFinish = Time.time + 1f;
        float leftFixAimTime = fixAimFinish - Time.time;
        while (leftFixAimTime >= 0)
        {
            var ratio = Mathf.Clamp01(leftFixAimTime);
            var currentRot = muzzle.rotation;
            muzzle.LookAt(heliPlayerData.Target);
            var targetRot = muzzle.rotation;

            muzzle.rotation = Quaternion.Lerp(targetRot, currentRot, ratio);
            yield return new WaitForEndOfFrame();
            leftFixAimTime = fixAimFinish - Time.time;
        }
        muzzle.LookAt(heliPlayerData.Target);

        GameInstance.Inst?.PlaySfx(SfxEnum.Mag);

        muzzleTracking = true;
        onComplete();
    }
}
