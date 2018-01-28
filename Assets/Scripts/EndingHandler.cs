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

    
	public string goverment_ending;  //between +9 to +15
	public string fired_ending; //between +3 to +9 
	public string nothing_ending; //between -3 to +3 
    public string rebel_ending; //between -9 to -3
	public string died_ending; //between -15 to -9

	public string the_end = "\nEND";
    public string and_string = "\nAND\n";

    public GameObject gameCanvas;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
		endingText.gameObject.SetActive(false);
		ending_background.gameObject.SetActive(false);
		resetButton.SetActive(false);

		goverment_ending = "The President proved his power again! Our legendary army defeated all the rebel forces. These traitors couldn’t managed to kill our glorious regime. Long live the president! ";
		fired_ending = "The best citizen is the one who does her job best. It is understandable that everyone has personal issues, that may affect the quality of ones work. But while the country is having a hard time, you cannot be stuck on your personal problems. The humanity is waiting for your service. You got fired! Look back to your performance and be honest to yourself. ";
		nothing_ending = "Nobody is expecting from you to change destiny. Of course you cannot save everybody but C’MON! You haven’t done anything at all!! No impact on the history.";
		rebel_ending = "The history has been rewritten today. After years of dictatorship, our country is experiencing a new beginning. The uprising of the rebel forces against the former government was successful. We had an interview with the leader of the rebels forces. \n\tRebel Leader: “This wouldn’t be the result without the unknown hero working in front of the switch board. If our calls where transfered to the government, we’d be having the darkest days. Feel the love transmitting from us to you - the hero!” \n";
		died_ending = "YOU ARE DEAD. The death toll is increasing dramatically. The other day, a new suspicious case happened at the call centre. One of the switchboard operators was found death in front of the switchboard. The police reported that the rebels were chasing the worker because calls where transmitted to the government. However, according to the people close to rebels, they have no relation with this case. So, the motive of the death is still unknown.";
        

    }

    public void End(int points)
    {
        gameCanvas.SetActive(false);

        string ending_string = " ";
			
		if (points >= -15 && points < -9 )
        {
            ending_string += died_ending;
        }
		else if (points >= -9 && points < -3)
        {
            ending_string += rebel_ending;
        }
		else if (points >= -3 && points < 3)
		{
			ending_string += nothing_ending;
		}
		else if (points >= 3 && points < 9)
		{
			ending_string += fired_ending;
		}
		else if (points >= 9 && points <= 15)
		{
			ending_string += goverment_ending;
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
