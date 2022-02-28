using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multiplyer : MonoBehaviour
{
	public int amount;
	public finish score;
	private void OnCollisionEnter(Collision collision)
	{
		score.IncreaseScore(amount);
	}
}
