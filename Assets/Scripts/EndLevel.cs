using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public GameObject canvas;

    private bool end;

    private void Update()
    {
        if (end)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //load next level
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                //load main menu
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Level cleared");
            canvas.SetActive(true);
            other.gameObject.GetComponent<BallController>().enabled = false;
            end = true;
        }
    }
}
