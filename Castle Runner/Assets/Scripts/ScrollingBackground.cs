using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour
{
    /* This might not be an optimal solution, I can't help but think that there is a better way of doing this, but I dont know */

    /*-=-=-=-=-=-= KNOWN PROBLEMS -=-=-=-=-=-
        - It always scrolls, it doesn't account for the players speed, maybe "public float speed" can be changed to a forumla working with the players current speed? 
    */
    public GameObject ting;
    public float speed;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //ting.transform.position = new Vector3(ting.transform.position.x, 0.3f, 0); //fix y position, else it jumps with character and looks odd
        Vector2 offset = new Vector2(Time.time * speed, 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
