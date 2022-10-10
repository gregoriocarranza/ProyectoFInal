
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    [SerializeField] private MunnitionData munnitionData;

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
