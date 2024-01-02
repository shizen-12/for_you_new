using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DamageHealDirector : MonoBehaviour
{
    public Queue<GameObject> characterHpPool;
    public Queue<GameObject> enemyHpPool;
    public Queue<GameObject> healPool;
    private int chpPoolSize = 5;
    private int ePoolSize = 10;
    private int hPoolSize = 3;
    public GameObject dmgObject;
    public GameObject healObject;
    [SerializeField] GameObject characterObject;
    CharacterScript cs;
    [SerializeField] Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        characterHpPool = new Queue<GameObject>();
        enemyHpPool = new Queue<GameObject>();
        healPool = new Queue<GameObject>();
        CreatePool(hPoolSize, healObject, healPool);    //healpool作成
        CreatePool(chpPoolSize, dmgObject, characterHpPool);  //charapool
        CreatePool(ePoolSize, dmgObject, enemyHpPool);    //enemyhppool
        cs = characterObject.GetComponent<CharacterScript>();
        cs.OnGainHp += CharacterTakeHeal;
        cs.OnDamaged += CharacterTakeDmage;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreatePool(int size, GameObject prefab, Queue<GameObject> pool)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject text = Instantiate(prefab);
            text.SetActive(false);
            pool.Enqueue(text);
            text.transform.SetParent(canvas.transform, false);
        }
    }

    public void SetEnemy(Enemy enemyScript) //enemyにセット
    {
        enemyScript.OnDamged += EnemyTakeDmage;
    }
    public void RemoveEnemy(Enemy enemyScript)
    {
        enemyScript.OnDamged -= EnemyTakeDmage;
    }
    public void EnemyTakeDmage(float value, GameObject target)
    {
        DisplayOnText(ePoolSize, dmgObject, enemyHpPool, value, target);
    }
    public void CharacterTakeDmage(float value, GameObject target)
    {
        DisplayOnText(chpPoolSize, dmgObject, characterHpPool, value, target);
    }
    public void CharacterTakeHeal(float value, GameObject target)
    {
        DisplayOnText(hPoolSize, healObject, healPool, value, target);
    }

    public void DisplayOnText(int size, GameObject prefab, Queue<GameObject> pool, float value, GameObject target)
    {
        if (pool.Count > 0)
        {
            //プールから取り出す
            GameObject textObj = pool.Dequeue();
            textObj.SetActive(true);  //可視化
            Damage text = textObj.GetComponent<Damage>();
            text.SetValue(value, pool, target);
        }
        else
        {  //もしプールのサイズが足りなければ新しく作成
            GameObject textObj = Instantiate(prefab);
            textObj.SetActive(true);  //可視化
            Damage text = textObj.GetComponent<Damage>();
            text.SetValue(value, pool, target);
            textObj.transform.SetParent(canvas.transform, false);

        }
    }
}
