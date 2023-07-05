using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfFirstIsEmpty : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            if (transform.GetChild(0).childCount == 0)
            {
                yield return new WaitForSeconds(0.5f);

                if (transform.GetChild(0).childCount == 0)
                {
                    for (int i = 1; i < transform.childCount - 1; i++)
                    {
                        if (transform.GetChild(i).childCount > 0)
                        {
                            transform.GetChild(i).GetChild(0).parent = transform.GetChild(0);
                            transform.GetChild(0).GetChild(0).localPosition = Vector3.zero;
                            break;
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
