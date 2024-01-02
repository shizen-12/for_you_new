using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealMp : SkillEffect
{
    override public void GetSkillEffectData()
    {
        skillEffectData = EffectParent.Instance.healMp;
    }
    void Update()
    {

    }
    public override IEnumerator DoSkillScript(CharacterScript cs, Enemy enemy)
    {
        float value = cs.MaxMp / 2;
        SoundDirector.Instance.PlaySound(SoundDirector.Instance.seHeal);
        cs.Mp += value;
        yield return base.DoSkillScript(cs, enemy);
    }
}
