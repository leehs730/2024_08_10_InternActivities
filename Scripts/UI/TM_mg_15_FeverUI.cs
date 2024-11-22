using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class TM_mg_15_FeverUI : MonoBehaviour
{
    public Image Gauge;
    public TM_mg_15_PlayerCollision TestCollision;
    public TM_mg_15_Boost TestBoost;

    // Start is called before the first frame update
    void Start()
    {
        TestCollision = GameObject.FindGameObjectWithTag("Player").GetComponent<TM_mg_15_PlayerCollision>();
        TestBoost = gameObject.AddComponent<TM_mg_15_Boost>();
        Gauge.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Gauge.fillAmount == 1f)
        //{
        //    TM_mg_15_PlayerInstance.instance.BoostActive = true;
        //    StartCoroutine("FeverActive");
        //    Debug.Log("피버초기화");
        //    Gauge.fillAmount = 0;
        //}
    }

    public IEnumerator FeverActive()
    {
        TM_mg_15_PlayerSFX.instance.FeverSound();

        foreach (GameObject coin in TM_mg_15_GameManager.instance.SpawnCoinList)
        {
            TM_mg_15_Coin Magnet = coin.GetComponent<TM_mg_15_Coin>();
            Magnet.AllowToMagnet = true;
        }

        Vector2 startPos = (Vector2)TM_mg_15_PlayerInstance.instance.transform.position;
        Vector2 targetPos = (Vector2)TM_mg_15_PlayerInstance.instance.transform.position + new Vector2(0, -15f);
        float elapsedTime = 0;
        TM_mg_15_SwipeDetection swipe = GameObject.FindGameObjectWithTag("GameController").GetComponent<TM_mg_15_SwipeDetection>();
        
        // 터치 비활성화
        swipe.stillAlive = false;

        while (elapsedTime < 1.5f)
        {
            TM_mg_15_PlayerInstance.instance.transform.position = Vector2.Lerp(startPos, targetPos, elapsedTime / 1.5f);
            elapsedTime += Time.deltaTime;
            yield return null;  // 프레임마다 대기
        }
        TM_mg_15_PlayerInstance.instance.transform.position = targetPos;
        //TM_mg_15_GameManager.instance.footstep += 10;

        TM_mg_15_PlayerInstance.instance.BoostActive = false;

        // 터치 활성화
        swipe.stillAlive = true;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<TM_mg_15_SwipeDetection>().virtualCoordinate
            = new Vector2(TM_mg_15_PlayerInstance.instance.transform.position.x, TM_mg_15_PlayerInstance.instance.transform.position.y);
        //Debug.Log(GameObject.FindGameObjectWithTag("Input").GetComponent<TM_mg_15_SwipeDetection>().virtualCoordinate);

        yield return new WaitForSeconds(0.2f);

        foreach (GameObject coin in TM_mg_15_GameManager.instance.SpawnCoinList)
        {
            TM_mg_15_Coin Magnet = coin.GetComponent<TM_mg_15_Coin>();
            Magnet.AllowToMagnet = false;
        }

        // 무적 상태 활성
        TM_mg_15_SpecialCRT playerCRT = TM_mg_15_GameManager.instance.GetComponent<TM_mg_15_SpecialCRT>();
        yield return playerCRT.StartCoroutine("PlayerImmortal");

        

        
    }

}
