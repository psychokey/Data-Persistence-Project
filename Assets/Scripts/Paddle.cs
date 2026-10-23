using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float Speed = 2.0f;
    public float MaxMovement = 2.0f;
    
    // Start is called before the first frame update
    private void Start()
    {
        if(MainManager.Instance.m_Menu)
        {
            gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (MainManager.Instance.m_Menu == false)
        {
            gameObject.SetActive(true);
        }

        float input = Input.GetAxis("Horizontal");
        
        Vector3 pos = transform.position;
        pos.x += input * Speed * Time.deltaTime;
        
        if (pos.x > MaxMovement)
            pos.x = MaxMovement;
        else if (pos.x < -MaxMovement)
            pos.x = -MaxMovement;

        transform.position = pos;
    }
}
