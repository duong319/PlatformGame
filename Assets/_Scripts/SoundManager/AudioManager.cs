using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [Header("-------Audio Source-------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("-------Audio clip---------")]

    public static AudioManager Instance;


    public AudioClip backGround;
    public AudioClip PlayerSwing;
    public AudioClip BossHit;
    public AudioClip PlayerHurt;
    public AudioClip BossHurt;
    public AudioClip BossSwing;
    public AudioClip BossBattle;
    public AudioClip GameOver;
    public AudioClip BossSkill;
    public AudioClip GetCoin;
    public AudioClip LevelClear;
    public AudioClip Heal;
    public AudioClip BtnClick;



    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (musicSource != null && backGround != null)
        {
            musicSource.clip = backGround;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && SFXSource != null)
        {
            SFXSource.PlayOneShot(clip);
        }
    }
    public void StopSFX(AudioClip clip)
    {
        if (musicSource.clip == clip)
        {
            musicSource.Stop();
        }
    }

    public static void DisableBackGround()
    {
        Instance.musicSource.Stop();
    }
    public static void EnableBackGround()
    {
        Instance.musicSource.Play();
    }
    public static void PlayPlayerSwing() => Instance?.PlaySFX(Instance.PlayerSwing);
    public static void PlayBossHit() => Instance?.PlaySFX(Instance.BossHit);
    public static void PlayPlayerHurt() => Instance?.PlaySFX(Instance.PlayerHurt);
    public static void PlayBossHurt() => Instance?.PlaySFX(Instance.BossHurt);

    public static void PlayBossSwing() => Instance?.PlaySFX(Instance.BossSwing);

    public void PlaySFXWithDelay(AudioClip clip, float delay, float volume=1f)
    {
        StartCoroutine(DelayedPlaySFX(clip, delay, volume));
    }

    private  IEnumerator DelayedPlaySFX(AudioClip clip, float delay,float volume)
    {
        yield return new WaitForSeconds(delay);
        SFXSource.PlayOneShot(clip,volume);
    }


}
