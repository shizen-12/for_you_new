using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    private RectTransform rectTransform;
    private bool isDisplay;
    private Text valueText;
    private float onTime = 0.8f;
    private float time = 0;
    Queue<GameObject> pool;
    private Vector2 pos;
    private void Awake()
    {
        valueText = gameObject.GetComponent<Text>();
        rectTransform = gameObject.GetComponent<RectTransform>();
    }
    private void Start()
    {

    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > onTime)
        {
            gameObject.SetActive(false);
            isDisplay = false;
            time = 0;
            pool.Enqueue(gameObject);
        }
        pos.y += 0.02f;
        rectTransform.position = pos;

    }

    public void SetValue(float value, Queue<GameObject> pool, GameObject target)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
        Debug.Log(target.transform.position + "GameObjectの場所");
        Debug.Log(screenPos + "screenPosの場所");
        screenPos.y = screenPos.y - Screen.height / 2 + 100;
        screenPos.x = screenPos.x - Screen.width / 2;
        rectTransform.anchoredPosition = screenPos;
        Debug.Log(rectTransform.position + "rectTransformの値");
        valueText.text = $"{(int)value}";
        isDisplay = true;
        this.pool = pool;
        pos = rectTransform.position;
    }


}
