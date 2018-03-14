using UnityEngine;
using System.Collections;

public class CamMove : MonoBehaviour {

    // We get the players transformation to get its position and offset the camera slightly
    Transform player;
    float offsetX;

    // Use this for initialization
    void Start()
    {
        GameObject follow_me = GameObject.FindGameObjectWithTag("Player");

        if (follow_me == null)
        {
            Debug.Log("Couldnt find an object with tag Player");
            return;
        }

        player = follow_me.transform;

        offsetX = transform.position.x - player.position.x;
    }

    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            Vector3 pos = transform.position;
            pos.x = player.position.x + offsetX;
            transform.position = pos;
        }
    }
}
