using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSelect : MonoBehaviour
{

    public Image arrow, circle;
    public GameObject pointerParent;
    public GameObject[] buttons;

    public float fillStep;

    private string selectedButton;

    private float currentVal;
    private float maxVal = 1f;
    private float lastTimeChecked, waitTime = .2f;
    private Coroutine regen;

    private int sum;
    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        sum = 1;
        selectedButton = buttons[0].name;
        currentVal = 0;
        circle.fillAmount = currentVal;
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
                currentIndex += sum;
                selectedButton = buttons[currentIndex].name;
                sum *= -1;

                pointerParent.transform.position = new Vector3(pointerParent.transform.position.x, buttons[currentIndex].transform.position.y, 0f);
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
            Debug.Log("Load scene");
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
