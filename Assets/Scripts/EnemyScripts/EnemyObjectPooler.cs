using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPooler : MonoBehaviour
{
	public static EnemyObjectPooler instance;

	public GameObject e_ProjectilePrefab;
	public List<GameObject> e_PooledObjects;
	public int e_CountToPool = 20;
	public bool e_DynamicPool = false;

	[SerializeField] private string currentPrefabTag;

	private void Awake()
	{
		currentPrefabTag = e_ProjectilePrefab.tag;
		if (instance == null)
		{
			instance = this;
		}
		e_PooledObjects = new List<GameObject>();
		for (int i = 0; i < e_CountToPool; i++)
		{
			GameObject instantiatedObject = Instantiate(e_ProjectilePrefab);
			instantiatedObject.SetActive(false);
			e_PooledObjects.Add(instantiatedObject);
		}
	}

	public GameObject GetPooledObject()
	{
		for (int i = 0; i < e_PooledObjects.Count; i++)
		{
			if (!e_PooledObjects[i].activeInHierarchy)
			{
				return e_PooledObjects[i];
			}
			if (e_DynamicPool)
			{
				GameObject instantiatedPrefab = Instantiate(e_ProjectilePrefab);
				instantiatedPrefab.SetActive(false);
				e_PooledObjects.Add(instantiatedPrefab);
				return instantiatedPrefab;
			}
		}
		return null;
	}
}
