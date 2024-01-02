using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWarCry : SkillEffect
{
    float consumeMp = 4;
    override public void GetSkillEffectData()
    {
        skillEffectData = EffectParent.Instance.warCry;
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
            float dmg = cs.Atk * 2.2f;
            SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seWarCry);

            yield return new WaitForSeconds(0.2f);
            enemy.TakeDmg(dmg);
        }
        else
        {
            NoMp();
        }
        yield return base.DoSkillScript(cs, enemy);
    }
}
