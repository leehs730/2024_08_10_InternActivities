using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_mg_15_ObstacleActive : MonoBehaviour
{
    public GameObject[] ObstaclePosition;
    public GameObject[] ObstaclePrefabs;
    public int ObstacleCount; // 장애물 스폰 수
    private int[] randomCount;

    public GameObject[] itemPosition;
    public GameObject[] itemPrefabs;
    private int itemCount;
    private int[] itemSpawnLocation;


    // Start is called before the first frame update
    void Start()
    {

        GameObject ItemPrefab = itemPrefabs[UnityEngine.Random.Range(0, itemPrefabs.Length)];
        bool isTrapSpawn = false;

        randomCount = MakeRandomNumbers(ObstaclePosition.Length); // 장애물 위치에 해당하는 랜덤 숫자를 만듦
        for(int i = 0; i < ObstacleCount + 1; i++)
        {
            int randomindex = randomCount[i];
            Transform spawnPosition = ObstaclePosition[randomindex].transform;
            GameObject obstaclePrefab = ObstaclePrefabs[UnityEngine.Random.Range(0, ObstaclePrefabs.Length)];
            if(i != ObstacleCount)
            {
                GameObject obsSpawn = Instantiate(obstaclePrefab, spawnPosition.position, spawnPosition.rotation);
                obsSpawn.transform.parent = this.transform;
                if(obsSpawn.gameObject.name == "TM_mg_15_Smail_Obstacle(Clone)" && !isTrapSpawn)
                {
                    isTrapSpawn = true;
                    obsSpawn.GetComponent<TM_mg_15_BugTrap>().enabled = true;
                }
            }
            else if(i == ObstacleCount)
            {
                int randomidx = UnityEngine.Random.Range(0, 10);
                int rate = ItemSpawnRate();
                if(randomidx >= rate)
                {
                    GameObject itemSpawn = Instantiate(ItemPrefab, spawnPosition.position, spawnPosition.rotation);
                    //itemSpawn.transform.parent = this.transform;      // 아이템을 도로의 자식 오브젝트로 재배치
                    
                }
                
            }
            
        }
    }

    private int ItemSpawnRate()
    {
        int checkStep = TM_mg_15_GameManager.instance.footstep;
        if (checkStep >= 100)
        {
            return 9;
        }
        else if (checkStep >= 70 && checkStep < 100)
        {
            return 8;
        }
        else if (checkStep >= 40 && checkStep < 70)
        {
            return 5;
        }
        else if (checkStep >= 10 && checkStep < 40)
        {
            return 2;
        }
        else
            return 0;
    }


    // Update is called once per frame
    void Update()
    {
        
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
