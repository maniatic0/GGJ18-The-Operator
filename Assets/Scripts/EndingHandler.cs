using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class EndingHandler : MonoBehaviour
{
    AudioSource sound;
    public GameObject resetButton;
    public UnityEngine.UI.Text endingText;
    public float letterPause; //lenght of pause

    public UnityEngine.UI.Image ending_background;
    public float fade_in_time; //lenght of pause

    public int goverment_bound = 10;
    public int rebel_bound = -5;

    public string goverment_ending = "THE GOVERMENT WON";
    public string fired_ending = "YOU ARE FIRED";
    public string nothing_ending = "YOU LIVED WITHOUT DOING ANYTHING";
    public string rebel_ending = "THE REBELS WON";
    public string the_end = "\nEND";
    public string and_string = "\nAND\n";

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        endingText.gameObject.SetActive(false);
        ending_background.gameObject.SetActive(false);
        resetButton.SetActive(false);
        //End(16); // Testing
    }

    public void End(int points, int special_ending = -1)
    {
        string ending_string;

        switch (special_ending)
        {
            case 0:
                ending_string = nothing_ending;
                break;
            case 1:
                ending_string = fired_ending;
                break;
            default:
                ending_string = "";
                break;
        }
        if (points >= goverment_bound)
        {
            ending_string += (ending_string.Length > 0 ? and_string : "") + goverment_ending;
        }
        else if (points <= rebel_bound)
        {
            ending_string += (ending_string.Length > 0 ? and_string : "") + rebel_ending;
        }

        ending_string += the_end;

        StartCoroutine(FadeBackGround(ending_string));
    }


    IEnumerator FadeBackGround(string ending)
    {
        ending_background.gameObject.SetActive(true);
        float start_time = Time.time;
        while (Time.time - start_time < fade_in_time)
        {
            ending_background.canvasRenderer.SetAlpha(Mathf.Lerp(0.0f, 1.0f, (Time.time - start_time) / fade_in_time));
            yield return new WaitForEndOfFrame();
        }
        yield return StartTyping(ending);
    }


    IEnumerator StartTyping(string myText)
    {
        endingText.gameObject.SetActive(true);
        endingText.text = "";
        foreach (char letter in myText.ToCharArray())
        {
            sound.Play();
            endingText.text += letter;
            yield return new WaitForSeconds(letterPause);
        }
        resetButton.SetActive(true);
    }
}
