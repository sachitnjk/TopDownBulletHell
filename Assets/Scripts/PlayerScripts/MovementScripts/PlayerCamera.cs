using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerCamera : MonoBehaviour
{
	[SerializeField] private Transform playerTransform;

	[Header("Camera Position and sensitivity")]
	[SerializeField] private Vector3 positionOffset;
	[SerializeField] private Vector3 rotationOffset;
	[SerializeField] private float sensitivity;
	[SerializeField] private float distance;

	[Header("Camera alignment angles")]
	[SerializeField] private float minVerticalAngle;
	[SerializeField] private float maxVerticalAngle;
	[SerializeField] private float minHorizontalAngle;
	[SerializeField] private float maxHorizontalAngle;

	[SerializeField] private PlayerInputControls _input;

	private Vector2 _rotation;
	private float _currentDistance;

	private void Start()
	{
		_currentDistance = distance;
	}

	private void LateUpdate()
	{
		Vector2 mouseDelta = _input.look;

		_rotation += mouseDelta * sensitivity;

		_rotation.y = Mathf.Clamp(_rotation.y, minVerticalAngle, maxVerticalAngle);
		_rotation.x = Mathf.Clamp(_rotation.x, minHorizontalAngle, maxHorizontalAngle);

		Quaternion rotation = Quaternion.Euler(_rotation.y + rotationOffset.x, _rotation.x + rotationOffset.y, 0 + rotationOffset.z);
		Vector3 position = playerTransform.position - (rotation * Vector3.forward * _currentDistance) + positionOffset;

		transform.rotation = rotation;
		transform.position = position;

		Quaternion playerRotation = Quaternion.Euler(0f, _rotation.x, 0f);
		playerTransform.rotation = playerRotation;
	}

	public Transform GetPlayerTransform()
	{
		return playerTransform;
	}
}
