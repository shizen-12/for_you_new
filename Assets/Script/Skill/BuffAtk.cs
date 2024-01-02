using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAtk : SkillEffect
{
    override public void GetSkillEffectData()
    {
        skillEffectData = EffectParent.Instance.atk;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override IEnumerator DoSkillScript(CharacterScript cs, Enemy enemy)
    {
        cs.Atk += 2;
        SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seBuff);
        yield return base.DoSkillScript(cs, enemy);
    }
}
