using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Image arrow, circle;
    public Text instructions;
    public GameObject pointerParent, powerBar;
    public PlayerSettingsSO settings;
    public GameObject[] numbers;


    public float fillStep;

    private string selectedButton;

    private float currentVal;
    private float maxVal = 1f;
    private float lastTimeChecked, waitTime = .2f;
    private Coroutine regen;

    private int[] raySpeeds = new int[] { 10, 20, 30, 50, 70 };
    private float[] meterSpeeds = new float[] { .01f, .02f, 0.03f, 0.05f, 0.07f };

    private int index = 2, stage = 0;

    // Start is called before the first frame update
    void Start()
    {
        settings.launchMeterSpeed = meterSpeeds[2];
        settings.rotateBarSpeed = raySpeeds[2];
        pointerParent.transform.position = new Vector3(numbers[2].transform.position.x, pointerParent.transform.position.y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            fillStep = 0.005f;
            if (CheckTime())
            {
                Debug.Log("Swap");

                index++;
                if (index >= numbers.Length)
                {
                    index = 0;
                }
                pointerParent.transform.position = new Vector3(numbers[index].transform.position.x, pointerParent.transform.position.y, 0f);

                if (stage == 0)
                {
                    settings.rotateBarSpeed = raySpeeds[index];
                }
                else if (stage == 1)
                    settings.launchMeterSpeed = meterSpeeds[index];
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

    public void MoveCursor(GameObject point)
    {
        pointerParent.transform.position = new Vector3(point.transform.position.x, pointerParent.transform.position.y, 0f);
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
            // next stage
            stage++;
            currentVal = 0;
            circle.fillAmount = currentVal;
            if (stage == 1)
            {
                instructions.text = "Choose meter speed:";
                index = 2;
                pointerParent.transform.position = new Vector3(numbers[2].transform.position.x, pointerParent.transform.position.y, 0f);
                GameObject.Find("Ray Variant").GetComponent<RayObjController1>().enabled = false;
                powerBar.SetActive(true);

            }
            if (stage == 2)
                SceneManager.LoadScene("Farm");
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

    private void LoadScene(string name)
    {
        if (name == "Start")
            name = null;
        else if (name == "Exit")
            Application.Quit();
    }
}
