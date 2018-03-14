using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndScore : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
	    GetComponent<Text>().text = "Score: " + ScoreKeeper.score.ToString();
	}
}
