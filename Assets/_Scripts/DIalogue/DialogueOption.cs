using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption
{
    public string option;
    public int points;


}

public class DialogueUnit
{
    DialogueOption currentOption;
}
