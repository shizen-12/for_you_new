using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTile : MonoBehaviour
{
    // Start is called before the first frame update
    public SkillTileData skillTileData;
    private Coroutine moveSkillSlotCoroutine; // MoveSkillSlotのコルーチンの参照を保存するための変数
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveSkillSlot(SkillTileData skillData, float tileSpeed)
    {
        skillTileData = skillData;
        moveSkillSlotCoroutine = StartCoroutine(GoMoveSkillSlot(skillData, tileSpeed));
    }

    IEnumerator GoMoveSkillSlot(SkillTileData skillData, float tileSpeed)
    {
        this.transform.localScale = new Vector3(0.35f, 0.35f, 0);
        while (this.transform.position.x > -13)
        {
            this.transform.position = new Vector2(this.transform.position.x - tileSpeed * Time.deltaTime, this.transform.position.y);
            yield return null;
        }
        //skillTileが画面外に出たら受け取ったデータクラスのプールに戻す
        skillData.ReturnSkillTile(this.gameObject);
    }

    public void StopMoveSkillSlot()
    {
        if (moveSkillSlotCoroutine != null)
        {
            StopCoroutine(moveSkillSlotCoroutine);
            moveSkillSlotCoroutine = null;
        }
    }

    public void ReturnPool()
    {
        skillTileData.ReturnSkillTile(this.gameObject);
    }
}
