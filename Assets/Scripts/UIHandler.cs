using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject NameInput;
    public GameObject StartButton;
    public GameObject GameOverButton;
    public Text ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        MainManager.Instance.GameOverText = GameOverButton;
        MainManager.Instance.ScoreText = ScoreText;
    }

    // Update is called once per frame
    void Update()
    {
        if (MainManager.Instance.m_Menu == false)
        {
            NameInput.SetActive(false);
            StartButton.SetActive(false);
        }
    }
}
