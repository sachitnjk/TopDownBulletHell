using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
	public float lifetime = 0.5f;

	private void Start()
	{
		Invoke("DestroyBulletAfterLifetime", lifetime);
	}
	private void DestroyBulletAfterLifetime()
	{
		gameObject.SetActive(false);
	}
}
