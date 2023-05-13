using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	private PlayerInput _input;
	private PlayerInputControls _playerInputControls;
	private CharacterController _playerCharacterController;

	private InputAction sprintInput;

	private float horizontalInput, verticalInput;
	private float currentSpeed;
	private Vector3 playerDirection;

	[SerializeField] private float moveSpeed;
	[SerializeField] private float sprintSpeed;
	[SerializeField] private float gravity;

	private void Start()
	{
		_input = GetComponent<PlayerInput>();
		_playerInputControls = GetComponent<PlayerInputControls>();
		_playerCharacterController = GetComponent<CharacterController>();

		sprintInput = _input.actions["Sprint"];

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
		_playerCharacterController.Move(playerDirection * currentSpeed * Time.deltaTime);

		// Apply gravity to the character controller
		_playerCharacterController.SimpleMove(Vector3.down * gravity * Time.deltaTime);
	}

	private void PlayerSprint()
	{
		if(sprintInput.WasPressedThisFrame())
		{
			currentSpeed = sprintSpeed;
		}
		else if(sprintInput.WasReleasedThisFrame())
		{
			currentSpeed = moveSpeed;
		}
	}
}
