using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startScript : MonoBehaviour {

	public void startGame()
    {
        Debug.Log("START GAME");
       // SceneManager.
        Application.LoadLevel(1);
    }
}
