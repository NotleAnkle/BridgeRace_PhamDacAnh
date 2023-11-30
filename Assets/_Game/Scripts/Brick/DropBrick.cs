using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBrick : GameUnit
{
    [SerializeField] private Rigidbody rb;
    private Stage stage;
    // Start is called before the first frame update

    public void OnInit()
    {
        rb.velocity = Vector3.zero;
        Vector3 force = RandomForce();
        rb.AddForce(force);
    }

    private void Update()
    {
        if (transform.position.y < -5f)  {
            OnDespawn();
        }
    }

    public void SetStage(Stage stage)
    {
        this.stage = stage;
    }

    private Vector3 RandomForce()
    {
        float vaule = 50f;
        float x = Random.Range(-vaule, vaule);
        float y = Random.Range(vaule/2, vaule);
        float z = Random.Range(-vaule, vaule);

        return new Vector3(x, y, z);
    }

    private void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            stage.SpawnGrayBrickAt(transform);
            OnDespawn();
        }
    }
}
