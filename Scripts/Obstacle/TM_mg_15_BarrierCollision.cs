using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_mg_15_BarrierCollision : MonoBehaviour
{
    public TM_mg_15_GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!TM_mg_15_PlayerInstance.instance.BoostActive)
            {
                //Debug.Log("배리어 파괴");
                StartCoroutine("BlinkPlayer");
            }

        }

        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(other.gameObject);
        }

        
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "item" && !TM_mg_15_PlayerInstance.instance.BoostActive)
        {
            if (other.gameObject.GetComponent<TM_mg_15_Item>().ItemClass == ItemEnum.Barrier)
            {
                if(gameObject.GetComponent<BoxCollider2D>().enabled == true && !TM_mg_15_PlayerInstance.instance.isPlayerInvincible
                        && other.gameObject.GetComponent<TM_mg_15_Item>())
                {
                    Destroy(other.gameObject);
                    TM_mg_15_PlayerSFX.instance.BarrierOnSound();
                }

            }

            else if(other.gameObject.GetComponent<TM_mg_15_Item>().ItemClass == ItemEnum.Magnet)
            {
                if(other.gameObject.GetComponent<TM_mg_15_Magnet>())
                {                   
                    TM_mg_15_Magnet magnet = other.gameObject.GetComponent<TM_mg_15_Magnet>();
                    magnet.ActiveMagnet();
                }
            }
            else if(other.gameObject.GetComponent<TM_mg_15_Item>().ItemClass == ItemEnum.Boost)
            {
                if(other.gameObject.GetComponent<TM_mg_15_Boost>())
                {
                    other.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                    TM_mg_15_Boost boost = other.gameObject.GetComponent<TM_mg_15_Boost>();
                    TM_mg_15_PlayerInstance.instance.BoostActive = true;
                    boost.ActiveBoostItem();
                    TM_mg_15_PlayerSFX.instance.BoostSound();
                }
            }
        }
    }


    private IEnumerator BlinkPlayer()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        TM_mg_15_PlayerInstance.instance.GetComponent<CapsuleCollider2D>().enabled = true;
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
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.SetActive(false);
        TM_mg_15_PlayerInstance.instance.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

    }

    private IEnumerator BarrierActiveMagnet()
    {
        //Debug.Log("자석발동");
        //Debug.Log(CoinObjects.Length);
        foreach (GameObject coin in TM_mg_15_GameManager.instance.SpawnCoinList)
        {
            TM_mg_15_Coin Magnet = coin.GetComponent<TM_mg_15_Coin>();
            Magnet.AllowToMagnet = true;
        }
        yield return new WaitForSeconds(2f);
        foreach (GameObject coin in TM_mg_15_GameManager.instance.SpawnCoinList)
        {
            TM_mg_15_Coin Magnet = coin.GetComponent<TM_mg_15_Coin>();
            Magnet.AllowToMagnet = false;
        }
    }
}
