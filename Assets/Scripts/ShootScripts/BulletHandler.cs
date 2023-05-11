using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
	public float lifetime = 0.5f;

	private float spawnTime;

	private void OnEnable()
	{
		spawnTime = Time.time;
	}

	private void Update()
	{
		if(Time.time - spawnTime >= lifetime) 
		{
			gameObject.SetActive(false);
		}
	}
}
