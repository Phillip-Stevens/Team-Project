using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuiPositioner : MonoBehaviour
{
    // Use this for initialization  
    public Texture Key;

    void OnGUI()
    {
        GUIStyle label = new GUIStyle();
        label.fontSize = 20;
        label.normal.textColor = Color.white;
        GUIStyle value = new GUIStyle();
        value.fontSize = 20;
        value.normal.textColor = Color.yellow;

        GUI.Label(new Rect(Screen.width * (0.85f / 6.3f), Screen.height * (0.1f / 6.3f), (4.8f / 6.55f), Screen.height * (0.85f / 6.3f)), "Gems: ", label);
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
}
