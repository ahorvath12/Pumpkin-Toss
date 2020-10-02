using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchMeter : MonoBehaviour
{
    public GameObject ball, rayObj, target, meterBar;
    public Image bar;
    public PlayerSettingsSO playerSettingsVals;

    private Camera cam;
    public float barChangeSpeed;
    public int maxPower = 1000, minPower = 100;

    private float currentPower;

    private void Start()
    {
        cam = Camera.main;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        barChangeSpeed = playerSettingsVals.launchMeterSpeed;
        currentPower = 0f;
        //transform.position = new Vector3(0, rayObj.transform.position.y + .67f, rayObj.transform.position.z);
        StartCoroutine(ChargePower());
    }

    private void OnDisable()
    {
        float powerVal = maxPower - minPower;
        powerVal *= currentPower;
        ball.GetComponent<BallController>().launchForce = (int) powerVal;
        Debug.Log(powerVal);
    }


    private void Update()
    {
        Vector3 offsetPos = target.transform.position;

        //Vector3 screenPos = cam.WorldToScreenPoint(target.transform.position);
        meterBar.transform.position = RectTransformUtility.WorldToScreenPoint(cam, target.transform.position);
    }

    private IEnumerator ChargePower()
    {
        while (true)
        {
            currentPower += barChangeSpeed;
            bar.fillAmount = currentPower;
            if (currentPower >= 1 || currentPower <= 0)
                barChangeSpeed *= -1;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
