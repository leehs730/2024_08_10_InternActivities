using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_mg_15_Coin : MonoBehaviour
{
    // ������ �÷��̾����� ���� ���� : �÷��̾ �ڼ� �������� �԰� ���ʵ��� �ߵ��� + ������ ���� ī�޶� �ȿ� �����ÿ� => isdragactive�� true�� �ǰ� 
    // ������ �÷��̾�� �̵��Ѵ�.
    public bool IsDragActive;       // �������� �ڼ� ����� �����ϴ� bool
    public bool AllowToMagnet;      // �ڼ� ����� ����ϴ� bool, �ٸ� �������� �������� ����� �ƴ� ����� ����ϴ� �޽��� ����
    public bool IsCameraInside;     // ���� �ڽ��� ����ī�޶� �ȿ� �ִ��� Ȯ��
    public float CoinSpeed;         // ���� ���󰡴� �ӵ�
    public Rigidbody2D rigid;       // ������ �������ִ� rigidbody2d - Kinetic���� ��ũ��Ʈ�θ� �����ϰ� �Ǿ� ����
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
        IsCameraInside = CheckCoinIsInSide(gameObject); // �ǽð����� ī�޶� �ȿ� �ִ��� üũ�ϴ� �޼���

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
