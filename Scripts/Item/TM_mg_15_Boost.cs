using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_mg_15_Boost : MonoBehaviour     // 기존 부스트 아이템은 이제 피버랑 합쳐저서 피버 아이템이라고 부른다.
{
    //public ItemEnum ItemClass;
    private Vector2 targetPosition;
    public float duration;
    private SpriteRenderer boostSprite;

    // 플레이어 피버 애니메이션 변수 입력

    //private bool hasMoved = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActiveBoostItem()
    {
        StartCoroutine(BoostAction(duration, new Vector2(0, -15f)));
    }

    public IEnumerator BoostAction(float moveDuration, Vector2 range)
    {
        transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);      // 부스트 아이템 이미지 알파값을 O으로

        // 코인 자석 기능 활성화
        foreach (GameObject coin in TM_mg_15_GameManager.instance.SpawnCoinList)
        {
            TM_mg_15_Coin Magnet = coin.GetComponent<TM_mg_15_Coin>();
            Magnet.AllowToMagnet = true;
        }

        Vector2 startPos = (Vector2)TM_mg_15_PlayerInstance.instance.transform.position;
        Vector2 targetPos = (Vector2)TM_mg_15_PlayerInstance.instance.transform.position + range;
        float elapsedTime = 0;
        TM_mg_15_SwipeDetection swipe = GameObject.FindGameObjectWithTag("GameController").GetComponent<TM_mg_15_SwipeDetection>();

        // 터치 비활성화
        swipe.stillAlive = false;

        // 플레이어 피버 상태 이펙트 활성
        GameObject feverState = TM_mg_15_PlayerInstance.instance.transform.GetChild(1).gameObject;
        feverState.SetActive(true);

        // 피버 애니메이션 활성화

        while (elapsedTime < moveDuration)
        {
            TM_mg_15_PlayerInstance.instance.transform.position = Vector2.Lerp(startPos, targetPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;  // 프레임마다 대기
        }

        // 이동한 거리에서 멈추기
        TM_mg_15_PlayerInstance.instance.transform.position = targetPos;

        TM_mg_15_PlayerInstance.instance.BoostActive = false;

        // 터치 활성화
        swipe.stillAlive = true;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<TM_mg_15_SwipeDetection>().virtualCoordinate 
            = new Vector2(TM_mg_15_PlayerInstance.instance.transform.position.x, TM_mg_15_PlayerInstance.instance.transform.position.y);

        feverState.SetActive(false);

        // 피버 애니메이션 비활성화

        yield return new WaitForSeconds(0.2f);

        // 코인 자석 비활성화
        foreach (GameObject coin in TM_mg_15_GameManager.instance.SpawnCoinList)
        {
            TM_mg_15_Coin Magnet = coin.GetComponent<TM_mg_15_Coin>();
            Magnet.AllowToMagnet = false;
        }

        // 플레이어 무적 상태 활성
        TM_mg_15_SpecialCRT playerCRT = TM_mg_15_GameManager.instance.GetComponent<TM_mg_15_SpecialCRT>();
        yield return playerCRT.StartCoroutine("PlayerImmortal");

        // 부스트 자가 파괴
        //Destroy(gameObject);
    }
}
