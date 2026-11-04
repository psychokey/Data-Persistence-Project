using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextInputManager : MonoBehaviour
{
    private TMP_InputField thisInput;
    // Start is called before the first frame update
    void Start()
    {
        thisInput = gameObject.GetComponent<TMP_InputField>();
        if (thisInput != null)
        {
            thisInput.onDeselect.AddListener((texto) => MainManager.Instance.GetUserName());
            thisInput.onDeselect.AddListener((texto) => MainManager.Instance.LoadData());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
