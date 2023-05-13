using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	private PlayerInput _input;
	private PlayerInputControls _playerInputControls;
	private CharacterController _playerCharacterController;

	private InputAction dashInput;

	private float horizontalInput, verticalInput;
	private float currentSpeed;
	private Vector3 playerDirection;
	private Vector3 lastMovementDirection;

	[SerializeField] private float moveSpeed;
	[SerializeField] private float dashSpeed;
	[SerializeField] private float gravity;

	private void Start()
	{
		_input = GetComponent<PlayerInput>();
		_playerInputControls = GetComponent<PlayerInputControls>();
		_playerCharacterController = GetComponent<CharacterController>();

		dashInput = _input.actions["Dash"];

		currentSpeed = moveSpeed;
	}

	private void Update()
	{
		PlayerSprint();
		PlayerMove();
	}

	private void PlayerMove()
	{
		Vector2 moveDirection = _playerInputControls.move;
		horizontalInput = moveDirection.x;
		verticalInput = moveDirection.y;

		playerDirection = transform.forward * verticalInput + transform.right * horizontalInput;
		lastMovementDirection = playerDirection.normalized;
		_playerCharacterController.Move(playerDirection * currentSpeed * Time.deltaTime);

		_playerCharacterController.SimpleMove(Vector3.down * gravity * Time.deltaTime);
	}

	private void PlayerSprint()
	{
		if(dashInput.WasPressedThisFrame())
		{
			currentSpeed = dashSpeed;
			Dash();
		}
		else if(dashInput.WasReleasedThisFrame())
		{
			currentSpeed = moveSpeed;
		}
	}

	private void Dash()
	{
		_playerCharacterController.Move(lastMovementDirection * currentSpeed);
	}
}
