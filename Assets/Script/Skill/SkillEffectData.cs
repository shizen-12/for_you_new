using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffectData", menuName = "ScriptableObjects/SkillEffectData", order = 1)]

public class SkillEffectData : ScriptableObject
{
    [SerializeField] GameObject soundObject;
    protected GameObject effectObject;
    public Queue<GameObject> Pool { get; private set; }
    public int poolSize = 1;   //オブジェクトプールサイズ
    protected GameObject effectParent;
    // Start is called before the first frame update
    public void Initialize(GameObject effectObject) //サウンド実装時には引数を増やす。
    {
        this.effectObject = effectObject;
        Pool = new Queue<GameObject>();
        Pool.Enqueue(effectObject);
        effectParent = GameObject.Find("EffectParent");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject ActivateSkill()
    {
        GameObject effectInstance;
        // Debug.Log(Pool.Count + "プールカウント");
        if (Pool.Count > 0)
        {
            effectInstance = Pool.Dequeue();    //キューから取り出して再生
        }
        else
        {
            effectInstance = Instantiate(effectObject);
            effectInstance.transform.SetParent(effectParent.transform, false);
        }
        effectInstance.SetActive(true);
        ParticleSystem effect = effectInstance.GetComponent<ParticleSystem>();
        effect.Play();
        // StartCoroutine(ReturnSkill(effectInstance, effect.main.duration));
        return effectInstance;
    }

    public IEnumerator ReturnSkill(GameObject effectInstance, float deley)
    {
        yield return new WaitForSeconds(deley);
        // Debug.Log(effectInstance);
        // Debug.Log(Pool);
        effectInstance.SetActive(false); //非アクティブ化して
        Pool.Enqueue(effectInstance);    //プールに戻す、ちゃぽん
        // Debug.Log("しまったよ");
    }

}
