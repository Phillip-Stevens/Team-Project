using UnityEngine;
using System.Collections;

public class GroundMover : MonoBehaviour
{

    int numBGPanels = 5;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Triggered: " + col.name);

        float widthOfBGObject = ((BoxCollider2D)col).size.x;

        Vector3 pos = col.transform.position;

        pos.x += widthOfBGObject * numBGPanels;

        col.transform.position = pos;
    }
}
