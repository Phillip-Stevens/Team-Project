using UnityEngine;
using System.Collections;

public class VertSaw : MonoBehaviour {


    public bool direction = false; // move horizontal, true for vertical
    public float speed = 5.0f; // travel right by default

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (direction == false) // Were travelling right
        {
            transform.position += new Vector3(0, Time.deltaTime * speed, 0);
        }
        else
        {
            transform.position -= new Vector3(0, Time.deltaTime * speed, 0);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("IN");
        if (col.gameObject.CompareTag("Reverser"))
        {
            direction = !direction; // Set the variable to the opposite of what it is
            Debug.Log("Triggered Reversal");
        }
    }
}
