using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_mg_15_PlayerInstance : MonoBehaviour
{
    public Transform player;
    public float playerPosX;
    public float playerPosY;
    public bool magneticPlayer = false;
    public bool BoostActive = false;
    public bool isPlayerInvincible = false;
    public static TM_mg_15_PlayerInstance instance;

    private void Awake()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
    }

}
