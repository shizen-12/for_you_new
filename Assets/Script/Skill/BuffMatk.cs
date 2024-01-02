using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMatk : SkillEffect
{
    override public void GetSkillEffectData()
    {
        skillEffectData = EffectParent.Instance.matk;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public override IEnumerator DoSkillScript(CharacterScript cs, Enemy enemy)
    {
        cs.Matk += 2;
        SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seBuff);

        yield return base.DoSkillScript(cs, enemy);
    }
}
