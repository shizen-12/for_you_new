using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBarController : MonoBehaviour
{
    // Start is called before the first frame update
    private Enemy EnemyCs;
    private RectTransform selfRect;
    private Image selfImage;
    [SerializeField] GameObject fillHpBar;  //埋める方
    [SerializeField] GameObject endStar;    //両端のアイコン
    private RectTransform fillHpBarRect;
    private Image fillHpBarImage;
    private RectTransform endStarRect;
    void Start()
    {
        fillHpBarRect = fillHpBar.GetComponent<RectTransform>();
        fillHpBarImage = fillHpBar.GetComponent<Image>();
        endStarRect = endStar.GetComponent<RectTransform>();
        selfRect = gameObject.GetComponent<RectTransform>();
        selfImage = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateHpBar()
    {
        float value;
        value = EnemyCs.Life / EnemyCs.MaxLife;
        fillHpBarImage.fillAmount = value;
    }
    public void UpdateMaxHpBar()
    {
        Vector2 size = selfRect.sizeDelta;
        size.x = EnemyCs.MaxLife;
        selfRect.sizeDelta = size;
        fillHpBarRect.sizeDelta = size;
        size.x += 5;
        endStarRect.sizeDelta = size;
    }
    public void RemoveEnemyData(Enemy enemyScript)
    {
        enemyScript.OnLifeChanged -= UpdateHpBar;
        enemyScript.OnMaxLifeChanged -= UpdateMaxHpBar;
    }
    public void SetEnemyData(Enemy enemyScript)
    {
        this.EnemyCs = enemyScript;
        EnemyCs.OnLifeChanged += UpdateHpBar;
        EnemyCs.OnMaxLifeChanged += UpdateMaxHpBar;
        UpdateMaxHpBar();
        UpdateHpBar();
    }
}
