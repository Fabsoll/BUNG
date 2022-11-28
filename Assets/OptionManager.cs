using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public TMP_Text optionName;
    public TMP_Text optionPrice;
    public Button button;
    public bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitializeContent(string name, int price)
    {
        optionName.text = name;
        optionPrice.text = " - " + price.ToString() + " $";
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
}
