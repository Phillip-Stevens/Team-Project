using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ScoreKeeper : MonoBehaviour {
    //Keep all variables

    public static int score; //so we can access it from other classes 
    public static int gems;
    public static int health;
    public static int keys;
    public static int level;
    public static int numLevels;

    public static int levelscomplete = 0;

    //level scores
    public static float lvl1 = 0;
    public static float lvl2 = 0;
    public static float lvl3 = 0;
    public static float inf = 0;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this); //never unloads the empty game object, simple yet effective!
        score = 0; //start score at 0
        gems = 0;
        health = 100;
        level = 1;
        numLevels = 3; //so we don't go over...
        Load();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void Save()
    {
        PlayerPrefs.SetInt("levelscomplete", levelscomplete);
        PlayerPrefs.SetFloat("lvl1", lvl1);
        PlayerPrefs.SetFloat("lvl2", lvl2);
        PlayerPrefs.SetFloat("lvl3", lvl3);
    }

    public static void Load()
    {
        levelscomplete = PlayerPrefs.GetInt("levelscomplete");
        lvl1 = PlayerPrefs.GetFloat("lvl1");
        lvl2 = PlayerPrefs.GetFloat("lvl2");
        lvl3 = PlayerPrefs.GetFloat("lvl3");
    }
}
