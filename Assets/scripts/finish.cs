using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finish : MonoBehaviour
{
	public movement mmt;
	public float timeRemaining = 2;
	public int score = 0;
	public bool end = false;
	public Text text;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "balls")
		{
			end = true;
			other.tag = "Untagged";
			mmt.RemoveTopSection();
		}
	}
	void Update()
	{
		text.text = score.ToString();
		if (timeRemaining > 0 && end)
		{
			timeRemaining -= Time.deltaTime;
		}
		if(timeRemaining <= 0)
		{
			end = false;
		}
	}

	public void IncreaseScore(int amount)
	{
		if (end) { score += amount; }
	}
}
