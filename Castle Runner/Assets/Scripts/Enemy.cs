using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
    //-=-=-=-=-=-=-=-=-=-=-=This enemy is a basic AI, just walks backwards, and forwards each time it hits a wall-=-=-=-=-=-=-=-==-=-=-=-
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

    public Transform wallcheck;
    public int speed;
    public bool walkingright = true;

    void FixedUpdate()
    {
        if (walkingright)
        {
            transform.position += Vector3.right * speed * Time.deltaTime; //right
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime; //left
        }
        
        bool wallhit = Physics2D.Linecast(transform.position, wallcheck.position, 1 << LayerMask.NameToLayer("Floor")); //check if hit wall

        if (wallhit) //hit wall
        {
            walkingright = !walkingright;
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1); //flip the character sprite        
        }
    }
}
