using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    public static Keyboard Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PressKey(string keyName)
    {
        char character = keyName.ToUpper()[keyName.Length - 1];
        Debug.Log("PRESSES KEY IS:" + character);

        if (transform.Find(character.ToString()) != null)
            transform.Find(character.ToString()).GetComponent<Animation>().Play();
    }
}
