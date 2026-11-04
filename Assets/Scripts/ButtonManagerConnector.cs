using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManagerConnector : MonoBehaviour
{
    private Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton = gameObject.GetComponent<Button>();
        startButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartGame()
    {
        MainManager.Instance.StartGame();
    }
}

