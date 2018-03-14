using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfinitePlatformer : MonoBehaviour
{
    public GameObject spike;

    public GameObject top;
    public GameObject outcorner;
    public GameObject incorner;
    public GameObject filler;
    public GameObject side;

    public GameObject[] decorations;

    public float startx, starty;

    List<GameObject> pieces = new List<GameObject>();
    List<GameObject> topbits = new List<GameObject>();
    List<GameObject> dec = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        Vector3 pos = new Vector3(startx, starty, 0);
        pieces.Add((GameObject)Instantiate(top, pos, Quaternion.identity));
        topbits.Add((GameObject)Instantiate(top, pos + (new Vector3(0.0f, 8.0f, 0.0f)), Quaternion.identity));
        topbits[0].transform.localScale = new Vector3(0.2f, -0.2f, 0.2f);
    }
	
	// Update is called once per frame

    //heres the messy shit................................

    void Update() //new version
    {
        int rand = Random.Range(0, 40);

        if (rand < 36)
        {
            if (pieces[pieces.Count - 1].transform.position.x < transform.position.x + 20)
            {
                //get position for next piece
                Vector3 pos = new Vector3(pieces[pieces.Count - 1].transform.position.x + 1.44f, pieces[pieces.Count - 1].transform.position.y);

                //create roof
                pieces.Add((GameObject)Instantiate(top, pos, Quaternion.identity));
                topbits.Add((GameObject)Instantiate(top, pos + (new Vector3(0, 8.0f)), Quaternion.identity));

                bool decs = dec.Count > 0 ? dec[dec.Count - 1].transform.position.x < pos.x - 9 : true; //if pieces, check if gap, else, just place as dec is empty (so pieces don't overlap) 

                if (rand < 13 && decs)
                {
                    dec.Add((GameObject)Instantiate(decorations[0], pos + (new Vector3(0, 6f)), Quaternion.identity)); //light
                }
                else if (rand < 26 && decs)
                {
                    dec.Add((GameObject)Instantiate(decorations[1], pos + (new Vector3(0, 4.5f)), Quaternion.identity)); //flag thing
                }

                if (rand % 2 == 0 && decs) //spawn gems on even numbers... maybe...? 
                {
                    int rande = Random.Range(1, 6);
                    if (rande == 4) //spawn 3 gems in a row :P
                    {
                        dec.Add((GameObject)Instantiate(decorations[2], pos + (new Vector3(0, 2f)), Quaternion.identity));
                        dec.Add((GameObject)Instantiate(decorations[2], pos + (new Vector3(2f, 2f)), Quaternion.identity));
                        dec.Add((GameObject)Instantiate(decorations[2], pos + (new Vector3(4f, 2f)), Quaternion.identity));
                    }
                    else
                        dec.Add((GameObject)Instantiate(decorations[2], pos + (new Vector3(0, 2f)), Quaternion.identity));
                }

                topbits[topbits.Count - 1].transform.localScale = new Vector3(0.2f, -0.2f, 0.2f); //flip it ovaaa
            }
        }
        else //uppy bit 
        {
            if (pieces[pieces.Count - 1].transform.position.x < transform.position.x + 20)
            {
                int gap = Random.Range(5, 10); //amount of roomy stuff
                Vector3 pos = pieces[pieces.Count - 1].transform.position; //position to place new thing

                for (int k = 1; k <= gap; k++)
                {
                    pieces.Add((GameObject)Instantiate(top, pos + (new Vector3(1.44f * k, 0)), Quaternion.identity)); //create new piece... for da gap yo
                }

                //corner bit
                pos = pieces[pieces.Count - 1].transform.position;
                pieces.Add((GameObject)Instantiate(incorner, pos + (new Vector3(1.44f, 0)), Quaternion.identity)); //create new piece... for da gap yo

                int height = Random.Range(1, 3);
                height--; //because 1 extra is added on top :P 
                pos = pieces[pieces.Count - 1].transform.position;

                for (int k = 1; k <= height; k++)
                {
                    pieces.Add((GameObject)Instantiate(side, pos + (new Vector3(0, 1.44f * k)), Quaternion.identity));
                }

                pos = pieces[pieces.Count - 1].transform.position;
                pieces.Add((GameObject)Instantiate(outcorner, pos + (new Vector3(0, 1.44f)), Quaternion.identity)); //create new piece... for da gap yo

                //now for the roof
                //corner
                pos = topbits[topbits.Count - 1].transform.position;

                topbits.Add((GameObject)Instantiate(outcorner, pos + (new Vector3(1.44f, 0)), Quaternion.identity));
                topbits[topbits.Count - 1].transform.localScale = new Vector3(-0.2f, -0.2f, 0.2f); //flip it ovaaa

                //side bits
                pos = topbits[topbits.Count - 1].transform.position;

                for (int k = 1; k <= height; k++)
                {
                    topbits.Add((GameObject)Instantiate(side, pos + (new Vector3(0, 1.44f * k)), Quaternion.identity));
                    topbits[topbits.Count - 1].transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f); //flip it ovaaa
                }

                //top corner bit
                pos = topbits[topbits.Count - 1].transform.position;
                topbits.Add((GameObject)Instantiate(incorner, pos + (new Vector3(0, 1.44f)), Quaternion.identity));
                topbits[topbits.Count - 1].transform.localScale = new Vector3(-0.2f, -0.2f, 0.2f); //flip it ovaaa
                pos = topbits[topbits.Count - 1].transform.position;

                //roof again
                for (int k = 1; k <= gap; k++)
                {
                    topbits.Add((GameObject)Instantiate(top, pos + (new Vector3(1.44f * k, 0)), Quaternion.identity)); //create new piece... for da gap yo
                    topbits[topbits.Count - 1].transform.localScale = new Vector3(0.2f, -0.2f, 0.2f); //flip it ovaaa
                }
                
            }
        }

        //-=-=-=-=-=-=-=--=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=SCORE INCREASER-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=--=-=-=-==-=-=-=--=-=-=-=-=-=-=-
        int score = Mathf.FloorToInt((transform.position.x - startx)/5);
        ScoreKeeper.score += score;
        //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-STUFF REMOVER-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        

        int count = pieces.Count - 1;
        for (int i = 0; i < count; i++)
        {
            GameObject obj = pieces[i];
            if (obj.transform.position.x < transform.position.x - 10)
            {
                Destroy(obj);
                pieces.Remove(obj);
            }
            else
                break;
        }

        count = topbits.Count - 1;
        for (int i = 0; i < count; i++)
        {
            GameObject obj = topbits[i];
            if (obj.transform.position.x < transform.position.x - 10)
            {
                Destroy(obj);
                topbits.Remove(obj);
            }
            else
                break;
        }

        count = dec.Count - 1;
        for (int i = 0; i < count; i++)
        {
            GameObject obj = dec[i];
            if (obj.transform.position.x < transform.position.x - 10)
            {
                Destroy(obj);
                dec.Remove(obj);
            }
            else
                break;
        }
    }
}
