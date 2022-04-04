using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class InputReader : MonoBehaviour, PlayerControls.IStationActions, PlayerControls.ISpaceActions, PlayerControls.IKiteActions{
	// Gameplay
	public event UnityAction<Vector2> moveEvent;
	public event UnityAction moveEventCancel;
	public event UnityAction jumpEvent;
	public event UnityAction jumpCanceledEvent;
	public event UnityAction attackEvent;
	public event UnityAction attackCanceledEvent;
	public event UnityAction interactEvent;
	public event UnityAction interactPadEvent;
	public event UnityAction pauseEvent;
	public event UnityAction rechargeEvent;
	public event UnityAction<Vector2, bool> lookEvent;
	public event UnityAction dashEvent;
	public event UnityAction soarEvent;
	public event UnityAction soarCanceledEvent;
	public event UnityAction mouseDownEvent;
	public event UnityAction mouseUpEvent;
	public event UnityAction rightClickDownEvent;
	public event UnityAction rightClickUpEvent;
	public event UnityAction <Vector2> mousePositionEvent;
	public event UnityAction  changeCameraEvent;

	//Menu
	public event UnityAction<Vector2> moveSelectionEvent;
	public event UnityAction confirmEvent;
	public event UnityAction cancelEvent;

	// Dialogue
	public event UnityAction<Vector2> onDialogueSelectEvent;
	public event UnityAction advanceDialogueEvent;

	//Space
	public event UnityAction<Vector2> spaceMoveEvent;
	public event UnityAction cancelSpaceMoveEvent;

	private PlayerControls playerControls;



	//Space
	public event UnityAction<Vector2> kiteMoveEvent;
	public event UnityAction kiteCancelMoveEvent;
	//
	private void OnEnable()
	{
		if (playerControls == null)
		{
			playerControls = new PlayerControls();
			playerControls.Station.SetCallbacks(this);
			playerControls.Space.SetCallbacks(this);
			playerControls.Kite.SetCallbacks(this);
		}

		if (SceneManager.GetActiveScene().name == "SpaceToCampus")
		{
			playerControls.Space.Enable();
		}
		//if (SceneManager.GetActiveScene().name == "StationInception")
		//{
		//	playerControls.Space.Enable();
		//}
		else if(SceneManager.GetActiveScene().name == "Kite")
        {
			playerControls.Kite.Enable();
        }
		else
		{
				playerControls.Station.Enable();
		}
	}

	public void EnableSpace()
    {
		playerControls.Station.Disable();
		playerControls.Kite.Disable();
		playerControls.Space.Enable();
	}

	private void OnDisable()
	{
		playerControls.Station.Disable();
		playerControls.Kite.Disable();
		playerControls.Space.Disable();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		if (moveEvent != null && context.phase == InputActionPhase.Performed)
		{
			moveEvent.Invoke(context.ReadValue<Vector2>());
		}

		if (moveEventCancel != null && context.phase == InputActionPhase.Canceled)
		{
			moveEventCancel.Invoke();
		}
	}

	public void OnJump(InputAction.CallbackContext context)
	{
		if (jumpEvent != null
			&& context.phase == InputActionPhase.Performed)
			jumpEvent.Invoke();

		if (jumpCanceledEvent != null
			&& context.phase == InputActionPhase.Canceled)
			jumpCanceledEvent.Invoke();
	}

	public void OnAttack(InputAction.CallbackContext context)
	{
		if (attackEvent != null
			&& context.phase == InputActionPhase.Performed)
			attackEvent.Invoke();
		if (attackCanceledEvent != null
			&& context.phase == InputActionPhase.Canceled)
			attackCanceledEvent.Invoke();
	}

	public void OnInteract(InputAction.CallbackContext context)
	{
		if (interactEvent != null
			&& context.phase == InputActionPhase.Performed)
			interactEvent.Invoke();
	}

	public void OnPause(InputAction.CallbackContext context)
	{
		if (pauseEvent != null
			&& context.phase == InputActionPhase.Performed)
			pauseEvent.Invoke();
	}

	public void OnRecharge(InputAction.CallbackContext context)
	{
		if (rechargeEvent != null
			 && context.phase == InputActionPhase.Performed)
			rechargeEvent.Invoke();
	}

	public void OnDash(InputAction.CallbackContext context)
	{
		if (dashEvent != null
			 && context.phase == InputActionPhase.Performed)
			dashEvent.Invoke();
	}

	public void OnMoveSelection(InputAction.CallbackContext context)
	{
		if (moveSelectionEvent != null)
		{
			moveSelectionEvent.Invoke(context.ReadValue<Vector2>());
		}
	}

	public void OnConfirm(InputAction.CallbackContext context)
	{
		if (confirmEvent != null
			&& context.phase == InputActionPhase.Performed)
			confirmEvent.Invoke();
	}

	public void OnCancel(InputAction.CallbackContext context)
	{
		if (cancelEvent != null
			&& context.phase == InputActionPhase.Performed)
			cancelEvent.Invoke();
	}

	public void OnDialogueSelect(InputAction.CallbackContext context)
	{
		if (onDialogueSelectEvent != null)
		{
			onDialogueSelectEvent.Invoke(context.ReadValue<Vector2>());
		}
	}

	public void OnAdvanceDialogue(InputAction.CallbackContext context)
	{
		if (advanceDialogueEvent != null
			&& context.phase == InputActionPhase.Performed)
			advanceDialogueEvent.Invoke();
	}

    public void OnMouseClick(InputAction.CallbackContext context)
    {
		if (mouseDownEvent != null
			 && context.phase == InputActionPhase.Performed)
			mouseDownEvent.Invoke(); 
		if (mouseUpEvent != null
			  && context.phase == InputActionPhase.Canceled)
			mouseUpEvent.Invoke();
	}

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        if (mousePositionEvent != null
              && context.phase == InputActionPhase.Performed)
            mousePositionEvent.Invoke(context.ReadValue<Vector2>());
	}

    public void OnSoar(InputAction.CallbackContext context)
    {
		if (soarEvent != null
			 && context.phase == InputActionPhase.Performed)
			soarEvent.Invoke();
		if (soarCanceledEvent != null
			  && context.phase == InputActionPhase.Canceled)
			soarCanceledEvent.Invoke();
	}

    public void OnLook(InputAction.CallbackContext context)
    {
		if (lookEvent != null)
		{
			lookEvent.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
		}
	}

    private bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (rightClickDownEvent != null
            && context.phase == InputActionPhase.Performed)
            rightClickDownEvent.Invoke();

        if (rightClickUpEvent != null
            && context.phase == InputActionPhase.Canceled)
            rightClickUpEvent.Invoke();
    }

    public void OnChangeCamera(InputAction.CallbackContext context)
    {
       if(context.phase == InputActionPhase.Performed)
        {
			changeCameraEvent?.Invoke();
        }
    }

    public void OnSpaceMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
			spaceMoveEvent?.Invoke(context.ReadValue<Vector2>());
		if (context.phase == InputActionPhase.Canceled)
			cancelSpaceMoveEvent?.Invoke();
	}

    public void OnMovement(InputAction.CallbackContext context)
    {
		if (kiteMoveEvent != null && context.phase == InputActionPhase.Performed)
		{
			kiteMoveEvent.Invoke(context.ReadValue<Vector2>());
		}

		if (kiteCancelMoveEvent != null && context.phase == InputActionPhase.Canceled)
		{
			kiteCancelMoveEvent.Invoke();
		}
	}

    public void OnInteractPad(InputAction.CallbackContext context)
    {
		if (interactPadEvent != null
			&& context.phase == InputActionPhase.Performed)
			interactPadEvent.Invoke();
	}
}
