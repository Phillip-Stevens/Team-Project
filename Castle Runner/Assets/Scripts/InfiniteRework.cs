using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfiniteRework : MonoBehaviour {

    public GameObject[] objects;
    public GameObject spike;
    public float StartX;
    public float StartY;
    List<GameObject> objList = new List<GameObject>();
    List<GameObject> traps = new List<GameObject>();
    GameObject prev;
    

	// Use this for initialization
	void Start ()
    {
        objList.Add((GameObject)Instantiate(objects[1], new Vector3(StartX, StartY), Quaternion.identity));

	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (objList.Count < 7)
        {
            GenPrefab();
        }

        if (objList[0].transform.position.x < transform.position.x - 80)
        {
            Destroy(objList[0]);
            objList.Remove(objList[0]);
        }

        if (traps.Count > 0)
        {
            if (traps[traps.Count - 1].transform.position.x < transform.position.x - 30)
            {
                Destroy(traps[traps.Count - 1]);
                traps.Remove(traps[traps.Count - 1]);
            }
        }
	}

    private void GenPrefab()
    {
        print("==================================================================");
        float size = objList[objList.Count - 1].GetComponent<BoxCollider2D>().size.x;
        Vector3 pos = objList[objList.Count - 1].transform.position;
        int rand = Random.Range(0, objects.Length-1);
        int trap = Random.Range(0, 10);

        if (objList[objList.Count - 1].name == "Straight(Clone)")
        {
            pos = new Vector3(pos.x + size, pos.y);

            traps.Add((GameObject)Instantiate(spike, pos + new Vector3(pos.x + trap / 3, pos.y + 3.0f), Quaternion.identity));
            print("Spike");
        }
        else
        {
            string[] splitted = objList[objList.Count - 1].name.Split('_');
            int height = int.Parse(splitted[splitted.Length - 2]);
            pos = new Vector3(pos.x + size, pos.y + (height * 1.44f));
        }

        objList.Add((GameObject)Instantiate(objects[rand], pos, Quaternion.identity));
    }
}

