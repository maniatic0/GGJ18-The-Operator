using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restartScript : MonoBehaviour
{



    public void restartGame()
    {
        Debug.Log("RESTART GAME");
        Application.LoadLevel(0);
    }

}
