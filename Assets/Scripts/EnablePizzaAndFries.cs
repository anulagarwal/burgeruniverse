using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePizzaAndFries : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.DeleteAll();
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("Fries", 1);
        PlayerPrefs.SetInt("Pizza", 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
