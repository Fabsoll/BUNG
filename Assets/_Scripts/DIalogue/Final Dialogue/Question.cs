using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Choice
{
    [TextArea(2, 5)]
    public string text;
    public Conversation conversation;
    public int points;
}

[CreateAssetMenu(fileName = "New Question", menuName = "Question")]
public class Question : ScriptableObject
{

    [TextArea(2, 5)]
    public string text;
    public Choice[] choices;
}
