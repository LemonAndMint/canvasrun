using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
  public float minDistance;
  public float speed;
  public float timeRemaining;

  public int baselenght;
  public int basewidght;

  public int lenght;
  public int widght;

  public bool constructed = false;
  public bool finish = false;

  public ObjectPooler OP;

  public List<List<Transform>> bodyParts = new List<List<Transform>>();
    
  private Transform curBodyPart;
  private Transform PrevBodyPart;

  void Start()
  {
    for (int i = 0; i < basewidght; i++)
		{
      AddTopParts(baselenght);
		}
    constructed = true;
  }

  // Update is called once per frame
  void Update()
  {
    if (constructed) { Movement(); }

    if(bodyParts[0][0] != null && constructed) {
      transform.Translate(transform.forward * speed * Time.deltaTime);
      transform.position = new Vector3(transform.position.x, bodyParts[0][0].transform.position.y + 4, transform.position.z);
    }
    if(finish)
		{
      if (timeRemaining > 0)
      {
        transform.Translate(transform.forward * speed * Time.deltaTime);
        timeRemaining -= Time.deltaTime;
      }
			else
			{
        this.enabled = false;
      }
    }
  }

  void Movement()
	{
    if(bodyParts[0][0] != null) { 
    foreach(List<Transform> colums in bodyParts)
		{
      colums[0].Translate(transform.forward * speed * Time.deltaTime +
                          Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime); 
      for (int i = 1; i < colums.Count; i++)
      {
        curBodyPart = colums[i];
        if(curBodyPart == null) { break; }
        PrevBodyPart = colums[i - 1];

        Vector3 newpos = new Vector3(PrevBodyPart.position.x, curBodyPart.position.y, PrevBodyPart.position.z);

        curBodyPart.position = Vector3.Slerp(curBodyPart.position,
                                            newpos - Vector3.forward * minDistance,
                                            0.5f); 
      }
    }
    }
  }

  public void AddTopParts(int columnSize)
	{
    if(lenght != columnSize) { lenght = columnSize; }
    widght++;

    List<Transform> column = new List<Transform>();
    
    for(int i = 0; i < columnSize ; i++)
		{
      AddBodyParts(column, i);
		}
    bodyParts.Add(column);
  }
  public void AddToTail(int columnSize)
	{
    for(int m = 0; m < columnSize; m++) { 
	    for (int i = 0; i < bodyParts.Count; i++)
	    {
        AddEndOfTheParts(i);
	    }
      lenght++;
    }
  }
  void AddEndOfTheParts(int w)
	{
    AddBodyParts(bodyParts[w], bodyParts[w].Count);
	}
  void AddBodyParts(List<Transform> column, int index)
  {
    Vector3 startPoint = column.Count - 1 >= 0 ? column[column.Count - 1].position : 
                                                 bodyParts.Count - 1 >= 0 ? bodyParts[bodyParts.Count - 1][0].position + Vector3.left * minDistance - Vector3.back * minDistance : 
                                                                            transform.position;
    GameObject ball = OP.SpawnFromPool(startPoint + Vector3.back * minDistance);
    column.Add(ball.transform);
   
    if(index - 1 >= 0) 
    { 
      ball.GetComponent<Renderer>().material.color = column[index - 1].GetComponent<Renderer>().material.color + new Color(0, 0.1f, 0);
    }
  }

  public void RemoveTopSection(int amount)
	{
    for (int k = 0; k < amount ; k++) { 
      for(int i = 0; i < widght; i++)
		  {
        bodyParts[i][0].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        bodyParts[i][0].gameObject.GetComponent<Rigidbody>().AddRelativeForce(400 * (transform.forward + (Vector3.right + Vector3.up) * Random.Range(-0.5f, 0.5f)));
        for (int m = 1; m <= lenght - 1; m++)
        {
          bodyParts[i][m - 1] = bodyParts[i][m];
        }
        bodyParts[i][lenght - 1] = null;
      }
      lenght--;
    }
  }

  public void RemoveTopSection()
  {
    if(lenght != 0) { 
      for (int i = 0; i < widght; i++)
      {
        if (bodyParts[i][0] != null)
        {
          bodyParts[i][0].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
          bodyParts[i][0].gameObject.GetComponent<Rigidbody>().AddRelativeForce(Random.Range(800, 1000) * (Vector3.forward + (Vector3.right + Vector3.up) * Random.Range(-0.5f, 0.5f)));
        }
        for (int m = 1; m <= lenght - 1; m++)
        {
          bodyParts[i][m - 1] = bodyParts[i][m];
        }
        bodyParts[i][lenght - 1] = null;
      }
      lenght--;
    }
		else
		{
      finish = true;
		}
  }
}
