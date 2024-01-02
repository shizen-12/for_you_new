using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicIce : SkillEffect
{
    float consumeMp = 7;
    override public void GetSkillEffectData()
    {
        skillEffectData = EffectParent.Instance.ice;
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
            float dmg = cs.Matk * 1.2f;
            enemy.TakeDmg(dmg);
            SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seIceStart);
            yield return new WaitForSeconds(1.5f);
            SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seIceBreak);
            dmg = cs.Matk * 3;
            enemy.TakeDmg(dmg);
        }
        else
        {
            NoMp();
        }
        yield return base.DoSkillScript(cs, enemy);
    }
}
