using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{

    public GameObject Target;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void LateUpdate()
    {
        // transform.localRotation = Quaternion.Euler(Target.transform.position);
        transform.LookAt(Target.transform.position);
        // Quaternion newRotation = Quaternion.Euler(Target.transform.position);
        // transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 200f * Time.deltaTime);

    }
}
