using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadDespawn : MonoBehaviour
{
    protected float distance = 0;

    private void FixedUpdate()
    {
        this.UpdateRoad();
    }

    protected virtual void UpdateRoad()
    {
        this.distance = Vector3.Distance(PlayerController.instance.transform.position, transform.position);
        if (this.distance > 220) this.Despawn();
    }

    protected virtual void Despawn()
    {
        Destroy(gameObject);
    }
}
