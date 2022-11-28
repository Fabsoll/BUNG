using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWindowButton : MonoBehaviour
{
    public string type;
    public Transform spawnPosition;
    public GameObject windowPrefab;
    private GameObject windObj;

    private Transform parent;
    private int position;
    public int temperatureChange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameManager.Instance.currentRoom != null && GameManager.Instance.currentRoom.directions.Contains(type))
        //{
            /*int index = GameManager.Instance.currentRoom.directions.IndexOf(type);
            Debug.Log(index);*/

            //spawnPosition = GameManager.Instance.currentRoom.windowPositions[index].transform;
            //parent = spawnPosition.GetComponentInParent<Transform>();

        //}

        //Debug.Log(FindWindowPosition("North").position + " => " + FindWindowPosition("North").gameObject.name);
    }

    public void SpawnWindow()
    {
        //Debug.Log(GameManager.Instance.currentRoom.hasMainwindow);

        if (GameManager.Instance.currentRoom != null && GameManager.Instance.currentRoom.directions.Contains(type))
        {
            GameManager.Instance.currentRoom.hasMainwindow = true;
            int index = GameManager.Instance.currentRoom.directions.IndexOf(type);

            spawnPosition = GameManager.Instance.currentRoom.windowPositions[index].transform;
        }
        

        
        //GameObject c= GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (GameManager.Instance.currentRoom.mainWindow == null)
        {
            //Debug.Log("1g");
            windObj = Instantiate(windowPrefab);

            windObj.transform.position = spawnPosition.position;
            windObj.GetComponent<Window>().parentPosition = spawnPosition.parent;
            GameManager.Instance.currentRoom.mainWindow = windObj.GetComponent<Window>();
            GameManager.Instance.currentRoom.mainWindow.instantiated = true;
            GameManager.Instance.currentRoom.hasMainwindow = true;
            GameManager.Instance.currentRoom.windowObject = windObj;
        }
        else if (GameManager.Instance.currentRoom.mainWindow != null)
        {
            Destroy(GameManager.Instance.currentRoom.windowObject);
            GameManager.Instance.currentRoom.mainWindow = null;
            
        }
        GameManager.Instance.points -= 100;
        GameManager.Instance.ChangeTemperatureForRoom(temperatureChange ,GameManager.Instance.rooms.IndexOf(GameManager.Instance.currentRoom));
    }

    public void ApplyType()
    {
        GameManager.Instance.currentRoom.mainWindow.hasType = true;
        GameManager.Instance.currentRoom.windowObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        GameManager.Instance.points -= 50;
    }
    public void ApplyMaterial()
    {
        GameManager.Instance.currentRoom.mainWindow.hasMaterial = true;
        GameManager.Instance.currentRoom.windowObject.GetComponent<MeshRenderer>().material.color = Color.green;
        GameManager.Instance.points -= 80;
    }

    public Transform FindWindowPosition(string name)
    {
        foreach(string s in GameManager.Instance.currentRoom.directions)
        {
            if (s == type && name == type)
            {
                return GameManager.Instance.currentRoom.windowPositions[GameManager.Instance.currentRoom.directions.IndexOf(type)].transform;
            }
        }
        return null;
    }


    
}
