using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRotate : MonoBehaviour {
    
    private float RotationsVel;
    public Vector3 targetAngle;
    private Vector3 currentAngle;
    
    void Update()
    {
        RotationsVel += Time.deltaTime;
        targetAngle = new Vector3(RotationsVel, 0, 0f);
        currentAngle = new Vector3(
        Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime),
        Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime),
        Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime));

        transform.eulerAngles = currentAngle;
    }
}
