using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public GameObject filler;
    public int filled = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void OnTriggerEnter(Collider other)
	{
        
		if (other.gameObject.tag == "cube")
		{
            print("cube entered at " +this.name);
            filled = 1;
            filler = other.gameObject;
        }

	}
	public void OnTriggerExit(Collider other)
	{
        if (other.gameObject.tag == "cube")
        {
            print("Exit registered at "+this.name);
            filled = 0;
            filler = null;
        }
	}
	public void deleteObject() 
    {
        filled = 0;
        Destroy(filler, .5f);
        filler = null;
    }

}
