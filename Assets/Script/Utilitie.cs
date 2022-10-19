using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Utilitie : MonoBehaviour
{
    public UnityEvent OnTriggerButton3D;
    private bool TrigerEnter = false;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            TrigerEnter = true;
            OnTriggerButton3D?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            TrigerEnter = false;
            OnTriggerButton3D?.Invoke();
        }
    }
    public void Explote(GameObject a)
    {
        Instantiate(a, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject, 0.1f);
    }

    public void showText(GameObject a)
    {
        a.SetActive(TrigerEnter);
    }
}