using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorPanelScript : MonoBehaviour
{
    public Button buttonObject;
    public TMP_Text colorNameText;
    public TMP_Text priceText;

    private string colorName;
    private float price;
    public bool isPressed;
    private WallColor wallColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed == true)
        {
            buttonObject.interactable = false;
        }
        else if (isPressed == false)
        {
            buttonObject.interactable = true;
        }
    }

    public void SetColorUI(WallColor wColor)
    {
        wallColor = wColor;
        colorNameText.text = wColor.uiName;
        priceText.text = " - " + wColor.price.ToString() + " $";
        buttonObject.interactable = !isPressed;

    }

    public void ApplyColor()
    {
        GameObject.FindGameObjectWithTag("Decorator").GetComponent<Decorator>().PaintInner(wallColor);
    }
}
