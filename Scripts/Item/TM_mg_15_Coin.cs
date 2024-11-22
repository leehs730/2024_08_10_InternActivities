using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_mg_15_Coin : MonoBehaviour
{
    // 코인이 플레이어한테 가는 조건 : 플레이어가 자석 아이템을 먹고 몇초동안 발동시 + 코인이 게임 카메라 안에 있을시에 => isdragactive가 true가 되고 
    // 코인은 플레이어로 이동한다.
    public bool IsDragActive;       // 실질적인 자석 기능을 가동하는 bool
    public bool AllowToMagnet;      // 자석 기능을 허용하는 bool, 다만 실질적인 움직임을 허용이 아닌 기능을 허용하는 메시지 역할
    public bool IsCameraInside;     // 코인 자신이 메인카메라 안에 있는지 확인
    public float CoinSpeed;         // 코인 따라가는 속도
    public Rigidbody2D rigid;       // 코인을 움직여주는 rigidbody2d - Kinetic으로 스크립트로만 조종하게 되어 있음
    public ItemEnum ItemClass;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        TM_mg_15_GameManager.instance.AddCoinOnGMList(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        IsCameraInside = CheckCoinIsInSide(gameObject); // 실시간으로 카메라 안에 있는지 체크하는 메서드

        if (AllowToMagnet && IsCameraInside)
        {
            IsDragActive = true;
        }
    }

    private void OnDestroy()
    {
        TM_mg_15_GameManager.instance.RemoveCoinFromGMList(this.gameObject);
    }

    private void FixedUpdate()
    {
        if (IsDragActive)
        {
            CoinMagnetic();
        }
        //CoinMagnetic();
    }

    private bool CheckCoinIsInSide(GameObject _coin)
    {
        Camera currentMainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector3 screenPoint = currentMainCamera.WorldToViewportPoint(_coin.transform.position);
        bool isInside = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        return isInside;
    }

    private void CoinMagnetic()
    {
        Vector2 dirVec = (Vector2)TM_mg_15_PlayerInstance.instance.transform.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * CoinSpeed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }
}
