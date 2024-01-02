using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeed : SkillEffect
{
    override public void GetSkillEffectData()
    {
        skillEffectData = EffectParent.Instance.speed;
    }
    void Update()
    {

    }
    public override IEnumerator DoSkillScript(CharacterScript cs, Enemy enemy)
    {
        float gainAspd = cs.AttackInterval * 0.05f;
        cs.AttackInterval -= gainAspd;
        SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seBuff);

        yield return base.DoSkillScript(cs, enemy);
    }
}
