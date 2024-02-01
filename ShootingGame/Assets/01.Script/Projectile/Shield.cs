using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Vector3 _direction;

    [SerializeField]
    private float _lifeTime = 30f;

    public Transform ParentObject;
    public float rotationSpeed = 5f;

    void Start()
    {
        Destroy(gameObject, _lifeTime);
        Vector3 pos = _direction * 0.5f;
        transform.Translate(pos);

        ParentObject = transform.parent;
    }

    void Update()
    {
        float angle = rotationSpeed * Time.deltaTime;

        transform.RotateAround(transform.parent.position, Vector3.forward, angle);
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D collsion)
    {
        if (collsion.gameObject.CompareTag("Enemy"))
        {
            Destroy(collsion.gameObject);
            Destroy(gameObject);
        }
    }
}
