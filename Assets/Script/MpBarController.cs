using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MpBarController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject characterObject;
    private CharacterScript cs;
    private RectTransform selfRect;
    private Image selfImage;
    [SerializeField] GameObject fillMpBar;  //埋める方
    [SerializeField] GameObject endStar;    //両端のアイコン
    private RectTransform fillMpBarRect;
    private Image fillMpBarImage;
    private RectTransform endStarRect;
    void Start()
    {
        fillMpBarRect = fillMpBar.GetComponent<RectTransform>();
        fillMpBarImage = fillMpBar.GetComponent<Image>();
        endStarRect = endStar.GetComponent<RectTransform>();
        selfRect = gameObject.GetComponent<RectTransform>();
        selfImage = gameObject.GetComponent<Image>();
        cs = characterObject.GetComponent<CharacterScript>();
        cs.OnMpChanged += UpdateMpBar;
        cs.OnMaxMpChanged += UpdateMaxMpBar;
        UpdateMaxMpBar();
        UpdateMpBar();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateMaxMpBar()
    {
        Vector2 size = selfRect.sizeDelta;
        size.x = cs.MaxMp;
        selfRect.sizeDelta = size;
        fillMpBarRect.sizeDelta = size;
        size.x += 5;
        endStarRect.sizeDelta = size;
    }
    public void UpdateMpBar()
    {
        float value;
        value = cs.Mp / cs.MaxMp;
        fillMpBarImage.fillAmount = value;
    }
}
