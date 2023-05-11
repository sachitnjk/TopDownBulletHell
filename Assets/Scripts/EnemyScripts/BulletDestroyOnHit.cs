using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyOnHit : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Bullet")
		{
			other.gameObject.SetActive(false);
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Bullet")
		{
			other.gameObject.SetActive(false);
		}
	}
}
