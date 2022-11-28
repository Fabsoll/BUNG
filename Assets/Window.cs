using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    
    public bool instantiated;
    public bool hasType;
    public bool hasMaterial;
    public Transform parentPosition;
    private bool isTurned;

    void Start()
    {
        isTurned = false;
        this.transform.SetParent(parentPosition);
        if (Mathf.Abs(transform.localPosition.x) < Mathf.Abs(transform.localPosition.y))
        {
            transform.Rotate(0, 90, 0);
        }

        // Update is called once per frame
    }
}
