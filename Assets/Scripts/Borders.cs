using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        if (MainManager.Instance.m_Menu == true)
        {
            gameObject.SetActive(false);
        }

        else
        {
            gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    public void LoadBorders()
    {
        if (MainManager.Instance.m_Menu == true)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
