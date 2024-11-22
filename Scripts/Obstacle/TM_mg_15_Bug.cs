using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_mg_15_Bug : MonoBehaviour
{
    public float BugSpeed;
    private int rotateSpeed;
    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rotateSpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        PlayerTracking();

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    private void PlayerTracking()
    {
        Vector2 dirVec = (Vector2)TM_mg_15_PlayerInstance.instance.transform.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * BugSpeed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;

        Vector2 direction = new Vector2(transform.position.x - TM_mg_15_PlayerInstance.instance.transform.position.x,
                                            transform.position.y - TM_mg_15_PlayerInstance.instance.transform.position.y);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);
        transform.rotation = rotation;
    }
}
