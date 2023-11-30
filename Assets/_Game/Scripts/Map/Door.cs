using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] public int index;
    [SerializeField] private Renderer render;

    public void OnDespawn()
    {
        render.enabled = false;
    }
}
