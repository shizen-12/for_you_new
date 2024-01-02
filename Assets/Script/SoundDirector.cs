using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundDirector : MonoBehaviour
{
    // シングルトンのインスタンス
    public static SoundDirector Instance { get; private set; }

    public List<AudioSource> audioSources;
    // ここに音声データを格納する変数を定義します
    public AudioClip bgmOpening;
    public AudioClip bgmBattleMain;
    public AudioClip seOnClick;
    public AudioClip seSwordHit;
    public AudioClip seFireBall;
    public AudioClip seHeal;
    public AudioClip seBuff;
    public AudioClip seLightning;
    public AudioClip seLightningHit;
    public AudioClip seIceStart;
    public AudioClip seIceBreak;
    public AudioClip seCatch;
    public AudioClip seWarCry;
    public AudioClip seIncrease;
    private void Awake()
    {
        // すでにインスタンスが存在する場合は破棄
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // このインスタンスをシングルトンとして登録
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     SeOnClick();
        //     Debug.Log("ボタンおされてる");
        // }
    }

    public void SeOnClick()
    {
        PlaySound(seOnClick);
    }

    public void PlaySound(AudioClip clip)   //通常
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip;
                audioSource.Play();
                break;
            }
        }
    }
    public void PlayOneShotSound(AudioClip clip)   //頻繁に使う方
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip;
                audioSource.PlayOneShot(clip);
                break;
            }
        }
    }
    public void StopSound()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

}
