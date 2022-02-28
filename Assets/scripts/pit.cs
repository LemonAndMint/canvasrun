using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pit : MonoBehaviour
{
  public movement mmt;
  public int amount;

  public Animator anim;

  private bool entered = true;
	private void OnTriggerEnter(Collider other)
	{
    if (other.tag == "balls" && entered)
    {
      entered = false;
      anim.SetBool("open", true);
      mmt.RemoveTopSection(amount);
    }
  }
}
