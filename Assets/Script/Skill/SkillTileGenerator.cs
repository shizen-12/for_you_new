using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class SkillTileGenerator : MonoBehaviour
{
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
    [SerializeField] float tileSpeed;   //タイルの動く速度
    private List<SkillTileData> skills; //SkillTilDataクラスのリスト
    private int maxRatio;   //合計確率
    private Vector3 generatePos = new Vector3(12, -0.8f, 0);
    void Start()
    {
        skills = new List<SkillTileData>
        {   //skillデータのリスト化、引数はSkillのPrefabと生成確率
            CreateSkillTileData(AttackBash, 10),
            CreateSkillTileData(AttackWarCry, 10),
            CreateSkillTileData(AttackDoubleHit, 10),
            CreateSkillTileData(MagicLightning, 10),
            CreateSkillTileData(MagicFire, 10),
            CreateSkillTileData(MagicIce, 10),
            CreateSkillTileData(HealHp, 8),
            CreateSkillTileData(HealMp, 8),
            CreateSkillTileData(IncreaseMhp, 2),
            CreateSkillTileData(IncreaseMmp, 2),
            CreateSkillTileData(BuffSpeed, 5),
            CreateSkillTileData(BuffParry, 5),
            CreateSkillTileData(BuffAtk, 5),
            CreateSkillTileData(BuffMatk, 5)
        };


        foreach (SkillTileData skill in skills)
        {
            maxRatio += skill.SkillRatio;
        }
        // StartGenerateSkill();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private SkillTileData CreateSkillTileData(GameObject prefab, int skillRatio)
    //ScriptableObjectのインスタンスを作成して、それのInitializeメソッドを呼び出して初期設定する
    {
        SkillTileData skillTileData = ScriptableObject.CreateInstance<SkillTileData>();
        skillTileData.Initialize(prefab, skillRatio);
        return skillTileData;
    }
    public void StartGenerateSkill()
    {
        InvokeRepeating("GenerateSkill", 0, 0.5f);
    }
    void GenerateSkill()
    {
        int flagNum = Random.Range(1, maxRatio + 1);
        foreach (SkillTileData skill in skills)
        {
            if (flagNum <= skill.SkillRatio)
            {
                // GameObject newSkill = Instantiate(skill.Prefab, generatePos, Quaternion.identity);   //タイルの生まれる場所
                GameObject newSkill = skill.GetSkillTile(); //skillをskillクラスのプールから獲得する
                if (newSkill != null)   //nullが返ってくることは基本的にないけども
                {
                    newSkill.transform.position = generatePos;
                    SkillTile skillTileCs = newSkill.GetComponent<SkillTile>();
                    skillTileCs.MoveSkillSlot(skill, tileSpeed);   //skillのGameObjectとskillTileDataクラスも渡す。スキルの移動が終わった時にプールにしまうため
                    break;
                }
            }
            else
            {
                flagNum -= skill.SkillRatio;
            }
        }
    }

    public void StopGenerateSkill()
    {
        CancelInvoke("GenerateSkill");
    }

}
