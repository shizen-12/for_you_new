using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffParry : SkillEffect
{
    override public void GetSkillEffectData()
    {
        skillEffectData = EffectParent.Instance.parry;
    }
    void Update()
    {

    }
    public override IEnumerator DoSkillScript(CharacterScript cs, Enemy enemy)
    {
        cs.ParryCount += 3;
        SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seBuff);

        yield return base.DoSkillScript(cs, enemy);
    }
}
