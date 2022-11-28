using System;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private static EventManager _instance;

    //references
    //private UIController

    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Event Manager is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        
    }

    public event EventHandler OnCPressed;
    public event EventHandler OnTPressed;

    public event EventHandler OnMousePressed;
    

    private GameObject clickedObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnCPressed?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetMouseButtonDown(0))
        {

            OnMousePressed?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            OnTPressed?.Invoke(this, EventArgs.Empty);
        }

    }
}
