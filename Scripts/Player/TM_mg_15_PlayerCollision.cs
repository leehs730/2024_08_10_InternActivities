using System.Collections;
using System.Collections.Generic;
using TM_mg_13; //  코인 파티클 준비1
using UnityEditor;
using UnityEngine;

public class TM_mg_15_PlayerCollision : MonoBehaviour
{
    public TM_mg_15_GameManager gameManager;
    public TM_mg_15_SwipeDetection inputManager;
    public Sprite CollideImage;
    public TM_mg_13_CoinVFX coinVFXPrefab;      //코인 파티클 준비2
    public TM_mg_13_ScoreUI scoreUI;            //코인 파티클 준비3
    private Sprite originImage;
    // 애니메이터 변수

    // Start is called before the first frame update
    void Start()
    {
        originImage = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 플레이어가 충돌하는 오브젝트 태그에 대해서 반영할 수 있도록 함
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && gameObject.GetComponent<CapsuleCollider2D>().enabled == true &&
            !TM_mg_15_PlayerInstance.instance.isPlayerInvincible)
        {
            if(!TM_mg_15_PlayerInstance.instance.BoostActive)
            {
                if (gameManager.LifeGauge > 1)
                {
                    gameManager.LifeGauge--;
                    gameManager.HealthDown();
                    StartCoroutine("CollisionProgress");
                }
                else
                {
                    gameManager.LifeGauge--;
                    gameManager.HealthDown();
                    gameObject.GetComponent<SpriteRenderer>().sprite = CollideImage;
                    TM_mg_15_PlayerSFX.instance.HeatOnSound();
                    TM_mg_15_SwipeDetection swipe = inputManager.GetComponent<TM_mg_15_SwipeDetection>();
                    swipe.stillAlive = false;
                    
                    TM_mg_15_GameManager.instance.GameOver();
                }
            }
            
        }

        if (other.gameObject.tag == "Item" && other.gameObject.GetComponent<TM_mg_15_Coin>())
        {
            if(other.gameObject.GetComponent<TM_mg_15_Coin>().ItemClass == ItemEnum.Coin)
            {
                Destroy(other.gameObject);
                TM_mg_15_PlayerSFX.instance.CoinSound();
                gameManager.CollectCoin += 1;
                var vfxobj = Instantiate(coinVFXPrefab.gameObject, transform.position, Quaternion.identity);    //코인 파티클 준비4
                vfxobj.GetComponent<TM_mg_13_CoinVFX>().SetSceneUI(scoreUI);            //코인 파티클 준비5
                vfxobj.GetComponent<TM_mg_13_CoinVFX>().OnLifetimeEnd += () => Destroy(vfxobj); // 코인 파티클 스폰 삭제
            }
            
        }

        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(other.gameObject);
        }

        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Item" && !TM_mg_15_PlayerInstance.instance.BoostActive)
        {
            if (other.gameObject.GetComponent<TM_mg_15_Item>())
            {
                if (other.gameObject.GetComponent<TM_mg_15_Item>().ItemClass == ItemEnum.Barrier)
                {
                    if (gameObject.GetComponent<CapsuleCollider2D>().enabled == true && !TM_mg_15_PlayerInstance.instance.isPlayerInvincible)
                    {
                        Destroy(other.gameObject);
                        GameObject player_barrier = transform.GetChild(0).gameObject;
                        CapsuleCollider2D playerCol = gameObject.GetComponent<CapsuleCollider2D>();
                        playerCol.enabled = false;
                        player_barrier.SetActive(true);
                        player_barrier.GetComponent<SpriteRenderer>().enabled = true;
                        TM_mg_15_PlayerSFX.instance.BarrierOnSound();
                    }
                }

                else if (other.gameObject.GetComponent<TM_mg_15_Item>().ItemClass == ItemEnum.Magnet)
                {
                    if (other.gameObject.GetComponent<TM_mg_15_Magnet>())
                    {
                        other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        TM_mg_15_Magnet magnet = other.gameObject.GetComponent<TM_mg_15_Magnet>();
                        magnet.ActiveMagnet();
                    }

                }
                else if (other.gameObject.GetComponent<TM_mg_15_Item>().ItemClass == ItemEnum.Boost)
                {
                    if (other.gameObject.GetComponent<TM_mg_15_Boost>())
                    {
                        TM_mg_15_Boost boost = other.gameObject.GetComponent<TM_mg_15_Boost>();
                        TM_mg_15_PlayerInstance.instance.BoostActive = true;
                        boost.ActiveBoostItem();
                        TM_mg_15_PlayerSFX.instance.BoostSound();
                        inputManager.GetComponent<TM_mg_15_SwipeDetection>().resetYCoordinate();
                    }
                }
            }
        }
    }

    public IEnumerator CollisionProgress()      // 충돌 과정 코루틴
    {
        SpriteRenderer playerSprite = gameObject.GetComponent<SpriteRenderer>();    
        playerSprite.sprite = CollideImage; // 기존 충돌 시 스프라이트 이미지 변경 1

        // 충돌 애니메이션 코드 넣는 곳
        TM_mg_15_PlayerSFX.instance.HeatOnSound();
        yield return new WaitForSeconds(0.3f);
        playerSprite.sprite = originImage; // 기존 충돌 시 스프라이트 이미지 변경 2
        TM_mg_15_PlayerInstance.instance.isPlayerInvincible = true;
        TM_mg_15_PlayerInstance.instance.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(0.5f);
        TM_mg_15_PlayerInstance.instance.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.5f);
        TM_mg_15_PlayerInstance.instance.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(0.5f);
        TM_mg_15_PlayerInstance.instance.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.5f);
        TM_mg_15_PlayerInstance.instance.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(0.5f);
        TM_mg_15_PlayerInstance.instance.isPlayerInvincible = false;
        TM_mg_15_PlayerInstance.instance.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }
}
