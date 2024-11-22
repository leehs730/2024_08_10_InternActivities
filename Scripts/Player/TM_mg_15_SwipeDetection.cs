using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TM_mg_15_SwipeDetection : MonoBehaviour
{
    private TM_mg_15_InputManager _pInput;

    [SerializeField] private float minDistance = 0.2f;
    [SerializeField, Range(0f, 1f)] private float dirThreshold = 0.9f;

    //[SerializeField] private GameObject trail;

    [SerializeField] private GameObject Player;

    private Vector2 _startPos;
    private float _startTime;

    private Vector2 _endPos;
    private float _endTime;

    public Vector2 virtualCoordinate;
    public int yVirtual = 0;  // 화면 밖으로 나가지 않기 위한 가상 y좌표 체계

    private Coroutine _swipeCor;

    public bool stillAlive;

    // 애니메이터 변수 입력

    //public TM_mg_15_FeverUI feverUI;

    #region Input Methods


    private void SwipeStart(Vector2 pos)
    {
        //Debug.Log("Start");
        _startPos = pos;

        //trail.SetActive(true);
        //trail.transform.position = pos;

        _swipeCor = StartCoroutine(SwipeCor());

        // TODO : Check Second Touch
    }

    private void SwipeEnd(Vector2 pos)
    {
        StopCoroutine(_swipeCor);
        _swipeCor = null;

        //trail.SetActive(false);

        _endPos = pos;
        DetectSwipe();
    }

    private IEnumerator SwipeCor()
    {
        while (true)
        {
            // TODO : Change Camera Position
            //trail.transform.position = _pInput.PrimaryPosition();
            yield return new WaitForEndOfFrame();
        }
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(_startPos, _endPos) >= minDistance)
        {
            // TODO : Swipe Event
            //Debug.DrawLine(_startPos, _endPos, Color.red, 5f);
            Vector2 dir = (_endPos - _startPos).normalized;
            SwipeDirection(dir);
        }
        else
        {
            // TODO : Touch Event
            //Debug.Log("Touched");
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (stillAlive)
        {
            if (Vector2.Dot(Vector2.up, direction) > dirThreshold)
            {
                //Debug.Log("Swipe Up");
                PlayerUp();
            }
            if (Vector2.Dot(Vector2.down, direction) > dirThreshold)
            {
                //Debug.Log("Swipe Down");
                PlayerDown();
            }
            else if (Vector2.Dot(Vector2.left, direction) > dirThreshold)
            {
                //Debug.Log("Swipe Left");
                PlayerLeft();
            }
            else if (Vector2.Dot(Vector2.right, direction) > dirThreshold)
            {
                //Debug.Log("Swipe Right");
                PlayerRight();
            }
        }

    }

    #endregion

    #region Player Direction
    private void PlayerUp()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(Player.transform.position.x - 0.3f, Player.transform.position.y), Vector2.up, 1.5f, LayerMask.GetMask("Water"));
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(Player.transform.position.x + 0.3f, Player.transform.position.y), Vector2.up, 1.5f, LayerMask.GetMask("Water"));
        if (hit.collider == null && hit2.collider == null)
        {
            if(yVirtual < 0)
            {
                Player.transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y + 1.5f);

                // 애니메이션(위로 이동)
                yVirtual++;
            }
        }        
    }

    private void PlayerDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(Player.transform.position.x - 0.3f, Player.transform.position.y), Vector2.down, 1.5f, LayerMask.GetMask("Water"));
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(Player.transform.position.x + 0.3f, Player.transform.position.y), Vector2.down, 1.5f, LayerMask.GetMask("Water"));
        if (hit.collider == null && hit2.collider == null)
        {
            //Debug.Log("Swipe Down");
            Player.transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y - 1.5f);
            //Debug.Log(Player.transform.position);
            CheckYCoordinate();     // 사용 : 차량 속도 증가
            yVirtual--;
            if(yVirtual < -4)
            {
                // 애니메이션(아래로 이동)
                yVirtual += 1;
            }         
        }
        
    }

    private void PlayerRight()
    {
        RaycastHit2D hit = Physics2D.Raycast(Player.transform.position, Player.transform.right, 1f, LayerMask.GetMask("Water"));
        //Debug.DrawRay(Player.transform.position, Vector2.right * 0.8f, Color.yellow);
        if (hit.collider == null)
        {
            if(Player.transform.position.x < 2.3f)
            {
                // 애니메이션(오른쪽 이동)
                Player.transform.position = new Vector2(Player.transform.position.x + 0.6f, Player.transform.position.y);
            }
            
        }
        else if(hit.collider != null)
        {
            //Debug.Log("오른쪽 감지됨");
        }
        
    }

    private void PlayerLeft()
    {
        RaycastHit2D hit = Physics2D.Raycast(Player.transform.position, -Player.transform.right, 1f, LayerMask.GetMask("Water"));
        //Debug.DrawRay(Player.transform.position, Vector2.left * 0.8f, Color.yellow);
        if (hit.collider == null)
        {
            if (Player.transform.position.x > -2.3f)
            {
                // 애니메이션(왼쪽 이동)
                Player.transform.position = new Vector2(Player.transform.position.x - 0.6f, Player.transform.position.y);
            }
            
        }
        else if (hit.collider != null)
        {
            //Debug.Log("왼쪽 감지됨");
        }

    }

    private void RaycastCollider(int h, int v)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(h, v, 0), 1, LayerMask.GetMask("Water"));
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
        }
    }
    #endregion

    #region virtual
    private void CheckYCoordinate() // 아래로 이동시 최저 위치를 확인해 피버 게이지를 채우는 메서드, 지금은 사용 X
    {
        if(virtualCoordinate.y > Player.transform.position.y)
        {
            virtualCoordinate = new Vector2(Player.transform.position.x, Player.transform.position.y);
            TM_mg_15_GameManager.instance.footstep += 1;
        }
    }

    public void resetYCoordinate()
    {
        Debug.Log("부스터로 인해 가상좌표 초기화");
        yVirtual = -3;
        
    }
    #endregion

    #region Unity Methods

    private void Awake()
    {
        stillAlive = true;
        _pInput = GetComponent<TM_mg_15_InputManager>();
        virtualCoordinate = new Vector2(Player.transform.position.x, Player.transform.position.y);
        //Debug.Log(virtualCoordinate.transform.position);
    }

    private void OnEnable()
    {
        _pInput.OnStartTouch += SwipeStart;
        _pInput.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        _pInput.OnStartTouch -= SwipeStart;
        _pInput.OnEndTouch -= SwipeEnd;
    }

    private void OnDestroy()
    {
        _pInput = null;
    }

    #endregion
}
