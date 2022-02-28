using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	public movement mmt;
	public GameObject sphere; // kürenin kendisi

	public int size;

	public List<GameObject> objectPool = new List<GameObject>();

	private void Start() // en başta topları oluşturup dictionarynin içine atar
	{
		for(int i = 0; i < size ; i++)
		{
			GameObject ball = Instantiate(sphere);
			ball.GetComponent<Index>().index = i;
			ball.SetActive(false);
			objectPool.Add(ball);
		}
		mmt.enabled = true;
	}

	public void DeactiveBall(GameObject ball) //Delete yok unactive etme var isim değişebilir
	{
		if (objectPool.Contains(ball))
		{
			objectPool[ball.GetComponent<Index>().index].SetActive(false);
		}
	}

	public GameObject SpawnFromPool(Vector3 position)
	{
		GameObject objectToSpawn = null;

		foreach (GameObject ball in objectPool)
		{
			if (ball.activeSelf == false)
			{
				objectToSpawn = ball;
				objectToSpawn.SetActive(true);
				objectToSpawn.transform.position = position;

				break;
			}
		}
		return objectToSpawn;
	}
}
