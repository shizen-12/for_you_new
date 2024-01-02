using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BattleDirector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject characterObject;    //キャラクターオブジェクト
    private Animator characterAnimator;
    private Transform characerTransform;
    private CharacterScript characterScript;
    [SerializeField] GameObject enemyObject1;    //エネミーオブジェクト1
    [SerializeField] GameObject enemyObject2;    //エネミーオブジェクト2
    [SerializeField] GameObject enemyObject3;    //エネミーオブジェクト3
    [SerializeField] GameObject enemyObject4;    //エネミーオブジェクト4
    [SerializeField] GameObject enemyObject5;    //エネミーオブジェクト5
    List<GameObject> enemys = new List<GameObject>();
    private Vector3 startPos = new Vector3(38, 1.4f, 0);
    private GameObject enemyObject;
    private Transform enemyTransform;
    public Enemy enemyScript;
    [SerializeField] GameObject back1;  //背景1
    [SerializeField] GameObject back2;  //背景2
    List<RectTransform> backs = new List<RectTransform>();
    [SerializeField] float backImageRollSpeed;
    [SerializeField] float enemySpeed;
    [SerializeField] GameObject enemyHpBarController;
    [SerializeField] GameObject skillTileGenerator;
    private SkillTileGenerator skillTileGeneratorSc;
    private EnemyHpBarController enemyHpBar;
    private bool characterIsRuning = false; //キャラクターが走っている状態かどうか
    private Coroutine playerAttackCoroutine;    //移動中に攻撃を止める
    private Coroutine enemyAttackCoroutine;
    private int killCount = 0;
    public delegate void onKillCountChanged();
    public event onKillCountChanged OnKillCountChanged;
    [SerializeField] GameObject resultPanel;    //終了時に表示するパネル
    public GameObject deathEffectPrefab;
    ParticleSystem deathEffect;
    [SerializeField] DamageHealDirector damageHealDirector;
    public int KillCount { get => killCount; set { killCount = value; OnKillCountChanged?.Invoke(); } }
    IEnumerator Start()
    {
        characterAnimator = characterObject.GetComponent<Animator>();
        characerTransform = characterObject.GetComponent<Transform>();
        characterScript = characterObject.GetComponent<CharacterScript>();
        enemyHpBar = enemyHpBarController.GetComponent<EnemyHpBarController>();
        skillTileGeneratorSc = skillTileGenerator.GetComponent<SkillTileGenerator>();
        deathEffect = deathEffectPrefab.GetComponent<ParticleSystem>();
        enemys.Add(enemyObject1);
        enemys.Add(enemyObject2);
        enemys.Add(enemyObject3);
        enemys.Add(enemyObject4);
        enemys.Add(enemyObject5);
        backs.Add(back1.GetComponent<RectTransform>());
        backs.Add(back2.GetComponent<RectTransform>());
        CreateNewEnemy();
        enemyHpBar.SetEnemyData(enemyScript);
        yield return StartCoroutine(WaitForSeconds(0.1f));
        StartCoroutine(BattleMainLoop());
        SoundDirector.Instance.PlaySound(SoundDirector.Instance.bgmBattleMain);
    }

    void Update()
    {

    }

    void CreateNewEnemy()   //敵キャラクターの生成
    {
        int num = Random.Range(0, enemys.Count);
        enemyObject = enemys[num];
        enemyObject.SetActive(true);
        enemyTransform = enemyObject.GetComponent<Transform>();
        enemyTransform.position = startPos;
        enemyScript = enemyObject.GetComponent<Enemy>();
        enemyScript.Life = enemyScript.MaxLife;
        enemyScript.IsAlive = true;
        enemyScript.PowerUpbyKillCount(KillCount);
        enemyHpBar.SetEnemyData(enemyScript);
        damageHealDirector.SetEnemy(enemyScript);
        //UIの更新

    }
    void ExitEnemy()
    {
        //消える演出
        //撃破カウントの増加
        //スコアの追加
        enemyHpBar.RemoveEnemyData(enemyScript);
        damageHealDirector.RemoveEnemy(enemyScript);
        enemyObject.SetActive(false);
    }
    void ForwardBackImage() //背景を前進
    {
        foreach (RectTransform img in backs)
        {
            if (img.anchoredPosition.x < -960)   //端まで行ったら
            {
                img.anchoredPosition = new Vector2(960, img.anchoredPosition.y);
            }
            else
            {
                float newX = img.anchoredPosition.x - backImageRollSpeed * Time.deltaTime * 100;
                img.anchoredPosition = new Vector2(newX, img.anchoredPosition.y);
            }
        }
    }


    IEnumerator AproachCharacter()
    {
        // 敵キャラクターが特定の位置に来るまで背景、味方キャラRun状態、敵キャラRun状態にする。
        while (enemyTransform.position.x >= 1.0f)
        {
            ForwardBackImage();
            float newX = enemyTransform.position.x - enemySpeed * Time.deltaTime;
            enemyTransform.position = new Vector2(newX, enemyTransform.position.y);
            if (!characterIsRuning)
            {
                characterScript.SetRuning(true);
                enemyScript.SetRuning(true);
                characterIsRuning = true;
            }
            yield return null;
        }
        //キャラクターが走っている状態を止める
        characterScript.SetRuning(false);
        enemyScript.SetRuning(false);
        characterIsRuning = false;
        //skillTile生成開始
        skillTileGeneratorSc.StartGenerateSkill();
    }

    IEnumerator BattleMainLoop()
    {
        while (characterScript.IsAlive)
        {
            //アプローチが終わるまで
            StopAttack();
            yield return StartCoroutine(AproachCharacter());
            playerAttackCoroutine = StartCoroutine(PlayerAttack());
            enemyAttackCoroutine = StartCoroutine(EnemyAttack());
            yield return new WaitUntil(() => !enemyScript.IsAlive || !characterScript.IsAlive);    //EnemyもしくはCharacterが敗北するまで
            if (!enemyScript.IsAlive)       //もしEnemyが敗北していたら、Enemyを作成してループを継続
            {
                ExitEnemy();
                CreateNewEnemy();
                KillCount++;
            }
            StopAttack();
            skillTileGeneratorSc.StopGenerateSkill();
            yield return new WaitForSeconds(1.0f);
        }
        // キャラクター死亡時処理
        DeathEffectPlay(characterObject);
        characterScript.DeadAnime();
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.5f);
        SoundDirector.Instance.StopSound();
        yield return new WaitForSeconds(0.5f);
        //ここにResultジングル
        characterScript.MoveStartPos();
        ExitEnemy();
        resultPanel.SetActive(true);
        ResultManager rm = resultPanel.GetComponent<ResultManager>();
        characterScript.animator.SetBool("isDead", false);
        yield return new WaitForSeconds(0.2f);
        rm.UpdateText();
        Debug.Log("敗北した");
    }

    public void RestartBattle(bool isReset)
    {
        KillCount = 0;
        if (isReset)
        {
            characterScript.ResetDefaultStatus();
        }
        characterScript.ReStart();
        CreateNewEnemy();
        StartCoroutine(characterScript.MoveToStartPosition());
        StartCoroutine(BattleMainLoop());
        SoundDirector.Instance.StopSound();
        SoundDirector.Instance.PlaySound(SoundDirector.Instance.bgmBattleMain);
    }
    IEnumerator PlayerAttack()
    {
        while (characterScript.IsAlive && enemyScript.IsAlive)
        {
            // プレイヤー攻撃メソッド
            characterScript.AttackNormal(enemyScript);
            yield return new WaitForSeconds(characterScript.AttackInterval);
        }
    }

    IEnumerator EnemyAttack()
    {
        while (characterScript.IsAlive && enemyScript.IsAlive)
        {
            // エネミー攻撃メソッド
            enemyScript.AttackNormal(characterScript);
            yield return new WaitForSeconds(enemyScript.AttackInterval);
        }
    }

    void StopAttack()
    {
        if (playerAttackCoroutine != null)
        {
            StopCoroutine(playerAttackCoroutine);
        }
        if (enemyAttackCoroutine != null)
        {
            StopCoroutine(enemyAttackCoroutine);
        }
    }
    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    void DeathEffectPlay(GameObject target)
    {
        deathEffectPrefab.transform.position = target.transform.position;
        deathEffect.Play();
    }
}
