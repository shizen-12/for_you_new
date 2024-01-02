using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDoubleHit : SkillEffect
{
    float consumeMp = 3;
    override public void GetSkillEffectData()
    {
        skillEffectData = EffectParent.Instance.doubleHit;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public override IEnumerator DoSkillScript(CharacterScript cs, Enemy enemy)
    {
        if (cs.Mp >= consumeMp)
        {
            cs.Mp -= consumeMp;
            float dmg = cs.Atk * 1.5f;
            SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seSwordHit);
            yield return new WaitForSeconds(0.2f);
            enemy.TakeDmg(dmg);
            yield return new WaitForSeconds(1.2f);
            SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seSwordHit);
            enemy.TakeDmg(dmg);
        }
        else
        {
            NoMp();
        }
        yield return base.DoSkillScript(cs, enemy);
    }
}
