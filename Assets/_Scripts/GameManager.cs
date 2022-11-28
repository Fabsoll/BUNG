using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public TabGroup tabGroup;

    private DialogueManager dManager;

    public int points;
    //public int paintPrice;

    public Room currentRoom;
    public List<Room> rooms;
    private bool temperatureIsDisplayed = false;

    public List<Color> temperatureColor = new List<Color>();

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game Manager is null");
            }
            return _instance;
        }
    }

    

    private void Awake()
    {
        dManager = FindObjectOfType<DialogueManager>();
        

        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        //rooms = GameObject.FindGameObjectsWithTag("Room");
    }

    // Update is called once per frame
    void Update()
    {
        if ( (SceneManager.GetActiveScene().buildIndex) == 1)
        {
            foreach (Room room in FindObjectsOfType<Room>())
            {
                if (!rooms.Contains(room))
                {
                    rooms.Add(room);
                    currentRoom = rooms[0];
                }
                else
                {
                    break;
                }
                
                //Debug.Log(room.gameObject.name);
            }

            
        }

        // ADD EVENT FOR THIS
        if (dManager == null)
        {
            return;
        }
        else
        {
            if (dManager.conversation.name == "End")
            {
                dManager = null;
                StartCoroutine(LoadingLevel());
            }
        }


    }

    public void ChangeTemperatureForAll(int temp)
    {
        foreach(Room room in rooms)
        {
            room.temperature += temp;
        }
    }
    public void ChangeTemperatureForRoom(int temp, int roomIndex)
    {
        rooms[roomIndex].temperature += temp;
    }

    public void DisplayTemperature()
    {
        if (temperatureIsDisplayed)
        {
            foreach (var room in rooms)
            {
                room.DisableHeatVision();
            }
            temperatureIsDisplayed = false;
        }
        else if (!temperatureIsDisplayed)
        {
            foreach (var room in rooms)
            {
                
                switch (room.temperature)
                {
                    case 18:
                        room.EnableHeatVision(temperatureColor[0]);
                        break;
                    case 19:
                        room.EnableHeatVision(temperatureColor[1]);
                        break;
                    case 20:
                        room.EnableHeatVision(temperatureColor[2]);
                        break;
                    case 21:
                        room.EnableHeatVision(temperatureColor[3]);
                        break;
                    case 22:
                        room.EnableHeatVision(temperatureColor[4]);
                        break;
                    case 23:
                        room.EnableHeatVision(temperatureColor[5]);
                        break;
                    case 24:
                        room.EnableHeatVision(temperatureColor[6]);
                        break;
                    case 25:
                        room.EnableHeatVision(temperatureColor[7]);
                        break;
                    case 26:
                        room.EnableHeatVision(temperatureColor[8]);
                        break;
                    case 27:
                        room.EnableHeatVision(temperatureColor[9]);
                        break;
                    case 28:
                        room.EnableHeatVision(temperatureColor[10]);
                        break;
                    case 29:
                        room.EnableHeatVision(temperatureColor[11]);
                        break;
                    case 30:
                        room.EnableHeatVision(temperatureColor[12]);
                        break;
                    case > 30:
                        room.EnableHeatVision(temperatureColor[12]);
                        break;
                    default:
                        room.EnableHeatVision(temperatureColor[0]);
                        break;
                }
            }
            temperatureIsDisplayed = true;
        }

        
    }


    public void increasePoints(int amount)
    {
        points += amount;
    }

    public void decreasePoints(int amount)
    {
        points -= amount;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPreviousLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private IEnumerator LoadingLevel()
    {
        yield return new WaitForSeconds(2f);
        LoadNextLevel();
    }

    /////////////// ROOM PART ///////////////////// 
    
}

[System.Serializable]
public class TemperatureValueColor
{
    public int temperature;
    public Color color;
}

