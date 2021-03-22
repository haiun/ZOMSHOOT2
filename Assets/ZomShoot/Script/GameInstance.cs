using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameInstance : MonoBehaviour
{
    [SerializeField]
    private AudioSource sfxSource = null;
    [SerializeField]
    private List<BgmSource> bgmSourceList = null;
    [SerializeField]
    private List<SfxClip> sfxClipList = null;

    private BgmSource[] bgmSourceInstanceArr = null;
    private SfxClip[] sfxClipInstanceArr = null;


    public static GameInstance Inst { get { return inst; } }
    private static GameInstance inst = null;

    void Start()
    {
        inst = this;
        InitializeAudio();
        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartUp());
    }

    IEnumerator StartUp()
    {
        //while (UICamera.currentCamera == null)
        //    yield return new WaitForEndOfFrame();

        SceneManager.Inst.initialize<TitleScene>(new TitleSceneInitData()
        {
            GameInstance = this
        });
        yield break;
    }

    public void InitializeAudio()
    {
        int bgmMax = (int)BgmEnum.Max;
        bgmSourceInstanceArr = new BgmSource[bgmMax];
        for (int i = 0; i < bgmMax; ++i)
        {
            bgmSourceInstanceArr[i] = bgmSourceList.FirstOrDefault(f => (int)f.BgmEnum == i);
        }

        int sfxMax = (int)SfxEnum.Max;
        sfxClipInstanceArr = new SfxClip[sfxMax];
        for (int i = 0; i < sfxMax; ++i)
        {
            sfxClipInstanceArr[i] = sfxClipList.FirstOrDefault(f => (int)f.SfxEnum == i);
        }
    }

    public void PlayBGM(BgmEnum[] bgmList)
    {
        for (int i = 0; i < (int)BgmEnum.Max; ++i)
        {
            bool active = bgmList.Contains((BgmEnum)i);
            bgmSourceInstanceArr[i]?.AudioSource.gameObject.SetActive(active);
        }
    }

    public void PlaySfx(SfxEnum sfxEnum)
    {
        var sfxClip = sfxClipInstanceArr[(int)sfxEnum];
        if (sfxClip != null)
        {
            sfxSource.PlayOneShot(sfxClip.Clip);
        }
    }
}
