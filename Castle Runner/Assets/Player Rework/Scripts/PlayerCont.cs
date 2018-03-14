using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// ADDITIONS: NEW COLLISION OBJECT TO CHANGE BETWEEN THE TWO ITEMS

public class PlayerCont : MonoBehaviour
{
    /*****************************************************************************/
    //                                    ALL VARRIABLES
    /*****************************************************************************/
    bool completedLevel = false;
    #region ALL VARIABLES
    /*****************************************************************************/
    //                                   COMPONENTS/AUDIO/MISC
    /*****************************************************************************/
    Animator anim;
    Rigidbody2D body;

    //Audio variables
    public AudioSource coin;
    public AudioSource key;
    public AudioSource chest;
    public AudioSource run;
    public AudioClip runSound; 
    public float runSoundSpeed;
    bool soundPlayed = false;
    // Misc
    public Texture Key;
    public Texture Death;
    bool gamePaused = false;
    bool pauseClicked = false;
    float stopTimer = 0.5f;
    bool stalled; 


    /*****************************************************************************/
    //                                   RUNNING VARIABLES
    /*****************************************************************************/
    public float speed;
    public float jumpForce;
    public float moveForce = 0;
    public Transform GroundCheck;
    public Transform EdgeCheck;
    bool hitledge;
    bool canJump;
    bool jump;
    public Camera cam;

    /*****************************************************************************/
    //                                   FLAPPY VARIABLES
    /*****************************************************************************/
    public bool angryKnights = false; // use this to change control schemes
    public float VelocityPerJump = 8f;
    public float XSpeed = 8f;
    bool didFlap = false;
    public bool dead = false;
	int loopchecker = 0; //loop checker, set this back to zero when coming out of flappy mode
    public Vector3 gravity = new Vector3(0, -20f, 0);

    #endregion

    /*****************************************************************************/
    //                                     GUI
    /*****************************************************************************/
    void OnGUI()
    {
        #region GUI METHOD
        GUIStyle label = new GUIStyle();
        label.fontSize = 20;
        label.normal.textColor = Color.white;
        GUIStyle value = new GUIStyle();
        value.fontSize = 20;
        value.normal.textColor = Color.yellow;
        GUIStyle golabel = new GUIStyle();
        golabel.fontSize = 40;
        golabel.normal.textColor = Color.yellow;

        if (completedLevel)
        {
            ScoreKeeper.Save();
            GUI.Label(new Rect(Screen.width / 2 - 80, (Screen.height / 3) - 40, 200, 50), "Level Completed!", golabel);
            golabel.fontSize = 30;
            GUI.Label(new Rect(Screen.width / 2 - 70, (Screen.height / 3) + 10, 200, 50), "Scored: ", golabel);
            golabel.normal.textColor = Color.red;
            GUI.Label(new Rect((Screen.width / 2) + 50, (Screen.height / 3) + 10, 200, 50), ScoreKeeper.score.ToString(), golabel);

            if (GUI.Button(new Rect(Screen.width / 2 - 70, ((Screen.height / 3) * 2) - 40, 200, 50), "Next Level =>"))
            {
                completedLevel = false;
                print(ScoreKeeper.level.ToString());
                if (ScoreKeeper.level == 1)
                {
                    ScoreKeeper.level = 2;
                    ScoreKeeper.score = 0;
                    ScoreKeeper.levelscomplete = 1;
                    ScoreKeeper.health = 100;
                    SceneManager.LoadScene("Level Two");
                }
                else if (ScoreKeeper.level == 2)
                {
                    ScoreKeeper.level = 3;
                    ScoreKeeper.score = 0;
                    ScoreKeeper.levelscomplete = 2;
                    ScoreKeeper.health = 100;
                    SceneManager.LoadScene("Level Three");
                }
                else if (ScoreKeeper.level == 3)
                {
                    ScoreKeeper.level = 2;
                    ScoreKeeper.score = 0;
                    ScoreKeeper.levelscomplete = 1;
                    ScoreKeeper.health = 100;
                    SceneManager.LoadScene("Main Menu");
                }
            }
            else if (GUI.Button(new Rect(Screen.width / 2 - 70, ((Screen.height / 3) * 2) + 20, 200, 50), "<= Return to Main Menu"))
            {
                completedLevel = false;
                SceneManager.LoadScene("MainMenu");
            }
        }

        if (ScoreKeeper.health > 0)
        {
            GUI.Label(new Rect(20, 20, 100, 50), "Gems: ", label);
            GUI.Label(new Rect(100, 20, 100, 50), ScoreKeeper.gems.ToString(), value);
            GUI.Label(new Rect(200, 20, 100, 50), "Score: ", label);
            GUI.Label(new Rect(290, 20, 100, 50), ScoreKeeper.score.ToString(), value);
            GUI.Label(new Rect(390, 20, 100, 50), "Keys: ", label);

            if (ScoreKeeper.keys >= 1)
            {
                GUI.DrawTexture(new Rect(450, 20, 70, 30), Key);
            }
            if (ScoreKeeper.keys >= 2)
            {
                GUI.DrawTexture(new Rect(530, 20, 70, 30), Key);
            }
            if (ScoreKeeper.keys >= 3)
            {
                GUI.DrawTexture(new Rect(610, 20, 70, 30), Key);
            }
            if (ScoreKeeper.keys >= 4)
            {
                GUI.DrawTexture(new Rect(690, 20, 70, 30), Key);
            }
        }
        else
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Death);
        }

        if (ScoreKeeper.health <= 0 && ScoreKeeper.level < 100)
        {
            GUI.Label(new Rect(Screen.width / 2 - 80, (Screen.height / 3) - 40, 200, 50), "Game Over!", golabel);
            golabel.fontSize = 30;
            GUI.Label(new Rect(Screen.width / 2 - 70, (Screen.height / 3) + 10, 200, 50), "Scored: ", golabel);         
            golabel.normal.textColor = Color.red;
            GUI.Label(new Rect((Screen.width / 2) + 50, (Screen.height / 3) + 10, 200, 50), ScoreKeeper.score.ToString(), golabel);

            if (GUI.Button(new Rect(Screen.width / 2 - 70, ((Screen.height / 3) * 2) - 40, 200, 50), "You have died. Retry?"))
            {
                print(ScoreKeeper.level.ToString());
                if (ScoreKeeper.level == 1)
                    SceneManager.LoadScene("Level One");
                else if (ScoreKeeper.level == 2)
                    SceneManager.LoadScene("Level Two");
                else if (ScoreKeeper.level == 3)
                    SceneManager.LoadScene("Level Three");
            }
            else if (GUI.Button(new Rect(Screen.width / 2 - 70, ((Screen.height / 3) * 2) + 20, 200, 50), "Return to Main Menu"))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        else if (ScoreKeeper.health <= 0 && ScoreKeeper.level == 100)
        {
            ScoreKeeper.Save();
            GUI.Label(new Rect(Screen.width / 2 - 80, (Screen.height / 3) - 40, 200, 50), "Game Over!", golabel);
            golabel.fontSize = 30;
            GUI.Label(new Rect(Screen.width / 2 - 70, (Screen.height / 3) + 10, 200, 50), "Scored: ", golabel);
            golabel.normal.textColor = Color.red;
            GUI.Label(new Rect((Screen.width / 2) + 50, (Screen.height / 3) + 10, 200, 50), ScoreKeeper.score.ToString(), golabel);

            if (GUI.Button(new Rect(Screen.width / 2 - 70, ((Screen.height / 3) * 2)-40, 200, 50), "Retry Infinite?"))
            {
                SceneManager.LoadScene("Infinite");
            }
            else if (GUI.Button(new Rect(Screen.width / 2 - 70, ((Screen.height / 3) * 2) + 20, 200, 50), "Return to Main Menu!"))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        /// PAUSE BUTTON ///
        if (ScoreKeeper.health > 0 && !completedLevel)
        {
            Texture2D image;
            GUI.backgroundColor = new Color(0, 0, 0, 0);
            if (!gamePaused)
            {
                image = Resources.Load("PauseButton") as Texture2D;
            }
            else
            {
                image = Resources.Load("PlayButton") as Texture2D;
            }
            if (GUI.Button(new Rect(Screen.width - 100, -10, 128, 128), image))
            {
                if (!gamePaused)
                {
                    Time.timeScale = 0;
                    gamePaused = true;
                    GUI.Button(new Rect(Screen.width * 0.5f, Screen.height * 0.5f, 100, 100), "Back to Main Menu");
                }
                else
                {
                    Time.timeScale = 1;
                    gamePaused = false;
                }
            }
        }
        #endregion
    }

    /*****************************************************************************/
    //                                   ONSTART
    /*****************************************************************************/
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>(); // Get the animator so we can manipulate the animations
        body = GetComponent<Rigidbody2D>();
        ScoreKeeper.health = 100;
    }

    /*****************************************************************************/
    //                                   UPDATE
    /*****************************************************************************/
    void Update()
    {
        //kills the player if not moving or falling for more than half a second. This might need to be lengthened
        //depending on level design
        float speedx = body.velocity.x;
        if (speedx <= 0.0f)
        {
            stopTimer -= Time.deltaTime;
            if (stopTimer < 0)
            {
                stopTimer = 0.5f;
                Dead();
            }
        }
        else
        {
            stopTimer = 0.5f;
        }

        //Running sounds 
        runSoundSpeed -= Time.deltaTime;                           //take the deltatime from the timer
        if (canJump && ScoreKeeper.health > 0 && !angryKnights)              //grounded and alive
        {
            if (runSoundSpeed <= 0)                                 //Waits for the timer to be under 0
            {
                float pitch = Random.Range(0.75f, 1.0f);            //Plays a different pitch every step
                run.pitch = pitch;                                  //Set the pitch
                run.PlayOneShot(runSound);
                runSoundSpeed = 0.2f;                               //Reset the timer for the next step
            }
        }


        //Dead();
        if (completedLevel == false)
        {
            if (ScoreKeeper.health > 0)
                MoveCamera();

            if (ScoreKeeper.health <= 0) //if player dies, load the game over screen
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("RiseToHeaven"))
                {
                    this.GetComponent<CircleCollider2D>().enabled = false; //so can go through roof
                    body.gravityScale = 0;
                    transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f);
                }

                return;
            }

            if (!angryKnights)
            {
                canJump = Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Floor"));

                if (Input.GetKeyDown(KeyCode.Space) && canJump || Input.GetMouseButtonDown(0) && canJump)
                {
                    jump = true;
                    hitledge = Physics2D.Linecast(transform.position, EdgeCheck.position, 1 << LayerMask.NameToLayer("Floor"));
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    didFlap = true;
                    Debug.Log("FLAPPING");
                    anim.SetTrigger("Flapping");

                    body.velocity = new Vector2(1.0f, jumpForce);
                    didFlap = false;
                }
            }    
        }
    }
    private void Dead()
    {
        ScoreKeeper.health = 0;
        body.velocity = Vector3.zero;
        anim.SetBool("Dead", true);
    }

    /*****************************************************************************/
    //                                   FIXED UPDATE
    /*****************************************************************************/
    // Update is called once per frame
    void FixedUpdate()
    {
        if (completedLevel == false)
        {
            if (ScoreKeeper.health <= 0)
                return;
            /*****************************************************************************/
            //                          RUNNER PLAYER CONTROLS
            /*****************************************************************************/

            #region RUNNER
            if (!angryKnights)
            {
                anim.SetBool("Flappy", false);
                anim.SetBool("Runner", true);
                /*****************************************************************************/
                //                    Runner Player Movement/Jumping Controls
                /*****************************************************************************/
                if (body.velocity.x <= speed)
                    body.AddForce(Vector2.right * moveForce);

                if (jump)
                {
                    body.AddForce(new Vector2(0, jumpForce * body.mass));
                    anim.SetTrigger("Jump");
                    Debug.Log("JUMP");
                    jump = false;
                }

                // Checking to make sure the speed is maintained
                if (body.velocity.x <= speed)
                    body.AddForce(Vector2.right * moveForce);

                if (Mathf.Abs(body.velocity.x) > speed)
                    body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed, body.velocity.y);

                if (hitledge)
                {
                    Dead();
                }

                /*****************************************************************************/
                //                          Some Player Animation Controlling
                //                                ADD NEW STATES HERE
                /*****************************************************************************/

                // Running Animation
                if (canJump)
                {
                    anim.SetBool("Jump", false);
                    anim.SetBool("Falling", false);
                    anim.SetBool("Grounded", true); // (jumps to the running state)
                    Debug.Log("Grounded");
                }

                // Jumping
                if (gameObject.GetComponent<Rigidbody2D>().velocity.y > -0.1f && !canJump) //climbing
                {
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-15.5f, 0));
                    anim.SetBool("Jump", true);
                    anim.SetBool("Grounded", false);
                    anim.SetBool("Falling", false);
                }

                // Falling
                if (gameObject.GetComponent<Rigidbody2D>().velocity.y <= -0.5f) //Checks the velocity of the rigidbody on player, if less than zero its safe to assume player is falling
                {
                    anim.SetBool("Falling", true);
                    anim.SetBool("Jump", false);
                    Debug.Log("Falling");
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-15.5f, -30)); //Adds a downward force when falling, gives the impress of gravity. Also adds a minus X, there should be no force in the air.
                }
            }
            #endregion

            /*****************************************************************************/
            //                          ANGRY KNIGHTS PLAYER CONTROLS
            /*****************************************************************************/
            #region ANGRY KNIGHTS
            else // ANGRY KNIGHTS
            {
                anim.SetBool("Runner", false);
                anim.SetBool("Flappy", true);

                if (body.velocity.x <= speed)
                {
                    body.AddForce(Vector2.right * moveForce);
                }

                if (Mathf.Abs(body.velocity.x) > speed)
                {
                    body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed, body.velocity.y);
                }

                // Controls
                if (dead) // player is dead
                {
                    Dead();
                }
                else // player is alive
                {
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    {
                        //body.velocity = Vector2.zero;
                        //body.AddForce(new Vector2(0, jumpForce * body.mass));
                        //body.velocity = new Vector2(0, jumpForce);
                        didFlap = true;
                        Debug.Log("FLAPPING");
                    }
                }

                anim.SetBool("Flappy", true);
                anim.SetBool("Runner", false);

            }
            #endregion
        }
    }

    /*****************************************************************************/
    //                               COLLISIONS
    /*****************************************************************************/
    void OnTriggerEnter2D(Collider2D col)
    {
        #region Collisions
        if (col.gameObject.CompareTag("Gem")) //collecting gems!
        {
            ScoreKeeper.gems++;
            col.gameObject.SetActive(false);

            coin.Play();
            ScoreKeeper.score += 100; //100 for a gem
        }
        else if (col.gameObject.CompareTag("Key"))
        {
            ScoreKeeper.keys++;
            col.gameObject.SetActive(false);
            ScoreKeeper.score += 1000;
            //key.Play();
        }
        else if (col.gameObject.CompareTag("Chest"))
        {
            //what happens if chest? i think more coins if they have all keys!
            if (ScoreKeeper.keys == 4) //or level max keys? maybe make the keys dynamic to each level?
            {
                //chest.Play();
                col.gameObject.SetActive(false);
                ScoreKeeper.gems += 100;
                ScoreKeeper.score += 10000;
            }
        }
        else if (col.gameObject.CompareTag("Spike")) //insta death
        {
            ScoreKeeper.health = 0;
            Dead();
        }
        else if (col.gameObject.CompareTag("EndObject"))
        {
            completedLevel = true;
            if (ScoreKeeper.level == 1)
            {
                if (ScoreKeeper.lvl1 < ScoreKeeper.level)
                    ScoreKeeper.lvl1 = ScoreKeeper.level;
            }
            else if (ScoreKeeper.level == 2)
            {
                if (ScoreKeeper.lvl2 < ScoreKeeper.level)
                    ScoreKeeper.lvl2 = ScoreKeeper.level;
            }
            else if (ScoreKeeper.level == 3)
            {
                if (ScoreKeeper.lvl3 < ScoreKeeper.level)
                    ScoreKeeper.lvl3 = ScoreKeeper.level;
            }
            ScoreKeeper.Save();
        }
        else if (col.gameObject.CompareTag("Changer"))
        {
            col.gameObject.SetActive(false);
            Debug.Log("CHANGED");
            angryKnights = !angryKnights;
            Debug.Log("Output: " + angryKnights.ToString());

            if (angryKnights == true)
            {
                anim.SetBool("Runner", false);
                anim.SetBool("Flappy", true);
                jumpForce = 20;
                Physics2D.gravity = new Vector3(-2, -7.81f, 0);
            }
            else
            {
                anim.SetBool("Runner", true);
                anim.SetBool("Flappy", false);
                jumpForce = 2000;
                Physics2D.gravity = new Vector3(-2, -9.81f, 0);
            }
        }
        #endregion

    }

    private void MoveCamera()
    {
        Transform targ = this.transform;
        //Vector3 target = this.transform.position + new Vector3(5.9f, 5.13f);
        Vector3 target = this.transform.position + new Vector3(5.9f, 0f);
        Vector3 velocity = Vector3.zero;
        float dampTime = 0.10f;

        if (targ)
        {
            Vector3 point = cam.WorldToViewportPoint(new Vector3(0.5f, 0.5f));
            Vector3 delta = target - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
            Vector3 dest = transform.position + delta;
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, dest, ref velocity, dampTime);
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -11);

            //Gets the pos of player and zooms out on height
            Vector3 playerPos = this.transform.position;        //Get player position, we only want the Y in this case
            int playerY = (int)System.Math.Round(playerPos.y); //Round the Y value
            playerY = (playerY >= 24) ? 24 : playerY;           //If the number is higher than 24, its set at 24
            float cameraZoomSmoothness = 1f;                    //Steps in which the camera moves 

            //Only moves the camera when the player is grounded. It seems to give motion sickness we leave this out 
            if (anim.GetBool("Grounded"))
            {
                switch (playerY) //Used switch statement instead of if/else because hash table. I know it looks messy but its fast
                {
                    case 0: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 7f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 1: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 8f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 2: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 9f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 3: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 9.5f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 4: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 9.75f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 5: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 10f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 6: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 10.5f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 7: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 11f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 8: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 11.5f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 9: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 12f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 10: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 12.5f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 11: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 13f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 12: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 13.5f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 13: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 14f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 14: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 14.5f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 15: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 15f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 16: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 16f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 17: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 16.5f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 18: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 17f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 19: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 17.5f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 20: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 18f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 21: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 18.5f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 22: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 19f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 23: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 19.5f, cameraZoomSmoothness * Time.deltaTime); break;
                    case 24: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 20f, cameraZoomSmoothness * Time.deltaTime); break;
                    default: cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 7f, 0.05f); break;
                }
            }
        }

    }
}