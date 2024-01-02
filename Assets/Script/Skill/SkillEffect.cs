using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject effectParent;
    EffectParent effectParentScript;
    protected SkillEffectData skillEffectData;
    BattleDirector battleDirector;
    Enemy enemy;
    private void Awake()
    {
        GameObject battleDirectorObject = GameObject.Find("BattleDirector");
        battleDirector = battleDirectorObject.GetComponent<BattleDirector>();
        effectParent = GameObject.Find("EffectParent");
        effectParentScript = effectParent.GetComponent<EffectParent>();
    }
    public IEnumerator Start()
    {
        enemy = battleDirector.enemyScript; //battleDirectorで指定されたenemyScriptを取得
        yield return StartCoroutine(Wait(0.1f));
        GetSkillEffectData();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ActivateSkill(CharacterScript cs)
    {
        GameObject effect = skillEffectData.ActivateSkill();
        ParticleSystem effectPs = effect.GetComponent<ParticleSystem>();
        effectParentScript.ReturnSkill(effect, effectPs.main.duration, skillEffectData);
        StartCoroutine(DoSkillScript(cs, enemy));
    }

    public IEnumerator Wait(float second)
    {
        yield return new WaitForSeconds(second);
    }

    public virtual void GetSkillEffectData()
    {

    }
    public virtual IEnumerator DoSkillScript(CharacterScript cs, Enemy enemy)
    {
        yield return null;
    }
    public void NoMp()
    {
        //MPが足りない！
    }
}
