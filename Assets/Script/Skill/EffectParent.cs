using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectParent : MonoBehaviour
{
    public static EffectParent Instance { get; private set; }
    [SerializeField] GameObject AttackBash;
    [SerializeField] GameObject AttackWarCry;
    [SerializeField] GameObject AttackDoubleHit;
    [SerializeField] GameObject MagicLightning;
    [SerializeField] GameObject MagicFire;
    [SerializeField] GameObject MagicIce;
    [SerializeField] GameObject HealHp;
    [SerializeField] GameObject HealMp;
    [SerializeField] GameObject IncreaseMhp;
    [SerializeField] GameObject IncreaseMmp;
    [SerializeField] GameObject BuffSpeed;
    [SerializeField] GameObject BuffParry;
    [SerializeField] GameObject BuffAtk;
    [SerializeField] GameObject BuffMatk;
    public SkillEffectData bash;
    public SkillEffectData warCry;
    public SkillEffectData doubleHit;
    public SkillEffectData lightning;
    public SkillEffectData fire;
    public SkillEffectData ice;
    public SkillEffectData healHp;
    public SkillEffectData healMp;
    public SkillEffectData increaseMhp;
    public SkillEffectData increaseMmp;
    public SkillEffectData speed;
    public SkillEffectData parry;
    public SkillEffectData atk;
    public SkillEffectData matk;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        bash = CreateSkillEffectData(AttackBash);
        warCry = CreateSkillEffectData(AttackWarCry);
        doubleHit = CreateSkillEffectData(AttackDoubleHit);
        lightning = CreateSkillEffectData(MagicLightning);
        fire = CreateSkillEffectData(MagicFire);
        ice = CreateSkillEffectData(MagicIce);
        healHp = CreateSkillEffectData(HealHp);
        healMp = CreateSkillEffectData(HealMp);
        increaseMhp = CreateSkillEffectData(IncreaseMhp);
        increaseMmp = CreateSkillEffectData(IncreaseMmp);
        speed = CreateSkillEffectData(BuffSpeed);
        parry = CreateSkillEffectData(BuffParry);
        atk = CreateSkillEffectData(BuffAtk);
        matk = CreateSkillEffectData(BuffMatk);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private SkillEffectData CreateSkillEffectData(GameObject prefab)
    //ScriptableObjectのインスタンスを作成して、それのInitializeメソッドを呼び出して初期設定する
    {
        SkillEffectData skillEffectData = ScriptableObject.CreateInstance<SkillEffectData>();
        skillEffectData.Initialize(prefab);
        return skillEffectData;
    }

    public void ReturnSkill(GameObject effectInstance, float deley, SkillEffectData skillEffectData)
    {
        StartCoroutine(skillEffectData.ReturnSkill(effectInstance, deley));
    }
}
