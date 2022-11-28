using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WindowManager : MonoBehaviour
{

    public List<GameObject> windowOptions;

    public List<GameObject> spawnButtons;
    public List<GameObject> typeButtons;
    public List<GameObject> materialButtons;

    public int chosenPosition;
    public int chosenObject;
    public bool haveToCheck = false;
    // Start is called before the first frame update

    public int currentWindow;

    private void Awake()
    {
        currentWindow = 0;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentRoom != null)
        {
            if (GameManager.Instance.currentRoom.mainWindow == null)
            {
                resetWindows();
                //Debug.Log("start");
            }
            else if (GameManager.Instance.currentRoom.mainWindow.instantiated)
            {
                EnableWindow(1);
                //Debug.Log("1st change " + "the window for room " + GameManager.Instance.currentRoom.name + " is " + GameManager.Instance.currentRoom.mainWindow.instantiated + "!");
                if (GameManager.Instance.currentRoom.mainWindow.hasType)
                {
                EnableWindow(2);
                //Debug.Log("2nd change " + "the type for room " + GameManager.Instance.currentRoom.name + " is " + GameManager.Instance.currentRoom.mainWindow.hasType + "!");
                }
                if (GameManager.Instance.currentRoom.mainWindow.hasMaterial)
                {
                    resetWindows();
                }
        }
    }
        //chosenObject = chosenPosition;
        if (haveToCheck)
        {
            CheckRoomDirections();
            haveToCheck = false;
        }
        
    }

    public void EnableWindow(int index)
    {
        foreach (GameObject fiels in windowOptions)
        {
            fiels.SetActive(false);
        }
        windowOptions[index].SetActive(true);
    }

    private void CheckRoomDirections()
    {
        if (GameManager.Instance.currentRoom != null)
        {
            foreach (GameObject spawnButton in spawnButtons)
            {
                spawnButton.SetActive(false);
            }
            foreach (GameObject spawnButton in spawnButtons)
            {
                foreach (string type in GameManager.Instance.currentRoom.directions)
                {
                    if (spawnButton.GetComponent<SpawnWindowButton>().type == type)
                    {
                        spawnButton.SetActive(true);
                        continue;
                    }

                }
            }
        }
        resetWindows();
    }

   /* public void Check()
    {
        if (chosenPosition != chosenObject)
        {
            resetWindows();
        }
    }*/

    public void resetWindows()
    {
        //Debug.Log(windowOptions.Count);
        currentWindow = 0;
        foreach(var window in windowOptions)
        {
            window.SetActive(false);
        }
        windowOptions[currentWindow].SetActive(true);
    }

    /*public void EnableNext()
    {

        if (currentWindow + 1 < 3)
        {
            currentWindow++;
        }
        
        windowOptions[currentWindow].SetActive(true);
    }*/

    public void enableSpawnButtons()
    {
        foreach (GameObject spawnButton in spawnButtons)
        {
            spawnButton.SetActive(false);
            //chosenPosition = spawnButton.GetComponent<SpawnWindowButton>().type;
            //chosen = chosenPosition;
        }
        foreach (GameObject spawnButton in spawnButtons)
        {
            if (GameManager.Instance.currentRoom.directions.Contains(spawnButton.GetComponent<SpawnWindowButton>().type))
            {
                spawnButton.SetActive(true);
            }
        }
    }

    
}
