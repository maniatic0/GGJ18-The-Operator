using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class autoText : MonoBehaviour {

    public float letterPause; //lenght of pause

    string myText = "Your country is a mess!" + "\n" + "People are not only turning against the dictator of your country" + "\n" + "but also against each other. The country is divided in two!" + "\n" + "Pro-governmentalist and rebels, killing each other day by day." + "\n" + "These groups are communicating through the most modern way possible," + "\n" + "the wired telephone!" + "\n" + "\n" + "The government need you to operate these phones and wiretap potential rebels," + "\n" + "in order to keep the government in control of the society." + "\n"  + "\n" + "Make a choice!" + "\n" + "Will you help maintain the government's control over society" + "\n" + "or will you help the rebels rise against the evil dictatorship?";

    public GameObject startButton;
    public Text introductionText;
    AudioSource sound;

    void Start () {

        startButton.SetActive(false);
        sound = GetComponent<AudioSource>();
        
        StartCoroutine(StartTyping());

    }


    IEnumerator StartTyping()
    {

        foreach (char letter in myText.ToCharArray())
        {
            sound.Play();
            introductionText.text += letter;
            yield return new WaitForSeconds(letterPause);
        }
        startButton.SetActive(true);
    }
}
