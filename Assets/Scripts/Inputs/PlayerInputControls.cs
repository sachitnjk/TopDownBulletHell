using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputControls : MonoBehaviour
{
	[HideInInspector] public Vector2 move;
	[HideInInspector] public bool sprint;
	[HideInInspector] public bool SwitchFireMode;

	[HideInInspector] public PlayerInput _input;
	[HideInInspector] public InputAction _shoot;
	[HideInInspector] public InputAction _switchFireMode;

	private void Awake()
	{
		_input = GetComponent<PlayerInput>();
		_shoot = _input.actions["Shoot"];
		_switchFireMode = _input.actions["SwitchFireMode"];
	}

	public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
	}
	public void OnSprint(InputValue value)
	{
		SprintInput(value.isPressed);
	}


	public void MoveInput(Vector2 newMoveInputDirection)
	{
		move = newMoveInputDirection;
	}
	public void SprintInput(bool newSprintState)
	{
		sprint = newSprintState;
	}

}
