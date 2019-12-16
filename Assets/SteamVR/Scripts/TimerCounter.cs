//Attach this script to a GameObject
//Create a Button (Create>UI>Button) and a Text GameObject (Create>UI>Text)
//Click on the GameObject and attach the Button and Text in the fields in the Inspector

//This script outputs the time since the last level load. It also allows you to load a new Scene by pressing the Button. When this new Scene loads, the time resets and updates.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerCounter : MonoBehaviour
{
    public Text counterText;
    public float seconds, minutes;

    void Start()
    {
        counterText = GetComponent<Text>() as Text;
    }

    void Update()
    {
        minutes = (int)(Time.time / 60f);
        seconds = (int)(Time.time % 60f);
        counterText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}