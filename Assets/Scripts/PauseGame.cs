using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public GameObject ball, ray, meter;
    public GameObject panel, pointerParent;
    public Image arrow, circle;
    public GameObject[] options;
    public float fillStep;

    public string selectedOption;
    private bool paused = false, navigate;

    private int index = 0;
    private float currentVal;
    private float maxVal = 1f;
    private float lastTimeChecked, waitTime = .2f;
    private Coroutine regen;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        selectedOption = options[0].name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                ball.GetComponent<BallController>().enabled = false;
                ball.GetComponent<Rigidbody>().isKinematic = true;
                ray.GetComponent<RayObjController>().enabled = false ;
                meter.SetActive(false);

                index = 0;
                selectedOption = options[0].name;
                panel.SetActive(true);
                currentVal = 0;
                circle.fillAmount = currentVal;
                //Time.timeScale = 0;
                navigate = true;
            }
            else
            {
                Cursor.visible = false;
                ball.GetComponent<BallController>().enabled = true;
                ball.GetComponent<Rigidbody>().isKinematic = false;
                ray.GetComponent<RayObjController>().enabled = true;
                meter.SetActive(true);

                panel.SetActive(false);
                Time.timeScale = 1;
                navigate = false;
            }
            paused = !paused;

            
        }

        if (navigate)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                fillStep = 0.005f;
                if (CheckTime())
                {
                    Debug.Log("Swap");
                    index++;
                    if (index >= options.Length)
                    {
                        index = 0;
                    }


                    pointerParent.transform.position = new Vector3(pointerParent.transform.position.x, options[index].transform.position.y, 0f);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                lastTimeChecked = Time.time;
            }
            else if (Input.GetKey(KeyCode.Space) && !CheckTime())
            {
                fillStep = 0f;
                arrow.enabled = false;
                LoadCircle(1 * Time.deltaTime);
            }
        }
    }

    private void LoadCircle(float amount)
    {
        if (currentVal + amount < maxVal)
        {
            currentVal += amount;
            circle.fillAmount = currentVal;

            if (regen != null)
            {
                StopCoroutine(regen);
            }

            regen = StartCoroutine(EmptyCircle());
        }
        else
        {
            //load scene
            LoadOption(options[index].name);
        }
    }

    private IEnumerator EmptyCircle()
    {
        while (currentVal > 0)
        {
            currentVal -= fillStep;
            circle.fillAmount = currentVal;
            yield return new WaitForSeconds(0.01f);
        }
        regen = null;
        arrow.enabled = true;
    }

    private bool CheckTime()
    {
        return Time.time - lastTimeChecked < waitTime;
    }

    private void LoadOption(string name)
    {
        if (name == "Resume")
        {
            index = 0;
            ball.GetComponent<BallController>().enabled = true;
            ball.GetComponent<Rigidbody>().isKinematic = false;
            ray.GetComponent<RayObjController>().enabled = true;
            meter.SetActive(true);
            panel.SetActive(false);
            Cursor.visible = false;
        }
        else if (name == "Settings")
        {
            //do something
        }
        else if (name == "Quit")
            Application.Quit();
    }

}
