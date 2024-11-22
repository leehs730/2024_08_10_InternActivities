using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Layouts;

public class TM_mg_15_GameManager : MonoBehaviour
{
    public static TM_mg_15_GameManager instance;

    public Image[] UIHealth;
    public Sprite BrokenHealth;
    public GameObject GameOverPanel;
    public GameObject PausePanel;

    // 스폰된 코인들을 관리할 배열 리스트
    public List<GameObject> SpawnCoinList = new List<GameObject>();

    public int CollectCoin = 0;
    public int LifeGauge = 3;
    public int footstep = 0;

    public TextMeshProUGUI CoinCountText;



    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            footstep = 0;
        }
        else
        {
            Debug.LogWarning("씬에 두개 이상의 게임 매니저 존재함!!!");
            Destroy(gameObject);
        }

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CoinCountText.text = CollectCoin.ToString();
    }


    public void HealthDown()
    {
        if(LifeGauge > 0)
        {
            UIHealth[LifeGauge].sprite = BrokenHealth;

        }
        else
        {
            UIHealth[0].sprite = BrokenHealth;

        }
    }

    // 코인 배열 관리 메서드
    public void AddCoinOnGMList(GameObject Coin)    // 코인 생성
    {
        SpawnCoinList.Add(Coin);
        //Debug.Log("현재 수량 : " + SpawnCoinList.Count);
    }
    
    public void RemoveCoinFromGMList(GameObject Coin)
    {
        if(SpawnCoinList.Contains(Coin))
        {
            SpawnCoinList.Remove(Coin);
            //Debug.Log("파괴되고 남은 수량 : " + SpawnCoinList.Count);
        }
    }


    // 게임 시스템 관리 쪽
    public void GameOver()
    {
        Time.timeScale = 0f;
        GameOverPanel.SetActive(true);
    }

    public void onClickRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void onClickPause()
    {
        Time.timeScale = 0f;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<TM_mg_15_SwipeDetection>().stillAlive = false;
        PausePanel.SetActive(true);
    }

    public void onClickReplay()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<TM_mg_15_SwipeDetection>().stillAlive = true;
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
