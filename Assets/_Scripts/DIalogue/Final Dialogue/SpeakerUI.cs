using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeakerUI : MonoBehaviour
{
    public Image portrait;
    public Text fullName;
    public Text dialog;
    public Mood mood;

    private Character speaker;
    public Character Speaker
    {
        get { return speaker; }
        set
        {
            speaker = value;
            portrait.sprite = speaker.portrait;
            fullName.text = speaker.fullName;
        }
    }

    public Mood Mood
    {
        set
        {
            Sprite sprite;
            if (value == Mood.Angry)
            {
                sprite = speaker.portraitAngry;
            }
            else if (value == Mood.Happy)
            {
                sprite = speaker.portraitHappy;
            }
            else
            {
                sprite = speaker.portrait;
            }
            portrait.sprite = sprite;
        }
    }

    public string Dialog
    {
        get { return dialog.text; }
        set { dialog.text = value; }
    }

    public bool HasSpeaker()
    {
        return speaker != null;
    }

    public bool SpeakerIs(Character character)
    {
        return speaker == character;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
