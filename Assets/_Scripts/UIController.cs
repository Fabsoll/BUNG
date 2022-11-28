using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("UI elements references")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject bottomBar;
    [SerializeField] private GameObject compas;


    // refrences
    private GameManager gameManager;
    //add property
    // [HideInInspector]

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager != null)
        {
            moneyText.text = GameManager.Instance.points.ToString();

        }

        if (gameManager.currentRoom == null)
        {
            HideInterface();
        }
        else
        {
            ShowInterface();
        }
    }

    public void HideInterface()
    {
        bottomBar.gameObject.SetActive(false);
        compas.gameObject.SetActive(false);
    }
    public void ShowInterface()
    {
        bottomBar.gameObject.SetActive(true);
        compas.gameObject.SetActive(true);
    }
}
