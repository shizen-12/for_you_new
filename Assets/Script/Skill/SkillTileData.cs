using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillTileData", menuName = "ScriptableObjects/SkillTileData", order = 1)]
public class SkillTileData : ScriptableObject
{
    public GameObject Prefab { get; private set; }
    public int SkillRatio { get; private set; }
    public Queue<GameObject> Pool { get; private set; }
    public int poolSize = 3;   //オブジェクトプールサイズ

    public void Initialize(GameObject prefab, int skillRatio)
    {
        Prefab = prefab;
        SkillRatio = skillRatio;
        Pool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject skillTile = Instantiate(prefab);
            skillTile.SetActive(false);
            Pool.Enqueue(skillTile);
        }

    }
    public GameObject GetSkillTile()
    {
        if (Pool.Count > 0)
        {
            //プールからSkillTileを取り出す
            GameObject skillTile = Pool.Dequeue();
            skillTile.SetActive(true);  //可視化
            return skillTile;
        }
        else
        {  //もしプールのサイズが足りなければ新しく作成し送り返す
            GameObject skillTile = Instantiate(Prefab);
            skillTile.SetActive(true);
            return skillTile;
        }
    }
    public void ReturnSkillTile(GameObject skillTile)
    {
        skillTile.SetActive(false); //非アクティブ可して
        Pool.Enqueue(skillTile);    //プールに戻す、ちゃぽん
    }

}
