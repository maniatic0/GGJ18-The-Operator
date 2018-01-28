﻿

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallScript : MonoBehaviour
{

    public Text conversationText;
    private int call;
    private int playerScore;
    private string intro, converstaion;
    private int callingTo = 0;
    private int comingFrom;
    public Button answerButton;
    public int[] consequences = new int[3];

    public float letterPause; //lenght of pause
    string myText; //text string you want to write - TODO: should have getter and setter

    //for score:
    int listeningPlugholeID = 0; //for testing which listeningPlugWhole the listening plug is in
    int score = 0;

    public Plug listeningPlyg;

    public Plug plug1;
    public Plug plug2;

    bool activeCall;


    bool checkingIncoming = false;
    bool checkForConnection = false;
    bool checkForConnectionType = false;



    public void Start()
    {

        activeCall = false;
   
    }

    public void Update()
    {
        if (!activeCall)
        {
            answerButton.interactable = true;

        }

        else if (checkingIncoming) {  checkIncomingLine();  }
        else if (checkForConnection) { checkOutputLine(); }

        else if (checkForConnectionType)
        {
            checkConnectionType();
            checkForConnectionType = false;
            activeCall = false;
        }



    }

    private void checkOutputLine()
    {


        if (plug1.ConnectId == callingTo)
        {
            myText = converstaion;
            startTyping();
            checkForConnection = false;
            checkForConnectionType = true;
            conversationText.text = "";

        }
        else if (plug2.ConnectId == callingTo)
        {
            myText = converstaion;
            startTyping();
            checkForConnection = false;
            checkForConnectionType = true;
            conversationText.text = "";
        }
        

    }

    private void checkIncomingLine()
    {
     
            if (plug1.ConnectId == comingFrom)
            {
            myText = intro;
            startTyping();
            checkingIncoming = false;
            checkForConnection = true;
            myText = "";
            conversationText.text = "";

        }
            else if (plug2.ConnectId == comingFrom)
        {
            myText = intro;
            startTyping();
            checkingIncoming = false;
            checkForConnection = true;
            myText = "";
            conversationText.text = "";

        }
    
    }

    public void Button_Clicked()
    {


        activeCall = true;
        answerButton.interactable = false;

        myText = "";
        conversationText.text = "";

        call++;

        switch (call)
        {
            case 6:

                comingFrom = 6;
                callingTo = 9;
                intro = " Man: Can you put me through to my wife she is in - district 9?";
                converstaion = " Man: I will be home late, don’t wait up!\nWife: Don’t bother coming home at all!";
                consequences[0] = 0;
                consequences[1] = 1;
                consequences[2] = -1;
                break;

            case 5:

                comingFrom = 8;
                callingTo = 10;
                intro = " rebel: Can you put me through to district 10, fast!\n";
                converstaion = " rebel: Eagle here! We need to protect ourselves with fireworks. Go get it from David, he has the delivery ready!\nRebel 2: understood eagle!";
                consequences[0] = -4;
                consequences[1] = 4;
                consequences[2] = 2;
                break;

            case 4:

                comingFrom = 9;
                callingTo = 14;
                intro = " Government official: Put me on with the president! \n";
                converstaion = " Government official: Mr. President, I have very bad news sir. People are getting ready for a big march against you. What do you want us to do?\nDictator: Activate Plan “sipping saucer”. I want the army and the national guard on the street.I want them dispersed right away!Those traitors will not get close to us.Tell them to use as much force as required and to get the reporters away.";
                consequences[0] = 3;
                consequences[1] = 3;
                consequences[2] = -2;
                break;

            case 3:

                comingFrom = 15;
                callingTo = 6;
                intro = " Rebel: I wanna link to district 1, please. \n";
                converstaion = " Rebel: Hey everybody there? Listen. We will mark all the walls on king street and the main boulevard. I want the whole street full with “No more”, “Freedom”, “Democracy Now!”, “Murderers!” and “SOS” slogans!. We meet at midnight. Not a single step back!";
                consequences[0] = -4;
                consequences[1] = 4;
                consequences[2] = -3;
                break;

            case 2:

                comingFrom = 12;
                callingTo = 9;
                intro = " Mafia Henchman: Put me through to the 9th district - NOW! \n";
                converstaion = "Mafia henchman: boss, the target has been silenced\nBoss: good job, be ready for your next assignment\n";
                consequences[0] = 4;
                consequences[1] = 3;
                consequences[2] = -3;
                break;

            case 1:

                comingFrom = 17;
                callingTo = 5; 
                intro = " Mom: Hello, can you put me through to my son in the fifth district?\n";
                converstaion = " Mom: Hello son, this is mama. How’ve you been? Are you going to pick up your sister today?\nSon: Everything good here.Yes!I will!Stop breathing down my neck, mother *sigh * \n";
                consequences[0] = 0;
                consequences[1] = 0;
                consequences[2] = -2;
                break;


        }

        Debug.Log("calling from: " + comingFrom + ", to " + callingTo);
        checkingIncoming = true;

    }


    void startTyping()
    {

        StartCoroutine(StartTyping());
    }

    IEnumerator StartTyping()
    {

        foreach (char letter in myText.ToCharArray())
        {
            conversationText.text += letter;
            yield return new WaitForSeconds(letterPause);
        }

    }


 

    private void checkConnectionType()
    {


        listeningPlugholeID = listeningPlyg.ConnectId; //get connect ID


        if (listeningPlugholeID == 2)
        {
            Debug.Log("government listening");
            score += consequences[1]; //government
        }
        else if (listeningPlugholeID == 1)
        {
            Debug.Log("rebels listening");
            score += consequences[2]; //Rebel
        }
        else {
            Debug.Log("nothing listening");
            score += consequences[0]; //neutral
        }
    }

}

