using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UfoController : MonoBehaviour
{
    //[HideInInspector] public Joystick joystick;

    private bool isEnemy = false;

    private Rigidbody rb;

    void Start()
    {
        if (gameObject.name.Contains("Enemy"))
            isEnemy = true;

        rb = GetComponent<Rigidbody>();
        //joystick = FindObjectOfType<Joystick>();
    }

    void Update()
    {
        transform.Rotate(Vector3.up, 300f * Time.deltaTime);

        if (isEnemy)
            return;

        rb.velocity = new Vector3(0f, 0f, 7f);

        //if (Mathf.Abs(joystick.Horizontal) < 0.25f)
        //return;

        //transform.position = new Vector3(transform.position.x + joystick.Horizontal * Time.deltaTime * 15f, transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3f, 3f), transform.position.y, transform.position.z);
    }

    public CinemachineVirtualCamera endCam;

    private List<Hip> tempConnectedCharacters = new List<Hip>();

    private bool finished = false;

    public void Finish()
    {
        if (finished)
            return;

        finished = true;
        transform.position = new Vector3(0f, transform.position.y, transform.position.z);

        Debug.Log("FINISH");
        isEnemy = true;
        endCam.m_Priority = 40;
        endCam.m_Follow = FindObjectOfType<EndCamTarget>().transform;
        rb.Sleep();
        rb.isKinematic = true;

        foreach (Hip hip in FindObjectsOfType<Hip>())
        {
            if (hip.attractPos.name.Contains("Player"))
                tempConnectedCharacters.Add(hip);
        }

        StartCoroutine(DropCharacters());
    }

    public Transform[] stepTransforms;

    int tempStepIndex = 0;

    IEnumerator DropCharacters()
    {
        while (tempConnectedCharacters.Count > tempStepIndex)
        {
            yield return new WaitForSeconds(0.3f);
            tempConnectedCharacters[tempStepIndex].StartMoveTowardEnemy(stepTransforms[tempStepIndex]);
            //tempConnectedCharacters.RemoveAt(tempConnectedCharacters.Count - 1);
            tempStepIndex++;
            FindObjectOfType<EndCamTarget>().transform.position = new Vector3(FindObjectOfType<EndCamTarget>().transform.position.x, stepTransforms[tempStepIndex].position.y + 6.5f, FindObjectOfType<EndCamTarget>().transform.position.z);
        }
    }
}
