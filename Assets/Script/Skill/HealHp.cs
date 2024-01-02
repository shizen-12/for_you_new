using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HealHp : SkillEffect
{
    override public void GetSkillEffectData()
    {
        skillEffectData = EffectParent.Instance.healHp;
    }
    void Update()
    {

    }

    public override IEnumerator DoSkillScript(CharacterScript cs, Enemy enemy)
    {
        float value = cs.MaxLife / 3;
        cs.GainHp(value);
        SoundDirector.Instance.PlaySound(SoundDirector.Instance.seHeal);
        yield return base.DoSkillScript(cs, enemy);
    }

}
