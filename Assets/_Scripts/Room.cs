using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Room : MonoBehaviour
{
    public float temperature;
    public float humidity;
    public bool hasMainwindow;
    public Window mainWindow;

    public List<string> directions;

    public List<GameObject> windowPositions;
    public GameObject windowSpawner;
    public GameObject windowObject;

    public GameObject temperatureCube;
    public TMP_Text temperatureText;
    public Color temperatureColor;

    public bool isActive;

    //public Transform relevantPosition;

    // Start is called before the first frame update
    void Start()
    {
        hasMainwindow = false;
    }

    // Update is called once per frame
    void Update()
    {
        temperatureCube.gameObject.GetComponent<MeshRenderer>().material.color = temperatureColor;
        temperatureText.text = temperature.ToString();
    }

    public void ChoosePosition()
    {
        
    }

    public void SpawnWindow()
    {
        
    }

    public void EnableHeatVision(Color tempColor)
    {
        temperatureColor = tempColor;
        temperatureText.text = temperature.ToString();
        temperatureCube.gameObject.GetComponent<MeshRenderer>().material.color = temperatureColor;
        temperatureCube.SetActive(true);
        temperatureText.gameObject.SetActive(true);
    }

    public void DisableHeatVision()
    {
        temperatureCube.SetActive(false);
        temperatureText.gameObject.SetActive(false);
    }
}

