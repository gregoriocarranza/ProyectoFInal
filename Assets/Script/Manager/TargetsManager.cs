using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _1P_target;
    [SerializeField]
    private GameObject _3P_target;
    [SerializeField]
    private float CameraAxisX;
    [SerializeField]
    [Range(0f, 100f)]
    public float sensibilidad = 5f;
    private void Start()
    {

    }
    void LateUpdate()
    {
        CameraAxisX = Input.GetAxis("Mouse Y");
        _1P_target.transform.position += new Vector3(0, CameraAxisX * sensibilidad, 0);
        // startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngel, clampAngel);
        // state.RawOrientation = Quaternion.Euler(startingRotation.y, 0, 0);

    }
}


