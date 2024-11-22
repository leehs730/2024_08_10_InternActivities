using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_mg_15_WarningTrigger : MonoBehaviour
{
    GameObject Warning;

    private void Awake()
    {
        Warning = transform.GetChild(0).gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            StartCoroutine("ActiveWarning");
        }
    }

    IEnumerator ActiveWarning()
    {
        Color c = Warning.GetComponent<SpriteRenderer>().color;
        for(float alpha = 1f; alpha >= -0.1f; alpha -= 0.01f )
        {
            c.a = alpha;
            Warning.GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(.01f);
        }
    }
}
