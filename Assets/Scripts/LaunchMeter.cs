using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchMeter : MonoBehaviour
{
    public GameObject rayObj;
    public Image bar;

    public float barChangeSpeed = 1;
    public int maxPower = 1000;
    public int minPower = 100;

    private float currentPower;
    

    private void OnEnable()
    {
        transform.position = new Vector3(0, rayObj.transform.position.y + .67f, rayObj.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ChargePower(1 * Time.deltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            //let go
        }
    }

    public void ChargePower(float amount)
    {
        if (currentPower - amount >= 0)
        {
            currentPower -= amount;

        }
    }
}
