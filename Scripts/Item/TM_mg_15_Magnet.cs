using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_mg_15_Magnet : MonoBehaviour
{
    public void ActiveMagnet()
    {
        StartCoroutine("Magnet");
    }

    private IEnumerator Magnet()
    {
        TM_mg_15_PlayerSFX.instance.MagnetSound();
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        foreach (GameObject coin in TM_mg_15_GameManager.instance.SpawnCoinList)
        {
            TM_mg_15_Coin Magnet = coin.GetComponent<TM_mg_15_Coin>();
            Magnet.AllowToMagnet = true;
        }
        yield return new WaitForSeconds(0.7f);

        foreach (GameObject coin in TM_mg_15_GameManager.instance.SpawnCoinList)
        {
            TM_mg_15_Coin Magnet = coin.GetComponent<TM_mg_15_Coin>();
            Magnet.AllowToMagnet = false;
        }
        Destroy(gameObject);
    }
}
