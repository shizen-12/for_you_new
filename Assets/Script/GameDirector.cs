using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public static GameDirector Instance { get; private set; }
    GameObject characterObject;
    CharacterScript cs;
    GameObject battleDirectorObject;
    BattleDirector battleDirector;
    public int maxKillCount = 0;    //最大Kill
    public int sumGetSkillCount = 0;    //パネル合計数
    public int firstPlayFlag = 0;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // シングルトンとして登録
        Instance = this;
        DontDestroyOnLoad(gameObject);
        firstPlayFlag = PlayerPrefs.GetInt("firstPlay");
    }
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadArchives()
    { //実績のロード合計Killと合計パネル数
        GetScripts();
        maxKillCount = PlayerPrefs.GetInt("MaxKill");
        sumGetSkillCount = PlayerPrefs.GetInt("sumGetSkill");
    }
    public void SaveArchives()
    { //実績のセーブ合計Killと合計パネル数
        GetScripts();
        if (battleDirector.KillCount >= maxKillCount)   //更新作業
        { maxKillCount = battleDirector.KillCount; }
        PlayerPrefs.SetInt("MaxKill", maxKillCount);
        sumGetSkillCount += cs.GetSKillCount;   //スキルカウントの更新作業
        PlayerPrefs.SetInt("sumGetSkill", sumGetSkillCount);
        PlayerPrefs.SetInt("firstPlay", 1);
    }
    public void OnButtonStartLoad()
    {   //シーンの移行とロード
        SoundDirector.Instance.SeOnClick();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("Main");
        SoundDirector.Instance.StopSound();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode loadScene)
    {
        OnLoad();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void OnButtonStartNoLoad()
    {
        SoundDirector.Instance.SeOnClick();
        SceneManager.LoadScene("Main");
        SoundDirector.Instance.StopSound();
    }
    void OnSceneLoadedRestart(Scene scene, LoadSceneMode loadScene)
    {
        LoadArchives();
    }
    public void OnLoad()
    {   //ロード
        LoadArchives(); //Archiveのロード
        cs.MaxLife = PlayerPrefs.GetFloat("Mhp");
        cs.MaxMp = PlayerPrefs.GetFloat("Mmp");
        cs.Atk = PlayerPrefs.GetFloat("Atk");
        cs.Matk = PlayerPrefs.GetFloat("Matk");
        cs.AttackInterval = PlayerPrefs.GetFloat("Aspd");
    }
    public void OnSave()
    {   //save
        SaveArchives(); //Archiveのセーブ
        PlayerPrefs.SetFloat("Mhp", cs.MaxLife);
        PlayerPrefs.SetFloat("Mmp", cs.MaxMp);
        PlayerPrefs.SetFloat("Atk", cs.Atk);
        PlayerPrefs.SetFloat("Matk", cs.Matk);
        PlayerPrefs.SetFloat("Aspd", cs.AttackInterval);
    }
    public void GetScripts()
    {   //キャラクターとBattleのscriptの取得
        characterObject = GameObject.Find("Character");
        cs = characterObject.GetComponent<CharacterScript>();
        battleDirectorObject = GameObject.Find("BattleDirector");
        battleDirector = battleDirectorObject.GetComponent<BattleDirector>();
    }

    public void OnButtonAllDelete()
    {
        SoundDirector.Instance.SeOnClick();
        PlayerPrefs.DeleteAll();
        firstPlayFlag = 0;
        GameObject opDirector = GameObject.Find("OpeningDirector");
        OpeningDirector opCs = opDirector.GetComponent<OpeningDirector>();
        opCs.noLoadStartButton.interactable = false;
    }


}
