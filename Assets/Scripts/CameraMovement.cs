using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform character, background;
    public Vector3 offset;

    // Start is called before the first frame update
    [Range(1,10)]
    public float smoothFactor;

    // Update is called once per frame
    void LateUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = character.position +offset;
        Vector3 smoothCam = Vector3.Lerp(targetPosition, transform.position, smoothFactor * Time.deltaTime);
        //transform.position = smoothCam - new Vector3(0, 0,0); // avoid camera follow player on the y axis
        smoothCam.y = -23.82f; // -23.82f is default y axis for camera
        transform.position = smoothCam;
        background.position = transform.position - offset;
    }
}
