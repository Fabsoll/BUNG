using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    private GameObject _mainCamera;

    public Transform orientationPoint;

    [Header("Camera Turning Settings")]
    [SerializeField] private float sensitivityHor;
    [SerializeField] private float sensitivityVert;
    [SerializeField] private float minimumVert;
    [SerializeField] private float maximumVert;
    private float _rotationX;

    [Header("Camera Zoom Settings")]
    public float maxZoomIn;
    public float maxZoomOut;
    public float zoomSpeed;
    private float ZoomMovement;
    private Vector3 topPoint;
    private Vector3 bottomPoint;

    //references
    private UIController ui;


    public bool isAbleToChooseRoom;

    // Start is called before the first frame update
    void Start()
    {
        //GameManager.Instance.currentRoom = GameManager.Instance.rooms[0];
        //GameManager.Instance.currentRoom.isActive = false;
        isAbleToChooseRoom = true;
        //EVENTS ON START
        EventManager.Instance.OnCPressed += SwitchCameraMode;
        EventManager.Instance.OnMousePressed += SelectRoom;

        _mainCamera = transform.GetChild(0).gameObject;
        ui = FindObjectOfType<UIController>();
    }



    private void SwitchCameraMode(object sender, System.EventArgs e)
    {
        if (_mainCamera.GetComponent<Camera>().orthographic == true)
            _mainCamera.GetComponent<Camera>().orthographic = false;
        else
            _mainCamera.GetComponent<Camera>().orthographic = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Target point position
        this.transform.position = orientationPoint.transform.position;
        
        // Camera Turning
        if (Input.GetMouseButton(1))
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        
        topPoint = transform.position + (_mainCamera.transform.position - transform.position).normalized * maxZoomOut;
        bottomPoint = transform.position + (_mainCamera.transform.position - transform.position).normalized * maxZoomIn;

        Vector3 directionToBot = (bottomPoint - _mainCamera.transform.position).normalized;
        Vector3 directionToTop = (topPoint - _mainCamera.transform.position).normalized;

        float distanceToTop = Vector3.Distance(_mainCamera.transform.position, topPoint);
        float distanceToBot = Vector3.Distance(_mainCamera.transform.position, bottomPoint);

        // Camera Zoom
        if (Mathf.Approximately(Vector3.Dot(directionToBot, directionToTop), -1))
        {
            ZoomMovement = Input.GetAxisRaw("Mouse ScrollWheel");
            _mainCamera.transform.localPosition += new Vector3(0, 0, ZoomMovement).normalized * zoomSpeed * Time.deltaTime;
        }
        else
        {
            if (distanceToBot < distanceToTop && Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                ZoomMovement = -1 * Mathf.Abs(Input.GetAxisRaw("Mouse ScrollWheel"));
                _mainCamera.transform.localPosition += new Vector3(0, 0, ZoomMovement).normalized * zoomSpeed * Time.deltaTime;
            }
            else if (distanceToBot > distanceToTop && Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                ZoomMovement = Mathf.Abs(Input.GetAxisRaw("Mouse ScrollWheel"));
                _mainCamera.transform.localPosition += new Vector3(0, 0, ZoomMovement).normalized * zoomSpeed * Time.deltaTime;
            }
        }

        /*if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject.tag == "Room")
                {
                    orientationPoint.position = hit.transform.GetComponent<BoxCollider>().bounds.center;
                    if (_mainCamera.GetComponent<Camera>().orthographic == false)
                        _mainCamera.GetComponent<Camera>().orthographic = true;
                }
            }
        }*/

    }

    private void SelectRoom(object sender, System.EventArgs e)
    {
        if (Input.GetMouseButtonDown(0) && isAbleToChooseRoom)
        { // if left button pressed...
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 5000f))
            {
                if (hit.transform.gameObject.tag == "Room" && GameManager.Instance.currentRoom == null)
                {
                    orientationPoint.position = hit.transform.GetComponent<BoxCollider>().bounds.center;
                    if (_mainCamera.GetComponent<Camera>().orthographic == false)
                        _mainCamera.GetComponent<Camera>().orthographic = true;
                    
                    Room room = hit.transform.gameObject.GetComponent<Room>();
                    if (GameManager.Instance.rooms.Contains(room))
                    {
                        GameManager.Instance.currentRoom = GameManager.Instance.rooms[GameManager.Instance.rooms.IndexOf(room)];
                    }
                    GameObject.FindObjectOfType<WindowManager>().haveToCheck = true;
                    //GameManager.Instance.currentRoom.isActive = true;
                }
            }
            else
            {
                //GameManager.Instance.currentRoom.isActive = false;
                GameManager.Instance.currentRoom = null;
            }

        }
    }
}
