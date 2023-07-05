using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAdministrator : MonoBehaviour {

    public bool ShowCursor = false;
    public bool InUpdate = false;

    void Start ()
    {
        if (!ShowCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
	
	void Update ()
    {
        if (!InUpdate)
            return;

        if(!ShowCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
