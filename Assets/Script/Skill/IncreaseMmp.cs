using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMmp : SkillEffect
{
    override public void GetSkillEffectData()
    {
        skillEffectData = EffectParent.Instance.increaseMmp;
    }
    void Update()
    {

    }
    public override IEnumerator DoSkillScript(CharacterScript cs, Enemy enemy)
    {
        cs.MaxMp += 10;
        cs.Mp += 999;
        SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seIncrease);

        yield return base.DoSkillScript(cs, enemy);
    }
}
