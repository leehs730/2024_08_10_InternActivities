using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_mg_15_SpecialCRT : MonoBehaviour
{

    public IEnumerator PlayerImmortal()
    {
        //Debug.Log("임시 무적 활성");
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
