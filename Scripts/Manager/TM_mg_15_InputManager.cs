using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class TM_mg_15_InputManager : MonoBehaviour
{
    internal TM_mg_15_SwipeControls input;
    internal Camera mainCam;

    //internal PlayerState state;

    //private StateMachine<PlayerInput> _stateMachine;

    #region Events

    public delegate void StartTouch(Vector2 pos);
    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 pos);
    public event StartTouch OnEndTouch;

    #endregion


    #region Unity Methods

    private void Awake()
    {
        input = new TM_mg_15_SwipeControls();
        mainCam = Camera.main;

        //state = new PlayerState();
        // _stateMachine = new StateMachine<PlayerInput>(this, new OnIdle());
        // _stateMachine.AddState(new OnSwipeAndTouch());
        // _stateMachine.AddState(new OnPinch());
    }

    private void OnEnable()
    {
        input.Enable();

        input.Touch.PrimaryContact.started += StartTouchPrimary;
        input.Touch.PrimaryContact.canceled += EndTouchPrimary;
    }

    private void OnDisable()
    {
        input.Touch.PrimaryContact.started -= StartTouchPrimary;
        input.Touch.PrimaryContact.canceled -= EndTouchPrimary;

        input.Disable();
    }

    private void OnDestroy()
    {
        // _stateMachine.RemoveAllState();
        // _stateMachine = null;
        //state = null;

        mainCam = null;
        input = null;
    }

    private void Update()
    {
        //_stateMachine.CurrentState.Update();
    }

    #endregion



    #region Input Methods

    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (OnStartTouch != null)
        {
            OnStartTouch(ScreenToWorld(mainCam,input.Touch.PrimaryPosition.ReadValue<Vector2>()));
        }
    }

    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch(ScreenToWorld(mainCam,input.Touch.PrimaryPosition.ReadValue<Vector2>()));
        }
    }

    #endregion



    #region Utilities

    private Vector3 ScreenToWorld(Camera camera, Vector3 screenPos)
    {
        // TODO : change screenPos.z to flexible value
        screenPos.z = camera.nearClipPlane;
        return mainCam.ScreenToWorldPoint(screenPos);
    }

    public Vector2 PrimaryPosition()
    {
        return ScreenToWorld(mainCam, input.Touch.PrimaryPosition.ReadValue<Vector2>());
    }

    #endregion
}
