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
      StartCoroutine(ExampleCoroutine());
    }
  }

  IEnumerator ExampleCoroutine()
	{
    mmt.RemoveTopSection(amount);
    mmt.constructed = false;
    yield return new WaitForSeconds(1f);
    anim.SetBool("open", true);
    yield return new WaitForSeconds(0.25f);
    mmt.constructed = true;
  }
}
