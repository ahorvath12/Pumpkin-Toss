using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayObjController1 : MonoBehaviour
{
    public GameObject target, meter;
    public float rotateSpeed;
    public PlayerSettingsSO playerSettingsVals;

    private bool showBar;
    

    // Start is called before the first frame update
    void OnEnable()
    {
        rotateSpeed = playerSettingsVals.rotateBarSpeed;
        transform.position = new Vector3(0, target.transform.position.y + 1.2f, target.transform.position.z);
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    private void OnDisable()
    {
        meter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        rotateSpeed = playerSettingsVals.rotateBarSpeed;
        //if (Input.GetKeyUp(KeyCode.Space) && showBar)
        //{

        //    target.GetComponent<BallController>().launchDir = transform.position;
        //    target.GetComponent<BallController>().canLaunch = true;
        //    meter.SetActive(false);
        //    showBar = false;
        //}
        //else if (Input.GetKeyDown(KeyCode.Space) && !showBar)
        //{
        //    meter.SetActive(true);
        //    showBar = true;
        //    rotateSpeed = 0;
        //}
        //else
        //{
        transform.RotateAround(target.transform.position, Vector3.right, rotateSpeed * Time.deltaTime);
        //}
        
    }

    private void LateUpdate()
    {
        
    }


}
