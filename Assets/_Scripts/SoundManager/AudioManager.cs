using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public static void PlayPlayerSwing() => Instance?.PlaySFX(Instance.PlayerSwing);
    public static void PlayBossHit() => Instance?.PlaySFX(Instance.BossHit);
    public static void PlayPlayerHurt() => Instance?.PlaySFX(Instance.PlayerHurt);
    public static void PlayBossHurt() => Instance?.PlaySFX(Instance.BossHurt);

    public static void PlayBossSwing() => Instance?.PlaySFX(Instance.BossSwing);

    public void PlaySFXWithDelay(AudioClip clip, float delay)
    {
        StartCoroutine(DelayedPlaySFX(clip, delay));
    }

    private  IEnumerator DelayedPlaySFX(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        SFXSource.PlayOneShot(clip);
    }


}
