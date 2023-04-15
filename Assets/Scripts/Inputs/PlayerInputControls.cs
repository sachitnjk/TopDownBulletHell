using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputControls : MonoBehaviour
{
	[HideInInspector] public Vector2 move;
	[HideInInspector] public bool shoot;
	[HideInInspector] public bool sprint;
	[HideInInspector] public bool SwitchFireMode;

	public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
	}
	public void OnSprint(InputValue value)
	{
		SprintInput(value.isPressed);
	}
	public void OnShoot(InputValue value)
	{
		ShootInput(value.isPressed);
	}
	public void OnSwitchFireMode(InputValue value)
	{
		SwitchFireModeInput(value.isPressed);
	}


	public void MoveInput(Vector2 newMoveInputDirection)
	{
		move = newMoveInputDirection;
	}
	public void SprintInput(bool newSprintState)
	{
		sprint = newSprintState;
	}
	public void ShootInput(bool newShootState)
	{
		shoot = newShootState;
	}
	public void SwitchFireModeInput(bool newFireModeState)
	{
		SwitchFireMode = newFireModeState;
	}

}
