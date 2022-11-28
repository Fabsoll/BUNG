using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class ConversationChangeEvent : UnityEvent<Conversation> { }

public class ChoiceController : MonoBehaviour
{
    public Choice choice;
    public ConversationChangeEvent conversationChangeEvent;

    public static ChoiceController AddChoiceButton(Button choiceButtonTemplate, Choice choice, int index)
    {
        int buttonSpacing = -32;
        Button button = Instantiate(choiceButtonTemplate);

        button.transform.SetParent(choiceButtonTemplate.transform.parent);
        button.transform.localScale = Vector3.one;
        button.transform.localPosition = new Vector3(0, index * buttonSpacing, 0);
        button.name = "Choice " + (index + 1);
        button.gameObject.SetActive(true);

        ChoiceController choiceController = button.GetComponent<ChoiceController>();
        choiceController.choice = choice;
        return choiceController;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (conversationChangeEvent == null)
            conversationChangeEvent = new ConversationChangeEvent();

        GetComponent<Button>().GetComponentInChildren<Text>().text = choice.text;
    }

    public void MakeChoice()
    {
        conversationChangeEvent.Invoke(choice.conversation);
        GameManager.Instance.increasePoints(choice.points);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
