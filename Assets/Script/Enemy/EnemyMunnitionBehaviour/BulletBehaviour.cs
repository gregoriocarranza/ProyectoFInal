
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletBehaviour : MonoBehaviour
{

    [SerializeField] private MunnitionData munnitionData;
    public static event Action<int> Damage;

    private bool pause;

    private void OnEnable()
    {
        UIManager.Pause += OnPause;
    }

    private void OnDisable()
    {
        UIManager.Pause -= OnPause;
    }

    public void OnPause(bool pauses)
    {
        pause = pauses;
    }

    void Start()
    {
        Destroy(gameObject, munnitionData.DestroyTime);

    }

    // Update is called once per frame
    void Update()
    {
        if (!pause) Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 8)
        {
            if (other.gameObject.tag == "Player")
            {
                Damage?.Invoke(munnitionData.damage);
            }
            Destroy(gameObject);
            Instantiate(munnitionData.EfectoDeDestruccion, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
    private void Move()
    {
        transform.Translate(Vector3.forward * munnitionData.Speed * Time.deltaTime);
    }

}
