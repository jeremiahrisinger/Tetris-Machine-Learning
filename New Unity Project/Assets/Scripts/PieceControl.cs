using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceControl : MonoBehaviour
{
    public bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 
                                                         transform.localEulerAngles.y, 
                                                         transform.localEulerAngles.z-90.0f);
			}
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                transform.position = new Vector3(transform.position.x,
                                                 transform.position.y - 0.1f,
                                                 transform.position.z);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                transform.position = new Vector3(transform.position.x + 1.0f,
                                                 transform.position.y,
                                                 transform.position.z);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                transform.position = new Vector3(transform.position.x - 1.0f,
                                                 transform.position.y,
                                                 transform.position.z);
            }
            transform.position = new Vector3(transform.position.x,
                                                 transform.position.y -.01f,
                                                 transform.position.z);
        }
    }


    void OnTriggerEnter()
	{
        StartCoroutine("Deactivate");
	}
    IEnumerator Deactivate()
	{
        yield return new WaitForSeconds(1.0f);
        active = false;
	}
    IEnumerator MoveLeft()
    {
        yield return new WaitForSeconds(1.0f);
        active = false;
    }
    IEnumerator MoveRight()
    {
        yield return new WaitForSeconds(1.0f);
        active = false;
    }
    IEnumerator RotateRight()
    {
        yield return new WaitForSeconds(1.0f);
        active = false;
    }
    IEnumerator RotateLeft()
    {
        while(transform.local)
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                                                         transform.localEulerAngles.y,
                                                         transform.localEulerAngles.z + 90.0f);
        yield return new WaitForSeconds(0.001f);
        active = false;
    }
}
