using System;
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
	private float dashStartTime;
	private float lastDashTime = -Mathf.Infinity;
	private bool isDashing = false;
	private Vector3 playerDirection;
	private Vector3 lastMovementDirection;

	[Header("Player Movement")]
	[SerializeField] private float moveSpeed;
	[SerializeField] private float dashSpeed;
	[SerializeField] private float dashDistance;
	[SerializeField] private float dashTime;
	[SerializeField] private float dashCooldown;
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
		if(dashInput.WasPressedThisFrame() && Time.time > lastDashTime + dashCooldown)
		{
			currentSpeed = dashSpeed;
			StartCoroutine(DashCoroutine());
			lastDashTime = Time.time;
		}
		else if(dashInput.WasReleasedThisFrame())
		{
			currentSpeed = moveSpeed;
		}
	}

	private IEnumerator DashCoroutine()
	{
		isDashing = true;
		Vector3 startPosition = transform.position;
		Vector3 endPosition = startPosition + lastMovementDirection * dashDistance;

		float elapsedTime = 0f;
		while (elapsedTime < dashTime)
		{
			transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / dashTime);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		transform.position = endPosition;
		isDashing = false;
	}
}
