using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public MainManager Manager;

    private void Start()
    {
        Manager  = MainManager.Instance;
        if (MainManager.Instance.m_Menu == true)
        {
            gameObject.SetActive(false);
        }

        else
        {
            gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        Manager.GameOver();
    }
}
