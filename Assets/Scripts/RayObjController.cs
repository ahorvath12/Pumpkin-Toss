using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayObjController : MonoBehaviour
{
    public GameObject target, meter;
    

    // Start is called before the first frame update
    void OnEnable()
    {
        Debug.Log("On disable");
        transform.position = new Vector3(0, target.transform.position.y + 1.5f, target.transform.position.z);
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    private void OnDisable()
    {
        meter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {

            target.GetComponent<BallController>().launchDir = transform.position;
            target.GetComponent<BallController>().canLaunch = true;
            meter.SetActive(false);
        }

        //Vector3 localForward = transform.parent.InverseTransformDirection(transform.forward);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            meter.SetActive(true);
        }
        else
        {
            transform.RotateAround(target.transform.position, Vector3.right, 30 * Time.deltaTime);
        }
        
    }

    private void LateUpdate()
    {
        
    }


}
