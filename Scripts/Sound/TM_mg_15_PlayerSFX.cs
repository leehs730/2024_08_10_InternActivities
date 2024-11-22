using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_mg_15_PlayerSFX : MonoBehaviour
{
    public static TM_mg_15_PlayerSFX instance;
    public AudioSource PlayerSource;

    [Header("===== In Game SFX =====")]
    public AudioClip CoinFX;
    public AudioClip BoostFX;
    public AudioClip MagnetFX;
    public AudioClip FeverFX;
    public AudioClip BarrierActiveFx;
    public AudioClip HeatFX;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
        PlayerSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CoinSound()
    {
        PlayerSource.Stop();
        PlayerSource.clip = CoinFX;
        if(PlayerSource.clip == CoinFX)
        {
            PlayerSource.Play();
        }
        else
        {
            PlayerSource.clip = CoinFX;
            PlayerSource.Play();
        }
        
    }

    public void BoostSound()
    {
        PlayerSource.clip = BoostFX;
        PlayerSource.Play();
    }

    public void MagnetSound()
    {
        Debug.Log("자석음");
        PlayerSource.clip = MagnetFX;
        PlayerSource.Play();
    }

    public void FeverSound()
    {
        PlayerSource.clip = FeverFX;
        PlayerSource.Play();
    }

    public void BarrierOnSound()
    {
        PlayerSource.clip = BarrierActiveFx;
        PlayerSource.Play();
    }

    public void HeatOnSound()
    {
        PlayerSource.clip = HeatFX;
        PlayerSource.Play();
    }

}
