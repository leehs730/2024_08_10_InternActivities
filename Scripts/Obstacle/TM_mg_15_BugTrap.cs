using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_mg_15_BugTrap : MonoBehaviour
{
    public GameObject BugPrefab;
    private bool isRaycastHit = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isRaycastHit)
        {
            bugTrapRayCast();
        }

    }

    private void bugTrapRayCast()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, transform.right, 10f, LayerMask.GetMask("TransparentFX"));
        RaycastHit2D hit2 = Physics2D.Raycast(this.transform.position, -transform.right, 10f, LayerMask.GetMask("TransparentFX"));

        if(hit.collider != null || hit2.collider != null)
        {
            //Debug.Log("충돌됨");
            int idx = Random.Range(0, 3);
            if (idx >= 0)
            {
                Instantiate(BugPrefab, transform.position, transform.rotation);
            }
            isRaycastHit = true;
        }
    }
}
