using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterContol : MonoBehaviour 
{
    //-=-=--=-=-=-==-=-=-=-=-=-=-=-VARIABLES-=-=-=-=-=-=-=-=--=--=-=-=--=-=--=--=-=\\
    private Animator anim;
    public Rigidbody2D body;
    public float speed;
    public Transform GroundCheck;
    public Transform EdgeCheck;
    public float jumpForce;
	public float moveForce; 

    //Audio variables
    public AudioSource coin;
    public AudioSource key;
    public AudioSource chest;

    float canJump = 0.5f;

    //-=-=--=-=-=-==-=-=-=-=-=-=-=-METHODS-=-=-=-=-=-=-=-=--=--=-=-=--=-=--=--=-=\\
    public Texture Key;
    void OnGUI()
    {
        GUIStyle label = new GUIStyle();
        label.fontSize = 20;
        label.normal.textColor = Color.white;
        GUIStyle value = new GUIStyle();
        value.fontSize = 20;
        value.normal.textColor = Color.yellow;

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

    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        ScoreKeeper.health = 100;
    }

    void FixedUpdate()
    {
        bool canjump = Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Floor")); //for jump
        bool hitledge = Physics2D.Linecast(transform.position, EdgeCheck.position, 1 << LayerMask.NameToLayer("Floor")); //for death

		if(body.velocity.x <= speed)
			body.AddForce(Vector2.right * moveForce);

		if(Mathf.Abs(body.velocity.x) > speed)
			body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed, body.velocity.y);
		
		if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && canjump && canJump < Time.time)
        {
            print("click");
            body.AddForce(new Vector2(0, jumpForce * body.mass));          
            canJump = Time.time + 1.0f;
        }

        if (hitledge)
        {
            ScoreKeeper.health = 0;
        }

        //-=-=-=-=-=ANIMATIONS=-=-=--=-=\\
        if (canjump) //if on floor
        {
            anim.Play("Run");
        }
        else if (gameObject.GetComponent<Rigidbody2D>().velocity.y <= -0.1f) //if player going down
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, -30)); 
            this.anim.Play("JumpFall");
        }

        else if (gameObject.GetComponent<Rigidbody2D>().velocity.y > -0.1f && !canjump) //if player jumping
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-15.5f, 0));
            this.anim.Play("JumpUp");
        }
    }

    void Update()
    {     
        if (ScoreKeeper.health <= 0) //if player dies, load the game over screen
        {
            SceneManager.LoadScene("GameOver"); //load gameover
            ScoreKeeper.health = 100; 
        } 
    }

    void OnTriggerEnter2D(Collider2D col)
    {
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
        }
        else if (col.gameObject.CompareTag("EndObject"))
        {
            SceneManager.LoadScene("LevelSuccess"); //load the success level :)
        }
    }
}



