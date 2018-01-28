

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
    private float callTime, waitTime = 0f;
    public float callTimeStart = 0f;
    public float waitTimeStart = 5f;
    public bool callActive = false;
    public Button answerButton;
    private AudioSource ringSound;
    public int[] consequences = new int[3];

    public GameObject light1, light2, light3;

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

    //END TEST
    public GameObject end_object;

    private bool firstCall = true;



    public void Start()
    {

        activeCall = false;
        callTime = callTimeStart;
        waitTime = waitTimeStart;
        ringSound = this.GetComponent<AudioSource>();

        if (firstCall)
        {
            myText = " Welcome to your first day! In front of you is the switch board, where you direct the calls from!";
            startTyping();
            conversationText.text = "";
            

        }
    }

    public void Update()
    {
        if (callActive)
            callTime -= 1 * Time.deltaTime;

        if (activeCall)
        {
            waitTime = waitTimeStart;
        }
        if (callTime <= 0 && callActive)
        {
            light1.GetComponent<MoveLightScript>().DisableLight();
            light2.GetComponent<MoveLightScript>().DisableLight();
            callActive = false;
            waitTime = waitTimeStart;
            clearTextBox();
        }
        if (!activeCall)
        {
            waitTime -= 1 * Time.deltaTime;
            //if (waitTime <= 0)
            //{
            //    callTime = callTimeStart;
            //}
            
            if (waitTime <= 0)
            {
                answerButton.interactable = true;
                //callTime = callTimeStart;

                if (!callActive)
                {
                    ActivateCall();
                }
            }
        }

        

        else if (checkingIncoming) { checkIncomingLine(); }
        else if (checkForConnection) { checkOutputLine(); }

        else if (checkForConnectionType)
        {
            checkConnectionType();
            checkForConnectionType = false;
            activeCall = false;
        }

        ListeningLight();
    }

    private void ListeningLight()
    {
        var input = listeningPlyg.ConnectId;
        if (input == 1 || input == 2)
        {
            light3.GetComponent<MoveLightScript>().MoveLight(input);
        }
        else
        {
            light3.GetComponent<MoveLightScript>().DisableLight();
        }
    }

    private void checkOutputLine()
    {


        ringSound.enabled = false;
        if (plug1.ConnectId == callingTo)
        {
            myText = converstaion;
            startTyping();
            checkForConnection = false;
            checkForConnectionType = true;
            conversationText.text = "";
            callActive = true;
            callTime = callTimeStart;
        }
        else if (plug2.ConnectId == callingTo)
        {
            myText = converstaion;
            startTyping();
            checkForConnection = false;
            checkForConnectionType = true;
            conversationText.text = "";
            callActive = true;
            callTime = callTimeStart;
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
            if (light2 != null)
                light2.GetComponent<MoveLightScript>().MoveLight(callingTo);

        }
        else if (plug2.ConnectId == comingFrom)
        {
            myText = intro;
            startTyping();
            checkingIncoming = false;
            checkForConnection = true;
            myText = "";
            conversationText.text = "";
            if (light2 != null)
                light2.GetComponent<MoveLightScript>().MoveLight(callingTo);
        }

    }

    public void ActivateCall()
    {
   

        activeCall = true;
        answerButton.interactable = false;

        myText = "";
        conversationText.text = "";

        call++;

        switch (call)
        {

            case 7:
                end_object.SendMessage("End", score);
                break;
            case 6:

                comingFrom = 6;
                callingTo = 9;
                intro = " MAN: Can you put me through to my wife she is in - district 9?";
                converstaion = " MAN: I will be home late, don’t wait up!\nLADY: Don’t bother coming home at all!";
                consequences[0] = 0;
                consequences[1] = 1;
                consequences[2] = -1;
                break;

            case 5:

                comingFrom = 8;
                callingTo = 10;
                intro = " REBEL: Can you put me through to district 10, fast!\n";
                converstaion = " REBEL: Eagle here! We need to protect ourselves with fireworks. Go get it from David, he has the delivery ready!\nANOTHER REBEL: understood eagle!";
                consequences[0] = -4;
                consequences[1] = 4;
                consequences[2] = 2;
                break;

            case 4:

                comingFrom = 9;
                callingTo = 14;
                intro = " GOVERNMENT OFFICIAL: Put me on with the president! \n";
                converstaion = " GOVERNMENT OFFICIAL: Mr. President, I have very bad news sir. People are getting ready for a big march against you. What do you want us to do?\nPRESIDENT: Activate Plan “sipping saucer”. I want the army and the national guard on the street. Tell them to use as much force as required and to get the reporters away.";
                consequences[0] = 3;
                consequences[1] = 3;
                consequences[2] = -2;
                break;

            case 3:

                comingFrom = 15;
                callingTo = 6;
                intro = " REBEL: I wanna link to district 1, please. \n";
                converstaion = " REBEL: Hey everybody there? Listen. We will mark all the walls on king street and the main boulevard. I want the whole street full with “No more”, “Freedom”, “Democracy Now!”, “Murderers!” and “SOS” slogans!. We meet at midnight. Not a single step back!";
                consequences[0] = -4;
                consequences[1] = 4;
                consequences[2] = -3;
                break;

            case 2:
                firstCall = false;
                comingFrom = 12;
                callingTo = 9;
                intro = " MAFIA HENCHMAN: Put me through to the 9th district - NOW! \n";
                converstaion = " MAFIA HENCHMAN: Boss, the target has been silenced\nBOSS: Good job, be ready for your next assignment\n";
                consequences[0] = 4;
                consequences[1] = 3;
                consequences[2] = -3;
                break;

            case 1:
                firstCall = true;
                comingFrom = 17;
                callingTo = 5;
                intro = " MOM: Hello, can you put me through to my son in the fifth district?\n \nGUIDE: What did you think about that call? Don’t you think the mom sounded suspicious? Should we let the rebels listening in on this call? Or the government? Or maybe you should just link the call directly ? Move The red plug to the plughole, with the 'dictator' or 'rebel' label on top of it, depending on who you want listening to your call. Lastly, direct the other yellow plug to the light up hole to direct the call to the son.";
                converstaion = " MOM: Hello son, this is mama. How’ve you been? Are you going to pick up your sister today?\nSON: Everything good here. Yes! I will! Stop breathing down my neck, mother *sigh * \n";
                consequences[0] = 0;
                consequences[1] = 0;
                consequences[2] = -2;
                break;


        }

        if (light1 != null)
            light1.GetComponent<MoveLightScript>().MoveLight(comingFrom);

        ringSound.enabled = true;
        //Debug.Log("calling from: " + comingFrom + ", to " + callingTo);

        if (firstCall)
        {
            myText = " A call is incoming.Take one of the yellow plugs and plug it into the hole under the ligthning bulb";
            startTyping();
            conversationText.text = "";

        } 

        checkingIncoming = true;

    }

    void clearTextBox()
    {
        myText = "";
        conversationText.text = "";

        if (firstCall)
        {
            myText = " Good job, you’ve directed your first call! But remember that your actions have consequences...";
            startTyping();
            conversationText.text = "";
            Debug.Log("welcome text is on");

        }


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
        else
        {
            Debug.Log("nothing listening");
            score += consequences[0]; //neutral
        }


    }

}


