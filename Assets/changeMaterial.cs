using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMaterial : MonoBehaviour
{
	public Material material;
	private void OnCollisionEnter(Collision collision)
	{
		this.GetComponent<Renderer>().material = material;
	}
}
