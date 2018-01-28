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

		goverment_ending = "Government Won";
		fired_ending = "The best citizen is the one who does her job best. The news of the day is about the ones who doesn’t do any work. It is understandable that everybody can have personal issues sometimes that affecting the work quality. Anyone may be having a difficult time. But while the hole country is having hard time you cannot be stuck on your personal problems. The humanity is waiting for your service. So the ones who fired, before blaming on your bosses, look back to your performance and be honest to yourself. ";
		nothing_ending = "Nobody is expecting from you to change the destiny. Of course you cannot save everybody but C’MON! You haven’t done anything at all!! No effect on the history.";
		rebel_ending = "The history is being rewritten today. After the years of dictatorship, our country is having a new beginning. The uprising of crowd against the former government  became successful. We had an interview with the lead of rebels. \n\tRebel Leader: “This wouldn’t be the result without the unknown hero working in front   of the switch board. If she transmitted our calls to government we’d be having our darkest days at the moment. Feel the love transmitting from us to you the hero!” \n";
		died_ending = "YOU ARE DEAD. The death toll is keep increasing dramatically. The other day, a new suspicious case happened in the phone transmission centre. One of the transmission resposables found death seated in front of the switch board. The police reported that the rebels were chasing the worker because he were transmitting their calls through government.However, according to the people close to rebels, they have no relation with this case. So, the reason of death is still unknown.";
        

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
