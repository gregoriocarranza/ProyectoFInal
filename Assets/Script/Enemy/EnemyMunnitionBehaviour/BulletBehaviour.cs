
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletBehaviour : MonoBehaviour
{

    [SerializeField] private MunnitionData munnitionData;
    public static event Action<int> Damage;

    void Start()
    {
        Invoke("DestroyMunition", munnitionData.DestroyTime);

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 8)
        {
            if (other.gameObject.tag == "Player")
            {
                Damage?.Invoke(munnitionData.damage);
            }
            DestroyMunition();
            Instantiate(munnitionData.EfectoDeDestruccion, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
    private void Move()
    {
        transform.Translate(Vector3.forward * munnitionData.Speed * Time.deltaTime);
    }

    private void DestroyMunition()
    {
        Destroy(gameObject);
    }
}
