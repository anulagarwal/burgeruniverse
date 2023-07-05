using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmEn : MonoBehaviour
{
    private void Fragments()
    {
        FindObjectOfType<HandGirlManager>().FragmentsEnable();
    }
}
