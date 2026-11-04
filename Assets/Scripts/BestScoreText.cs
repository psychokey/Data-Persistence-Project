using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MainManager.Instance.BestScore = gameObject.GetComponent<Text>();
        MainManager.Instance.LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
