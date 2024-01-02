using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OpeningDirector : MonoBehaviour
{
    // Start is called before the first frame update
    public Button noLoadStartButton;
    void Start()
    {
        Deactiveate();
        SoundDirector.Instance.PlaySound(SoundDirector.Instance.bgmOpening);
        // Debug.Log("ここ");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Deactiveate()
    {
        if (GameDirector.Instance.firstPlayFlag == 0)
        {
            noLoadStartButton.interactable = false;
        }
    }
}
