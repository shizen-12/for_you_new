using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject playerParent;
    [SerializeField] GameObject character;
    private GameObject player;  //playerのroot;
    private Animator animator;
    private Transform pTransform;
    private float threshold = 0.2f; //左右移動の閾値
    [SerializeField] float scale = 3.0f;    //サイズ（反転に使う）
    [SerializeField] float speed;    //プレイヤーの移動速度
    private bool isThrowing;    //投げている状態かどうか
    private GameObject currentSkillBox; //現在接触しているSkillBox
    private Vector3 boxPosition = new Vector3(-0.3f, 0.9f, 0.1f);  //スキルを持っているときの位置
    private Vector3 boxScale = new Vector3(-0.1f, 0.1f, 0);  //スキルを持っているときの大きさ
    // Start is called before the first frame update
    void Start()
    {
        player = playerParent.transform.Find("Player").gameObject;
        animator = player.GetComponent<Animator>();
        pTransform = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float move = speed * Time.deltaTime;
        // 左移動
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            pTransform.localScale = new Vector2(scale, scale);
            if (pTransform.position.x >= -10)
            {
                pTransform.position = new Vector2(-move + pTransform.position.x, pTransform.position.y);
            }
            animator.SetBool("isRun", true);
        }
        // 右移動
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            pTransform.localScale = new Vector2(-scale, scale);
            if (pTransform.position.x <= 10)
            {
                pTransform.position = new Vector2(move + pTransform.position.x, pTransform.position.y);
            }
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
        //投げ
        if (Input.GetKeyDown(KeyCode.Space))    //かつ条件を満たしたとき投げる。
        {
            if (currentSkillBox != null && !isThrowing)
            {
                StartCoroutine(Throw(currentSkillBox));
                currentSkillBox = null;
            }
            // StartCoroutine(test());
        }
    }

    public IEnumerator test()
    {
        Debug.Log("最初");
        SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seIceStart);
        yield return new WaitForSeconds(1.5f);
        SoundDirector.Instance.PlayOneShotSound(SoundDirector.Instance.seIceBreak);
        Debug.Log("最後");
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "SkillBox")
        {
            currentSkillBox = other.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "SkillBox")
        {
            currentSkillBox = null;
        }
    }

    IEnumerator Throw(GameObject skillBox)
    {
        isThrowing = true;
        float time = 0;
        SkillTile skillTile = skillBox.GetComponent<SkillTile>();
        skillTile.StopMoveSkillSlot();
        skillBox.transform.SetParent(this.gameObject.transform);
        skillBox.transform.localPosition = boxPosition;
        skillBox.transform.localScale = boxScale;
        float lerpValue = 0;
        animator.SetTrigger("throw");
        while (time >= 0.4f)
        {
            time += Time.deltaTime;
        }
        yield return new WaitForSeconds(0.5f);
        isThrowing = false;
        skillBox.transform.parent = null;
        while (lerpValue <= 1)
        {
            lerpValue += Time.deltaTime * 2;    //Tileの移動速度
            skillBox.transform.position = Vector3.Lerp(skillBox.transform.position, character.transform.position, lerpValue);
            yield return null;
        }
        skillTile.ReturnPool();
    }

}
