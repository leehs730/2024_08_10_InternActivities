using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_mg_15_RoadRemover : MonoBehaviour
{
    public GameObject[] roadList;
    public GameObject[] roadList1;
    public GameObject[] roadList2;

    private List<GameObject[]> RoadCollection = new List<GameObject[]>();
    public Transform spawnPosition;
    private GameObject[] currentList;

    private int circulateIndex;             // 도로 게임 오브젝트 배열 인덱스

    private int currentCollectionIndex;     // 배열 인덱스


    private void Awake()
    {
        RoadCollection.Add(roadList);
        RoadCollection.Add(roadList1);
        RoadCollection.Add(roadList2);
    }
    // Start is called before the first frame update
    void Start()
    {
        currentCollectionIndex = UnityEngine.Random.Range(0, RoadCollection.Count);
        currentList = RoadCollection[currentCollectionIndex];
        circulateIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "EraseObject")
        {
            spawnPosition = other.gameObject.transform;
            spawnPosition.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y - 22.5f, spawnPosition.position.z);
            Destroy(other.gameObject);

            //Debug.Log(index);
            if(circulateIndex == RoadCollection[currentCollectionIndex].Length)
            {
                currentCollectionIndex = Random.Range(0, RoadCollection.Count);
                currentList = RoadCollection[currentCollectionIndex];
                circulateIndex = 0;
            }

            Instantiate(currentList[circulateIndex], spawnPosition.position, spawnPosition.rotation);
            circulateIndex++;
        }
    }
}
