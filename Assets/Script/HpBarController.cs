using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject characterObject;
    private CharacterScript cs;
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
        cs = characterObject.GetComponent<CharacterScript>();
        cs.OnLifeChanged += UpdateHpBar;
        cs.OnMaxLifeChanged += UpdateMaxHpBar;
        UpdateMaxHpBar();
        UpdateHpBar();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateMaxHpBar()
    {
        Vector2 size = selfRect.sizeDelta;
        size.x = cs.MaxLife;
        selfRect.sizeDelta = size;
        fillHpBarRect.sizeDelta = size;
        size.x += 5;
        endStarRect.sizeDelta = size;
    }
    public void UpdateHpBar()
    {
        float value;
        value = cs.Life / cs.MaxLife;
        fillHpBarImage.fillAmount = value;
    }
}
