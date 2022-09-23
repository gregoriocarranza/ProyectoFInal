
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Start is called before the first frame updateVector3(0.443978846,-0.146086693,-0.0994603038)
    public Vector3 direction = new Vector3(0f, 1f, 0f);

    [SerializeField][Range(0f, 5f)] public float Speed = 1f;
    [SerializeField][Range(1f, 10f)] public float DestroyTime = 1f;


    void Start()
    {
        Invoke("DestroyMunition", DestroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Entrand");
            DestroyMunition();
        }

    }
    private void Move()
    {
        transform.Translate(direction * Speed * Time.deltaTime);
    }

    private void DestroyMunition()
    {
        Destroy(gameObject);
    }
}
