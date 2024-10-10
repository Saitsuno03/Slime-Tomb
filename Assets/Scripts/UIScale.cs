using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScale : MonoBehaviour
{
    private float scaleval = 1f;
    // Start is called before the first frame update
    void Start()
    {
        if (Screen.width > 1920)
        {
            scaleval = 2f;
        }
        this.transform.localScale = new Vector3(scaleval, scaleval, scaleval);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
