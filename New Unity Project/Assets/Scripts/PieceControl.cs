using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceControl : MonoBehaviour
{
    public GameObject[] boxes;
    public bool active = true;
    public bool fall = true;
    // Start is called before the first frame update
    void Start()
    {
        //boxes = new GameObject[2];
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
                StartCoroutine("RotateRight");
                StopCoroutine("Deactivate");
                fall = true;
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine("RotateLeft");
                StopCoroutine("Deactivate");
                fall = true;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                transform.position = new Vector3(transform.position.x,
                                                 transform.position.y - 1f,
                                                 transform.position.z);
                StopCoroutine("Deactivate");
                fall = true;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine("MoveRight");
                StopCoroutine("Deactivate");
                fall = true;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine("MoveLeft");
                StopCoroutine("Deactivate");
                fall = true;
            }
            if(fall)
                transform.position = new Vector3(transform.position.x,
                                                 transform.position.y -.02f,
                                                 transform.position.z);
        }
		try
		{
            GameObject child = GetComponentInChildren<Transform>().gameObject;
        }
		catch (MissingReferenceException m)
		{
            m = m;
            Destroy(gameObject);
		}
       
    }


    void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "block" || other.gameObject.tag == "cube" || other.gameObject.tag == "floor")
        {
            StartCoroutine("Deactivate");
        }
	}
    IEnumerator Deactivate()
	{
        fall = false;
        yield return new WaitForSeconds(0.5f);

		//gameObject.GetComponent<Rigidbody>().useGravity = true;
		for (int i = 0; i < boxes.Length; i++)
		{
            boxes[i].GetComponent<Rigidbody>().useGravity = true;
            boxes[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            boxes[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            boxes[i].GetComponent<BoxCollider>().enabled = true;
        }

        BoxCollider[] b = this.gameObject.GetComponents<BoxCollider>();

		foreach (BoxCollider box in b)
		{
            box.enabled = false;
		}
        active = false;
	}
    IEnumerator MoveLeft()
    {
        for (int i = 0; i < 20; i++)
        {
            transform.position = new Vector3(transform.position.x - 0.05f,
                                                transform.position.y,
                                                transform.position.z);
            yield return new WaitForSeconds(0.00001f);
        }
    }
    IEnumerator MoveRight()
    {
        for (int i = 0; i < 20; i++)
        {
            transform.position = new Vector3(transform.position.x + 0.05f,
                                                 transform.position.y,
                                                 transform.position.z);
            yield return new WaitForSeconds(0.00001f);
        }
    }
    IEnumerator RotateRight()
    {
		for (int i = 0; i < 45; i++)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                                                             transform.localEulerAngles.y,
                                                             transform.localEulerAngles.z - 2.0f);
            yield return new WaitForSeconds(0.0001f);
        }
    }
    IEnumerator RotateLeft()
    {
        for (int i = 0; i < 45; i++)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                                                             transform.localEulerAngles.y,
                                                             transform.localEulerAngles.z + 2.0f);
            yield return new WaitForSeconds(0.0001f);
        }
    }
}
