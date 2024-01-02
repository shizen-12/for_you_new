using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLightning : SkillEffect
{
    float consumeMp = 10;
    override public void GetSkillEffectData()
    {
        skillEffectData = EffectParent.Instance.lightning;
    }
    void Update()
    {

    }
    public override IEnumerator DoSkillScript(CharacterScript cs, Enemy enemy)
    {
        if (cs.Mp >= consumeMp)
        {
            cs.Mp -= consumeMp;
            float dmg = cs.Matk * 0.7f;
            SoundDirector.Instance.PlaySound(SoundDirector.Instance.seLightning);
            yield return new WaitForSeconds(0.9f);
            SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seLightningHit);
            enemy.TakeDmg(dmg);
            yield return new WaitForSeconds(0.4f);
            SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seLightningHit);
            enemy.TakeDmg(dmg);
            yield return new WaitForSeconds(0.2f);
            SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seLightningHit);
            enemy.TakeDmg(dmg);
            yield return new WaitForSeconds(0.1f);
            SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seLightningHit);
            enemy.TakeDmg(dmg);
            yield return new WaitForSeconds(0.1f);
            SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seLightningHit);
            enemy.TakeDmg(dmg);
            yield return new WaitForSeconds(0.1f);
            SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seLightningHit);
            enemy.TakeDmg(dmg);
            yield return new WaitForSeconds(0.1f);
            SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seLightningHit);
            enemy.TakeDmg(dmg);
            yield return new WaitForSeconds(0.2f);
            SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seLightningHit);
            dmg = cs.Matk * 1.2f;
            enemy.TakeDmg(dmg);
        }
        else
        {
            NoMp();
        }
        yield return base.DoSkillScript(cs, enemy);
    }
}
