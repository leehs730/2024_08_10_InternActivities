using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemEnum
{
    Coin = 0,
    Magnet = 1,
    Boost = 2,
    Barrier = 3
}

public class TM_mg_15_Item : MonoBehaviour
{
    public ItemEnum ItemClass;

    private void OnBecameVisible()
    {
        Debug.Log($"{gameObject.name} 이 카메라 안에 나타났습니다");
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeInHierarchy)
        {
            Debug.Log($"{gameObject.name} 이 밖으로 나갔습니다.");
            StartCoroutine(SelfDestroy());
        }      
    }

    private IEnumerator SelfDestroy()
    {
        Debug.Log("자가파괴시작");
        yield return new WaitForSeconds(5f);
        Debug.Log($"{gameObject.name} 의 자가파괴");
        
    }
}
