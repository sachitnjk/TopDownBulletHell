using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputControls : MonoBehaviour
{
	public bool Shoot;

	public void OnShoot(InputValue value)
	{
		ShootInput(value.isPressed);
	}

	public void ShootInput(bool newShootState)
	{
		Shoot = newShootState;
	}
}
