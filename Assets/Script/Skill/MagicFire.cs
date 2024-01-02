using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFire : SkillEffect
{
    float consumeMp = 15;
    override public void GetSkillEffectData()
    {
        skillEffectData = EffectParent.Instance.fire;
    }
    void Update()
    {

    }
    public override IEnumerator DoSkillScript(CharacterScript cs, Enemy enemy)
    {
        if (cs.Mp >= consumeMp)
        {
            cs.Mp -= consumeMp;
            float dmg = cs.Matk * 8;
            SoundDirector.Instance.PlaySound(SoundDirector.Instance.seFireBall);
            yield return new WaitForSeconds(0.5f);
            enemy.TakeDmg(dmg);
        }
        else
        {
            NoMp();
        }
        yield return base.DoSkillScript(cs, enemy);
    }
}
