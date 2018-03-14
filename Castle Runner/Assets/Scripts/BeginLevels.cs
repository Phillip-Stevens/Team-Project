using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BeginLevels : MonoBehaviour {
    int mode = 3; //0 for normal, 1 for inf
    public Texture runner;
    public GameObject plr;
    float ax = 19f;
    float bx = 1.9f;
    bool swiped = false;
    bool right = false;
    public Font font;
    bool adventure = false;
    int levelselected = 1;
    public AudioClip click;
    public AudioSource clicksound;

    // Use this for initialization        
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        //swipe mode change 
        //Right is for inf, left for normal
        //Currently just right arrow and left arrow lol
        if (adventure == false)
        {
            Swipe();
            if (swiped || SystemInfo.deviceType == DeviceType.Desktop)
            {
                if ((Input.GetKeyDown(KeyCode.RightArrow) || right) && (mode == 0 || mode == 3))
                {
                    clicksound.PlayOneShot(click);

                    mode = 1;
                    plr.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                    plr.GetComponent<Animator>().SetBool("run", true);

                }
                else if (((Input.GetKeyDown(KeyCode.LeftArrow)) || (right == false && swiped == true)) && (mode == 1 || mode == 3))
                {
                    clicksound.PlayOneShot(click);
                    mode = 0;
                    plr.transform.localScale = new Vector3(-1.3f, 1.3f, 1.3f);
                    plr.GetComponent<Animator>().SetBool("run", true);
                }

                if (mode == 0)
                {
                    if (plr.transform.position.x > bx)
                    {
                        plr.transform.position += new Vector3(Time.deltaTime * -8.0f, 0);
                    }
                    else
                    {
                        plr.GetComponent<Animator>().SetBool("run", false);
                        adventure = true;
                    }
                }
                else if (mode == 1)
                {
                    if (plr.transform.position.x < ax)
                    {
                        plr.transform.position += new Vector3(Time.deltaTime * 8.0f, 0);
                    }
                    else
                    {
                        plr.GetComponent<Animator>().SetBool("run", false);
                        BeginlevelOne();
                    }
                }
            }
        }
        else
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    clicksound.PlayOneShot(click);

                    if (levelselected < 3)
                        levelselected++;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    clicksound.PlayOneShot(click);

                    if (levelselected > 1)
                        levelselected--;
                }
            }
            else
            {
                clicksound.PlayOneShot(click);

                Swipe();
            }           
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.font = font;
        style.normal.textColor = Color.yellow;
        style.fontSize = 20;

        
        if (adventure == false)
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                GUI.DrawTexture(new Rect((Screen.width / 2) - 200, (Screen.height / 2) - 400, 400, 200), runner);
                GUI.Label(new Rect((Screen.width / 2) - 190, (Screen.height / 2) - 200, 300, 100), "Press Right Arrow for Infinite Mode",style);//, style);
                GUI.Label(new Rect((Screen.width / 2) - 190, (Screen.height / 2) - 140, 300, 100), "Press Left Arrow for Adventure Mode", style);//, style);
            }
            else //phone, (and console if we even port to that)
            {
                GUI.DrawTexture(new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 200, 200, 100), runner);
                GUI.Label(new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 100, 300, 100), "Swipe Right for Infinite Mode", style);//, style);
                GUI.Label(new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 70, 300, 100), "Swipe Left for Adventure Mode", style);//, style);
            }
        }
        else
        {
            Normal();
            style.font = Font.CreateDynamicFontFromOSFont("Aerial", 30);
            style.fontSize = 40;
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                GUI.DrawTexture(new Rect((Screen.width / 2) - 200, (Screen.height / 2) - 400, 400, 200), runner);
            }
            else
            {
                GUI.DrawTexture(new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 200, 200, 100), runner);
            }
            if (levelselected == 1)
            {
                style.normal.textColor = Color.green;
                GUI.Label(new Rect((Screen.width / 2) - 70, (Screen.height / 2) - 100, 300, 100), "EASY", style);//, style);
                if (GUI.Button(new Rect((Screen.width / 2) - 100, ((Screen.height / 3) * 2) - 40, 200, 50), "Level 1"))
                {
                    clicksound.PlayOneShot(click);

                    ScoreKeeper.level = 1;
                    SceneManager.LoadScene("Level One");
                }
            }
            else if (levelselected == 2)
            {
                if (ScoreKeeper.levelscomplete == 1)
                {
                    style.normal.textColor = Color.yellow;
                    GUI.Label(new Rect((Screen.width / 2) - 90, (Screen.height / 2) - 100, 300, 100), "MEDIUM", style);//, style);
                    if (GUI.Button(new Rect((Screen.width / 2) - 100, ((Screen.height / 3) * 2) - 40, 200, 50), "Level 2"))
                    {
                        clicksound.PlayOneShot(click);

                        ScoreKeeper.level = 2;
                        SceneManager.LoadScene("Level Two");
                    }
                }
                else
                {
                    style.normal.textColor = Color.yellow;
                    GUI.Label(new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 100, 300, 100), "Medium - Locked", style);//, style);
                    if (GUI.Button(new Rect((Screen.width / 2) - 100, ((Screen.height / 3) * 2) - 40, 200, 50), "Level Locked"))
                    {
                        clicksound.PlayOneShot(click);

                    }
                }
            }
            else if (levelselected == 3)
            {
                if (ScoreKeeper.levelscomplete == 2)
                {
                    style.normal.textColor = Color.red;
                    GUI.Label(new Rect((Screen.width / 2) - 130, (Screen.height / 2) - 100, 300, 100), "HARD - Locked", style);//, style);
                    if (GUI.Button(new Rect((Screen.width / 2) - 100, ((Screen.height / 3) * 2) - 40, 200, 50), "Level 3"))
                    {
                        clicksound.PlayOneShot(click);

                        ScoreKeeper.level = 3;
                        SceneManager.LoadScene("Level Three");
                    }
                }
                else
                {
                    style.normal.textColor = Color.red;
                    GUI.Label(new Rect((Screen.width / 2) - 90, (Screen.height / 2) - 100, 300, 100), "Hard - Locked", style);//, style);
                    if (GUI.Button(new Rect((Screen.width / 2) - 100, ((Screen.height / 3) * 2) - 40, 200, 50), "Level Locked"))
                    {
                        clicksound.PlayOneShot(click);

                    }
                }
            }
        }
    }

    public void BeginlevelOne()
    {
        if (mode == 0)
        {
            print("Norm");
            Normal();
        }
        else
        {
            InfRun();
        }
    }

    public void InfRun()
    {
        SceneManager.LoadScene("Infinite");
        ScoreKeeper.score = 0;
        ScoreKeeper.level = 100;
        ScoreKeeper.keys = 0;
        ScoreKeeper.health = 100;
        ScoreKeeper.gems = 0;
    }

    public void Normal()
    {
        ScoreKeeper.score = 0;
        ScoreKeeper.keys = 0;
        ScoreKeeper.health = 100;
        ScoreKeeper.gems = 0;
    }

    public void Next() //move level
    {
        if (ScoreKeeper.level != ScoreKeeper.numLevels)
        {
            ScoreKeeper.level++;
            SceneManager.LoadScene(ScoreKeeper.level);
            ScoreKeeper.score = 0;
            ScoreKeeper.keys = 0;
            ScoreKeeper.health = 100;
            ScoreKeeper.gems = 0;
        }
        else
            MainMenu();
    }

    public void MainMenu() //don't change level for this... then player wont have to redo all levels :D
    {
        SceneManager.LoadScene("MainMenu");
        ScoreKeeper.score = 0;
        ScoreKeeper.keys = 0;
        ScoreKeeper.health = 100;
        ScoreKeeper.gems = 0;
    }


    //Swipe with credit to: http://forum.unity3d.com/threads/swipe-in-all-directions-touch-and-mouse.165416/
    //inside class
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0 &&  currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    Debug.Log("up swipe");
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    Debug.Log("down swipe");
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    if (adventure == false)
                    {
                        swiped = true;
                        right = false;
                    }
                    else
                    {
                        if (levelselected < 3)
                            levelselected++;
                        
                    }
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    if (adventure == false)
                    {
                        swiped = true;
                        right = true;
                    }
                    else
                    {
                        if (levelselected > 1)
                            levelselected--;
                    }
                }
            }
        }
    }
}
