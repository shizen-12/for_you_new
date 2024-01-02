using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    // キャラクターを制御するクラス
    float life = 100;
    float maxLife = 100;
    float mp = 100;
    float maxMp = 100;
    float atk = 10;
    float matk = 10;
    float parryCount = 0; //パリィカウント
    Transform charaTransform;
    private bool isAlive = true;
    [SerializeField] float speed;   //登場スピード
    float attackInterval = 1.2f;  //攻撃速度、0で0秒後に再攻撃
    [SerializeField] GameObject attackNormalObject;
    private ParticleSystem attackNormalPs;
    int getSKillCount = 0;
    public float Life
    {
        get { return life; }
        set
        {
            life = Mathf.Clamp(value, 0, MaxLife);
            OnLifeChanged?.Invoke();
            if (life <= 0) { isAlive = false; }
        }
    }
    public float Mp
    {
        get { return mp; }
        set { mp = Mathf.Clamp(value, 0, MaxMp); OnMpChanged?.Invoke(); }
    }
    public float MaxLife //LifeのDelegate
    {
        get { return maxLife; }
        set { maxLife = value; OnMaxLifeChanged?.Invoke(); }
    }
    public float MaxMp
    {
        get { return maxMp; }
        set { maxMp = value; OnMaxMpChanged?.Invoke(); }
    }
    public float Atk
    {
        get { return atk; }
        set { atk = value; OnAtkChanged?.Invoke(); }
    }
    public float Matk
    {
        get => matk;
        set { matk = value; OnMatkChanged?.Invoke(); }
    }
    public float ParryCount
    {
        get => parryCount;
        set { parryCount = value; OnParryCuontChanged?.Invoke(); }
    }
    public bool IsAlive { get => isAlive; set => isAlive = value; }
    public float AttackInterval { get => attackInterval; set => attackInterval = Mathf.Clamp(value, 0.2f, 1.5f); }
    public int GetSKillCount { get => getSKillCount; set => getSKillCount = value; }

    //delegate集
    public delegate void onLifeChanged();
    public event onLifeChanged OnLifeChanged;
    public delegate void onMpChanged();
    public event onMpChanged OnMpChanged;
    public delegate void onMaxLifeChanged();
    public event onMaxLifeChanged OnMaxLifeChanged;
    public delegate void onMaxMpChanged();
    public event onMaxMpChanged OnMaxMpChanged;
    public delegate void onAtkChanged();
    public event onAtkChanged OnAtkChanged;
    public delegate void onMatkChanged();
    public event onMatkChanged OnMatkChanged;
    public delegate void onParryCountChanged();
    public delegate void onDamaged(float value, GameObject target);
    public event onDamaged OnDamaged;
    public delegate void onGainHp(float value, GameObject target);
    public event onGainHp OnGainHp;
    public event onParryCountChanged OnParryCuontChanged;
    private Vector3 startPos = new Vector3(-15, 1.4f, 0);
    public Animator animator;
    AudioSource audioSource;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        charaTransform = gameObject.GetComponent<Transform>();
    }
    void Start()
    {
        StartCoroutine(MoveToStartPosition());
        attackNormalPs = attackNormalObject.GetComponent<ParticleSystem>();
        Life = MaxLife;
        Mp = MaxMp;
        audioSource = GetComponent<AudioSource>();
        ReStart();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator MoveToStartPosition()
    {
        while (charaTransform.position.x <= -1)
        {
            float newX = charaTransform.position.x + speed * Time.deltaTime;
            charaTransform.position = new Vector2(newX, charaTransform.position.y);
            yield return null;
        }
    }
    public void AttackNormal(Enemy enemy)   //通常攻撃
    {
        animator.SetTrigger("Attack");
        attackNormalPs.Play();
        audioSource.PlayOneShot(audioSource.clip);
        enemy.TakeDmg(Atk);
    }

    public void SetRuning(bool isRuning)
    {
        animator.SetBool("isRun", isRuning);
        // Debug.Log(isRuning);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "SkillBox")
        {
            SoundDirector.Instance.PlaySound(SoundDirector.Instance.seCatch);
            SkillEffect effectCs = other.GetComponent<SkillEffect>();
            effectCs.ActivateSkill(this);
            GetSKillCount++;
        }
    }
    public void TakeDmg(float dmg)
    {
        if (ParryCount >= 1)
        {
            ParryCount -= 1;
        }
        else
        {
            Life -= dmg;
            OnDamaged?.Invoke(dmg, gameObject);
        }
    }

    public void GainHp(float hpAmount)
    {
        Life += hpAmount;
        OnGainHp?.Invoke(hpAmount, gameObject);
        // Debug.Log(hpAmount + "回復した");
    }

    public void ReStart()
    {
        Life = MaxLife;
        Mp = MaxMp;
        GetSKillCount = 0;
        ParryCount = 0;
        IsAlive = true;
        animator.SetBool("isDead", false);
    }
    public void ResetDefaultStatus()
    {
        MaxLife = 100;
        MaxMp = 100;
        Atk = 10;
        Matk = 10;
        AttackInterval = 1.2f;
    }

    public void MoveStartPos()
    {
        this.transform.position = startPos;
    }

    public void DeadAnime()
    {
        animator.SetBool("isDead", true);
    }
}
