using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMhp : SkillEffect
{
    override public void GetSkillEffectData()
    {
        skillEffectData = EffectParent.Instance.increaseMhp;
    }
    void Update()
    {

    }
    public override IEnumerator DoSkillScript(CharacterScript cs, Enemy enemy)
    {
        cs.MaxLife += 10;
        cs.GainHp(999);
        SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seIncrease);

        yield return base.DoSkillScript(cs, enemy);
    }
}
