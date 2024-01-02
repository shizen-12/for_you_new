using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool isAlive = true;
    private float attackInterval = 1.2f;
    Animator animator;
    float maxLife = 100;
    float life = 100;
    float atk = 10;
    public float Life //LifeのDelegate
    {
        get { return life; }
        set
        {
            life = Mathf.Clamp(value, 0, MaxLife);
            if (life <= 0) { isAlive = false; }
            OnLifeChanged?.Invoke();
        }
    }
    public delegate void onLifeChanged();
    public event onLifeChanged OnLifeChanged;
    public bool IsAlive { get => isAlive; set => isAlive = value; }
    public float AttackInterval { get => attackInterval; set => attackInterval = Mathf.Clamp(value, 0.3f, 1.5f); }
    public float Atk { get => atk; set => atk = value; }
    public float MaxLife
    {
        get => maxLife;
        set { maxLife = value; OnMaxLifeChanged?.Invoke(); }
    }
    public delegate void onMaxLifeChanged();
    public event onLifeChanged OnMaxLifeChanged;
    public delegate void onDamaged(float value, GameObject target);
    public event onDamaged OnDamged;    //damageを受けた時用Delegate
    float powerUpBase = 1.1f;
    float powerUpBaseAtkInterval = 0.98f;   //atkintervalの増加指数
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRuning(bool isRuning)
    {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isRun", isRuning);
    }

    public void AttackNormal(CharacterScript cs)
    {
        animator.SetTrigger("Attack");
        cs.TakeDmg(Atk);
    }

    public void TakeDmg(float dmg)
    {
        Life -= dmg;
        OnDamged?.Invoke(dmg, gameObject);
        //被弾SE
    }
    public void PowerUpbyKillCount(int killcount)
    {
        MaxLife *= Mathf.Pow(powerUpBase, killcount);
        Atk *= Mathf.Pow(powerUpBase, killcount);
        AttackInterval *= Mathf.Pow(powerUpBaseAtkInterval, killcount);
        Life = MaxLife;
    }


}
