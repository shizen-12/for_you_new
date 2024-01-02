using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] GameObject BattleObject;
    [SerializeField] GameObject characterObject;
    CharacterScript cs;
    BattleDirector battleDirector;
    [SerializeField] Text baseStatus;
    [SerializeField] Text specialStatus;
    [SerializeField] GameObject bestScore;
    [SerializeField] GameObject killcountObject;
    // Start is called before the first frame update
    void Start()
    {
        battleDirector = BattleObject.GetComponent<BattleDirector>();
        cs = characterObject.GetComponent<CharacterScript>();
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonSaveRestart()
    {
        SoundDirector.Instance.PlaySound(SoundDirector.Instance.seOnClick);
        bestScore.SetActive(false);
        //GameDirectorのセーブメソッド
        GameDirector.Instance.OnSave();
        //Battleクラスの再スタートメソッド isReset をfalseで送る
        battleDirector.RestartBattle(false);
        gameObject.SetActive(false);
    }

    public void OnButtonNotSaveRestart()
    {
        SoundDirector.Instance.PlaySound(SoundDirector.Instance.seOnClick);
        bestScore.SetActive(false);
        //Battleクラスの再スタートメソッド isReset をtrueで送る
        //GameDirectorの実績セーブ
        GameDirector.Instance.SaveArchives();
        battleDirector.RestartBattle(true);
        gameObject.SetActive(false);
    }

    public void OnButtonSaveExit()
    {
        SoundDirector.Instance.PlaySound(SoundDirector.Instance.seOnClick);
        bestScore.SetActive(false);
        //GameDirectorのセーブメソッド
        GameDirector.Instance.OnSave();
        SoundDirector.Instance.StopSound();
        //OPへシーン移動
        SceneManager.LoadScene("Opening");
    }

    void SetBaseStatus()
    {
        baseStatus.text = $"MHP :{cs.MaxLife}\nMMP :{cs.MaxMp}\nATK :{cs.Atk}\nMATK:{cs.Matk}";
    }
    void SetSpecialStatus()
    {
        specialStatus.text = $"通常攻撃間隔\n{cs.AttackInterval}秒\n届けたパネルの合計数\n{GameDirector.Instance.sumGetSkillCount += cs.GetSKillCount}枚";
    }
    void isKillCountBest()
    {
        if (battleDirector.KillCount >= GameDirector.Instance.maxKillCount)
        { bestScore.SetActive(true); }
        else
        {
            bestScore.SetActive(false);
        }
    }
    void killCountUpdate()
    {
        KillCountController k = killcountObject.GetComponent<KillCountController>();
        k.UpdateKillCount();
    }
    public void UpdateText()
    {
        SetBaseStatus();
        SetSpecialStatus();
        killCountUpdate();
        isKillCountBest();
        GameDirector.Instance.firstPlayFlag = 1;
        PlayerPrefs.SetInt("firstPlay", 1);
    }
}
