using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_mg_15_CarSpawn : MonoBehaviour
{
    public bool checkPosition;

    private bool isSpecialSpawn = false;    // 스포츠가 스폰 조건
    public Transform[] SpawnPoint;
    public Transform LeftSpawn;
    public Transform RightSpawn;
    public GameObject[] CarList;
    public GameObject SportsCar;

    public Transform[] CoinSpawnList;
    private int[] CoinIndex;
    public int CoinCount;
    public GameObject CoinPrefab;

    private float minDelayTime;
    private GameObject chosenCar;
    private Transform choseSpawn;

    private void Awake()
    {
        isSpecialSpawn = UnityEngine.Random.Range(0, 100) < 10 ? true : false;

        if (!isSpecialSpawn)    // 일반 차량 스폰 준비
        {
            chosenCar = CarList[UnityEngine.Random.Range(0, CarList.Length)];
            minDelayTime = chosenCar.GetComponent<TM_mg_15_CarMoving>().MinDelaytime;
            choseSpawn = SpawnPoint[UnityEngine.Random.Range(0, SpawnPoint.Length)];
            checkPosition = choseSpawn.position.x < 0 ? true : false;
        }
        else                    // 스포츠카 차량 준비
        {
            chosenCar = SportsCar;
            minDelayTime = chosenCar.GetComponent<TM_mg_15_CarMoving>().MinDelaytime;
            choseSpawn = SpawnPoint[UnityEngine.Random.Range(0, SpawnPoint.Length)];
            checkPosition = choseSpawn.position.x < 0 ? true : false;
            if(checkPosition)
            {
                choseSpawn.position = new Vector2(choseSpawn.position.x - 30f, choseSpawn.position.y);
            }
            else
            {
                choseSpawn.position = new Vector2(choseSpawn.position.x + 30f, choseSpawn.position.y);
            }
        }
        

        CoinCount = UnityEngine.Random.Range(2, 4);
        CoinIndex = MakeRandomNumbers(CoinSpawnList.Length);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
        for (int i = 0; i < CoinCount; i++)
        {
            int randomindex = CoinIndex[i];
            Transform CoinspawnPosition = CoinSpawnList[randomindex].transform;
            GameObject coinPrefab = CoinPrefab;
            GameObject CoinSpawn = Instantiate(coinPrefab, CoinspawnPosition.position, CoinspawnPosition.rotation);
            CoinSpawn.transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnRoutine()
    {
        while (true) 
        {
            
            GameObject car = Instantiate(chosenCar, choseSpawn.position, choseSpawn.rotation);
            car.transform.parent = this.transform;
            if(!isSpecialSpawn)
            {
                FootstepCheck(car);
            }
            

            minDelayTime = car.GetComponent<TM_mg_15_CarMoving>().MinDelaytime;

            // 스폰 차 세부 설정
            TM_mg_15_CarMoving carDetail = car.GetComponent<TM_mg_15_CarMoving>();
            if (carDetail != null)
            {
                carDetail.isSpawnLeft = checkPosition;
                carDetail.LeftPoint = LeftSpawn;
                carDetail.RightPoint = RightSpawn;
            }

            yield return new WaitForSeconds(minDelayTime);   // 차량 종류별 스폰 변수를 넣는다 -> minDelayTime
        }

    }

    private void FootstepCheck(GameObject CarChosen)
    {
        int checkStep = TM_mg_15_GameManager.instance.footstep;
        if (checkStep >= 100)
        {
            CarChosen.GetComponent<TM_mg_15_CarMoving>().CarSpeed *= 3f;
            if (CarChosen.GetComponent<TM_mg_15_CarMoving>().CarRank == CarRank.Compact || CarChosen.GetComponent<TM_mg_15_CarMoving>().CarRank == CarRank.Midsize ||
                                CarChosen.GetComponent<TM_mg_15_CarMoving>().CarRank == CarRank.SUV)
            {
                CarChosen.GetComponent<TM_mg_15_CarMoving>().MinDelaytime -= 5.5f;
            }
        }
        else if (checkStep >= 70 && checkStep < 100)
        {
            CarChosen.GetComponent<TM_mg_15_CarMoving>().CarSpeed *= 2f;
            if (CarChosen.GetComponent<TM_mg_15_CarMoving>().CarRank == CarRank.Compact || CarChosen.GetComponent<TM_mg_15_CarMoving>().CarRank == CarRank.Midsize ||
                                CarChosen.GetComponent<TM_mg_15_CarMoving>().CarRank == CarRank.SUV)
            {
                CarChosen.GetComponent<TM_mg_15_CarMoving>().MinDelaytime -= 3f;
            }
        }
        else if (checkStep >= 40 && checkStep < 70)
        {
            CarChosen.GetComponent<TM_mg_15_CarMoving>().CarSpeed += 2f;
            if (CarChosen.GetComponent<TM_mg_15_CarMoving>().CarRank == CarRank.Compact || CarChosen.GetComponent<TM_mg_15_CarMoving>().CarRank == CarRank.Midsize ||
                                CarChosen.GetComponent<TM_mg_15_CarMoving>().CarRank == CarRank.SUV)
            {
                CarChosen.GetComponent<TM_mg_15_CarMoving>().MinDelaytime -= 1f;
            }
        }
        else if (checkStep >= 10 && checkStep < 40)
        {
            CarChosen.GetComponent<TM_mg_15_CarMoving>().CarSpeed += 1f;
            if (CarChosen.GetComponent<TM_mg_15_CarMoving>().CarRank == CarRank.Compact || CarChosen.GetComponent<TM_mg_15_CarMoving>().CarRank == CarRank.Midsize ||
                                CarChosen.GetComponent<TM_mg_15_CarMoving>().CarRank == CarRank.SUV)
            {
                CarChosen.GetComponent<TM_mg_15_CarMoving>().MinDelaytime -= 0.5f;
            }
        }
    }



    /// <summary>
    /// 중복되지 않는 랜덤 숫자 리스트 만들기 (minValue: 0)
    /// </summary>
    /// <param name="maxValue">최대값(제외)</param>
    /// <param name="count">반환 개수</param>
    /// <param name="isDuplicate">중복 숫자 여부</param>
    /// <param name="randomSeed">랜덤 씨드</param>
    /// <returns></returns>
    public static int[] MakeRandomNumbers(int maxValue, int randomSeed = 0)
    {
        return MakeRandomNumbers(0, maxValue, randomSeed);
    }

    /// <summary>
    /// 중복되지 않는 랜덤 숫자 리스트 만들기
    /// </summary>
    /// <param name="minValue">최소값(포함)</param>
    /// <param name="maxValue">최대값(제외)</param>
    /// <param name="count">반환 개수</param>
    /// <param name="isDuplicate">중복 숫자 여부</param>
    /// <param name="randomSeed">랜덤 씨드</param>
    /// <returns></returns>
    public static int[] MakeRandomNumbers(int minValue, int maxValue, int randomSeed = 0)
    {
        if (randomSeed == 0)
            randomSeed = (int)DateTime.Now.Ticks;

        List<int> values = new List<int>();
        for (int v = minValue; v < maxValue; v++)
        {
            values.Add(v);
        }

        int[] result = new int[maxValue - minValue];
        System.Random random = new System.Random(Seed: randomSeed);
        int i = 0;
        while (values.Count > 0)
        {
            int randomValue = values[random.Next(0, values.Count)];
            result[i++] = randomValue;

            if (!values.Remove(randomValue))
            {
                // Exception
                break;
            }
        }

        return result;
    }
}
