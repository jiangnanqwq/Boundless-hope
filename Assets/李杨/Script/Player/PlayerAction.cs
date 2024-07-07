using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAction : MonoBehaviour
{
    private float m_speed;
    public Transform handPoint;
    public Transform shootPoint;
    public GameObject hand;
    public GameObject arrow;//���ȥ�ļ�ʸ
    public GameObject bowAndArrow;//����
    public GameObject knife;
    [Space]
    public GameObject DidUI;
    public GameObject HideEndingUI;
    public GameObject GoodEndingUI;
    private float handDistance = 0.24f;
    private bool isShooting = false;
    private Rigidbody2D rb;

    [HideInInspector] public PlayerAttribute playerAttribute;

    private void Start()
    {
        playerAttribute = GetComponent<PlayerAttribute>();
        rb = GetComponent<Rigidbody2D>();
        m_speed = playerAttribute.playerSpeed;

    }

    void Update()
    {
        //���̿����ƶ�
        PlayerMove();
        //���ƶ� ��ת
        HandMove();
        if (Input.GetMouseButtonDown(0) && CheckNotInUIClick() && isShooting == false && bowAndArrow.activeSelf == true && BagManagement.instance.objs.ContainsKey(2))//2���Ǽ���ID
        {
            StartCoroutine(nameof(PlayerShoot));
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SwitchWeapon();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            playerAttribute.dog.Attack();
        }
    }
    bool CheckNotInUIClick()
    {
        return !BagManagement.IsUIMouseLeftClick && EventSystem.current.currentSelectedGameObject == null;
    }


    //�ƶ�
    float horizontal; //A D ����
    float vertical; //W S �� ��
    public void PlayerMove()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        rb.velocity = m_speed * (Vector3.up * vertical + Vector3.right * horizontal);
        Animator animator = GetComponent<Animator>();
        animator.SetFloat("xValue", horizontal);
        animator.SetFloat("yValue", vertical);
        if (horizontal == 0 && vertical == 0)
        {
            gameObject.GetComponent<PlayerSoundCtr>().CloseWalkAudio();
        }
        else
        {
            gameObject.GetComponent<PlayerSoundCtr>().OpenWalkAudio();
        }
    }
    //���
    IEnumerator PlayerShoot()
    {
        gameObject.GetComponent<PlayerSoundCtr>().PlayerShootAudio();
        isShooting = true;
        Animator animator = hand.GetComponentInChildren<Animator>();
        animator.SetTrigger("isShoot");
        yield return new WaitForSeconds(0.5f);
        GameObject aArow = Instantiate(arrow, shootPoint);
        BagManagement.instance.ObjToBag(2, -1);
        aArow.transform.SetParent(this.gameObject.transform.parent);
        isShooting = false;
    }
    //���ƶ�
    public void HandMove()
    {
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(worldMousePosition.x - handPoint.position.x, worldMousePosition.y - handPoint.position.y);
        direction = direction.normalized;
        hand.transform.position = new Vector2(handPoint.position.x + handDistance * direction.x, handPoint.position.y + handDistance * direction.y);
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        hand.transform.rotation = Quaternion.Euler(0, 0, targetAngle);

        //������������ߵ�״̬���ֵ�λ����ͷ��,�򽻻���Ⱦ���ȼ�
        //if(vertical >0.05f&& (targetAngle >50&& targetAngle<130))
        //{
        //    bowAndArrow.GetComponent<SpriteRenderer>().sortingOrder = 1;
        //    knife.GetComponent<SpriteRenderer>().sortingOrder = 1;
        //    this.GetComponent<SpriteRenderer>().sortingOrder = 2;
        //}
        //else
        //{
        //    bowAndArrow.GetComponent<SpriteRenderer>().sortingOrder = 2;
        //    knife.GetComponent<SpriteRenderer>().sortingOrder = 2;
        //    this.GetComponent<SpriteRenderer>().sortingOrder = 1;
        //}
    }
    //�л�����
    public void SwitchWeapon()
    {
        if (knife.activeSelf == true && bowAndArrow.activeSelf == false)
        {
            knife.SetActive(false);
            bowAndArrow.SetActive(true);
        }
        else if (knife.activeSelf == false && bowAndArrow.activeSelf == true)
        {
            knife.SetActive(true);
            bowAndArrow.SetActive(false);
        }
        else
        {
            knife.SetActive(true);
            bowAndArrow.SetActive(false);
        }
    }
    public void PlayerDie() 
    {
        Instantiate(DidUI);
        GameObject.Find("GameCtr").GetComponent<SumCtr>().endingIndex = 2;
    }
    public void PlayerHideEnding()
    {
        Instantiate(HideEndingUI);
        GameObject.Find("GameCtr").GetComponent<SumCtr>().endingIndex = 1;
    }
    public void PlayerGoodEndint()
    {
        Instantiate(GoodEndingUI);
        GameObject.Find("GameCtr").GetComponent<SumCtr>().endingIndex = 2;
    }

}
