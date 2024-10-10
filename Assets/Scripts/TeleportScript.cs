using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    private float backgroundWidth;
    [SerializeField] Transform otherBackground;

    void Start()
    {
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        if (ShouldTeleport())
        {
            TeleportToZone();
        }
    }

    private bool ShouldTeleport()
    {
        return transform.position.x < -backgroundWidth;
    }
    private void TeleportToZone()
    {
        Vector3 newPosition = otherBackground.position + Vector3.right * backgroundWidth;
        transform.position = newPosition;
    }
}