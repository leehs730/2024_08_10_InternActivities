using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CarRank
{
    Compact, Midsize, SUV, Truck, Bus
}

public class TM_mg_15_CarMoving : MonoBehaviour
{

    public CarRank CarRank;
    public float CarSpeed;
    public float MinDelaytime;
    public bool isSpawnLeft = true;
    public Transform LeftPoint;
    public Transform RightPoint;

    // Start is called before the first frame update
    void Start()
    {
        if(isSpawnLeft)
        {
            transform.Rotate(0, -180f, 0); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawnLeft)
        {
            movingRight();
            if(transform.position.x > RightPoint.position.x)
            {
                Destroy(gameObject);
                //Debug.Log("destroy");
            }
        }
        else
        {
            movingLeft();
            //Debug.Log(RightPoint.position.x);
            if (transform.position.x < LeftPoint.position.x) 
            {
                Destroy(gameObject);
                //Debug.Log("destroy");
            }
        }
    }

    private void movingRight()
    {
        transform.position += Vector3.right * CarSpeed * Time.deltaTime;
    }

    private void movingLeft()
    {
        transform.position += Vector3.left * CarSpeed * Time.deltaTime;
    }
}
