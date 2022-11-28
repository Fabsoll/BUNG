using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaterialPannelScript : MonoBehaviour 
{

    public Button buttonObject;
    public TMP_Text materialNameText;
    public TMP_Text priceText;

    private string materialName;
    private float price;
    private bool isPressed;
    private WallMaterial wallMaterial;

    

    

    public void SetMaterialUI(WallMaterial wMaterial)
    {
        wallMaterial = wMaterial;
        materialNameText.text = wMaterial.uiName;
        priceText.text = " - " + wMaterial.price.ToString() + " $";
        buttonObject.interactable = !isPressed;
    }

    public string MaterialName { get => materialName; set => materialName = value; }
    public float Price { get => price; set => price = value; }
    public bool IsPressed { get => isPressed; set => isPressed = value; }
    public WallMaterial WallMaterial { get => wallMaterial; set => wallMaterial = value; }

    public void ApplyMaterial()
    {
        GameObject.FindGameObjectWithTag("Decorator").GetComponent<Decorator>().ApplyMaterialInner(wallMaterial);
    }

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
}
