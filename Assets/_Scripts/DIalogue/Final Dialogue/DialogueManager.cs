using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using System.Collections;

[System.Serializable]
public class QuestionEvent : UnityEvent<Question> { }


public class DialogueManager : MonoBehaviour
{
    public Conversation conversation;
    public GameObject speaker;
    private SpeakerUI speakerUI;

    public QuestionEvent questionEvent;

    private int activeLineIndex = 0;
    private bool conversationStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        speakerUI = speaker.GetComponent<SpeakerUI>();

        speakerUI.Speaker = conversation.speaker;
    }

    public void ChangeConversation(Conversation nextConversation)
    {
        conversationStarted = false;
        conversation = nextConversation;
        AdvanceLine();
    }
    private void AdvanceLine()
    {
        if (conversation == null)
        {
            return;
        }
        if (!conversationStarted)
        {
            Initialize();
        }
        if (activeLineIndex < conversation.lines.Length)
        {
            DisplayLine();
        }
        else
        {
            AdvanceConversation();
        }
    }

    private void Initialize()
    {
        conversationStarted = true;
        activeLineIndex = 0;
        speakerUI.Speaker = conversation.speaker;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceLine();
        }
    }

    private void AdvanceConversation()
    {

        if (conversation.question != null) {
            questionEvent.Invoke(conversation.question);
        }
        else if(conversation.nextConversation != null)
        {
            ChangeConversation(conversation.nextConversation);
        }
        else
        {
            EndConversation();
        }
    }

    private void EndConversation()
    {
        conversation = null;
        conversationStarted = false;
        speakerUI.Hide();
    }

    private void DisplayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        SetDialog(speakerUI, line.text);
        speakerUI.Mood = line.mood;
        activeLineIndex += 1;
    }

    private void SetDialog(SpeakerUI activeSpeaker, string text)
    {
        activeSpeaker.Dialog = text;
        activeSpeaker.Show();
    }
}
