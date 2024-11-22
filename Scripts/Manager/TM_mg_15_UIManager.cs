using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TM_mg_15_UIManager : MonoBehaviour
{
    public TextMeshProUGUI Score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Collect Coin : " + TM_mg_15_GameManager.instance.CollectCoin.ToString();
    }
}
