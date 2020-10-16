using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public GameObject canvas, particleSystem;
    public string nextLevel;

    private bool end, playSystem;

    private void Update()
    {
        if (end)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(nextLevel);
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
            particleSystem.GetComponent<ParticleSystem>().Emit(5);
            playSystem = true;
            end = true;
        }
    }
}
