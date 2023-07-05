using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaszpunciSzopkodas : MonoBehaviour
{
    private float moveBy = 10f, tempMoveX = 0f;

    public void Move(bool isRight)
    {
        if (isRight)
            tempMoveX += moveBy;
        else
            tempMoveX -= moveBy;
    }

    void Update()
    {
        Vector3 targetPos = new Vector3(transform.position.x + tempMoveX, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 10f * Time.deltaTime);
    }
}
