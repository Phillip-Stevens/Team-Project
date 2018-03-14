using UnityEngine;
using System.Collections;

public class ModeChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2d(Collider2D col)
    {
        if (col.gameObject.CompareTag("ModeChanger"))
        {
            /*Put code in here to change the mode. Since it isn't put together and its two seperate play
            modes, I dont know how we all want to handle it. Personally, I would make an ENUM in the player script*/ 
        }
    }
}
