using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseBall : MonoBehaviour
{
	public movement mmt;
	public bool lenghtorwidth; //true ise lenght false ise width olacak
	public int amount;

	private bool entered = true;
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "balls" && entered)
		{
			entered = false;
			if (lenghtorwidth) { mmt.AddToTail(amount); }
			else { mmt.AddTopParts(mmt.lenght); }
		}
	}
}
