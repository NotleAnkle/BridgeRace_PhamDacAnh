using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField] private StairBrick stairBrickPrefab;
    private void Start()
    {
        for(int i = 1; i < 8; i++)
        {
            StairBrick stairBrick = Instantiate(stairBrickPrefab, transform);
            stairBrick.name = "StairBrick " + i;
            stairBrick.TF.localPosition = new Vector3 (0, 0.7f + i*0.5f, i*0.5f);

            stairBrick = Instantiate(stairBrickPrefab, transform);
            stairBrick.name = "StairBrick -" + i;
            stairBrick.TF.localPosition = new Vector3(0, 0.7f - i * 0.5f, -i * 0.5f);
        }
    }
}
