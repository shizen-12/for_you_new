using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBash : SkillEffect
{
    float consumeMp = 5;
    override public void GetSkillEffectData()
    {
        skillEffectData = EffectParent.Instance.bash;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override IEnumerator DoSkillScript(CharacterScript cs, Enemy enemy)
    {
        if (cs.Mp >= consumeMp)
        {
            float dmg = cs.Atk * 3;
            enemy.TakeDmg(dmg);
            SoundDirector.Instance.PlaySound(SoundDirector.Instance.seSwordHit);
            cs.Mp -= consumeMp;
        }
        else
        {
            NoMp();
        }
        yield return base.DoSkillScript(cs, enemy);
    }
}
