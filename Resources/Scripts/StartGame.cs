using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

    void OnMouseDown()
    {
        
        PlayerPrefs.SetInt("lifeLeft", 3);
        Application.LoadLevel(1); // 0 = main menu
    }
}
