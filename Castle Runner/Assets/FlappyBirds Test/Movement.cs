//using UnityEngine;
//using System.Collections;

//public class Movement : MonoBehaviour {

//    // Variables
//    float flapForce = 100f;
//    float deathCooldown;
//    float forwardSpeed = 5f;
//    float maxSpeed = 5f;
//    bool didFlap = false;
//    public bool dead = false;

//    // Parts we need
//    Animator animator;

//    // Use this for initialization
//    void Start()
//    {
//        // Since the animator is stored in the child component of the player object
//        animator = transform.GetComponentInChildren<Animator>();
//    }

//    void Update()
//    {
//        if (dead)
//        {
//            deathCooldown -= Time.deltaTime;

//            if (deathCooldown <= 0)
//            {
//                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
//                {
//                    Application.LoadLevel(Application.loadedLevel);
//                }
//            }
//        }
//        else
//        {
//            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
//            {
//                didFlap = true;
//                Debug.Log("FLAPPING");
//            }
//        }
//    }

//    // Physics upates OP
//    void FixedUpdate()
//    {
//        if (dead)
//        {
//            return;
//        }

//        if(Mathf.Abs(this.GetComponent<Rigidbody2D>().velocity.x) < maxSpeed)
//        {
//            GetComponent<Rigidbody2D>().AddForce(Vector2.right * forwardSpeed);
//        }

//        if (didFlap)
//        {
//            animator.SetTrigger("flap");
//            GetComponent<Rigidbody2D>().AddForce(Vector2.up * flapForce);
//            didFlap = false;
//        }
//    }

//    void OnCollisionEnter2D(Collision2D col)
//    {
//        animator.SetTrigger("Dead");
//        dead = true;
//        deathCooldown = 0.5f;
//    }
//}
