 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Mood
{
    Neutral,
    Angry,
    Happy
}

[System.Serializable] 
public struct Line
{
    public Character character;

    [TextArea(2, 5)]
    public string text;
    public Mood mood;
}

[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    public Character speaker;
    public Question question;
    public Conversation nextConversation;
    public Line[] lines;
}
