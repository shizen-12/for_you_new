using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCountController : MonoBehaviour
{
    [SerializeField] GameObject battleDirector;
    private BattleDirector battleScript;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        battleScript = battleDirector.GetComponent<BattleDirector>();
        text = gameObject.GetComponent<Text>();
        battleScript.OnKillCountChanged += UpdateKillCount;
        UpdateKillCount();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateKillCount()
    {
        battleScript = battleDirector.GetComponent<BattleDirector>();
        // Debug.Log(battleScript);
        text = gameObject.GetComponent<Text>();
        text.text = $"{battleScript.KillCount}";
        // Debug.Log("updata呼ばれた");
    }
}
