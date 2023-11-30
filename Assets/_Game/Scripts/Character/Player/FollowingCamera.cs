using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : GameUnit
{
    private Vector3 offset;
    public GameUnit target;
    // Start is called before the first frame update
    void Start()
    {
        offset = TF.position - target.TF.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = offset + target.TF.position;
    }
}
