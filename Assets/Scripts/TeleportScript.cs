using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    private float backgroundWidth;
    public Transform otherBackground;

    void Start()
    {
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void Update()
    {
        if (shouldTeleport())
        {
            teleportToZone();
        }
    }
    private bool shouldTeleport()
    {
        return transform.position.x < -backgroundWidth;
    }
    private void teleportToZone()
    {
        Vector3 newPosition = otherBackground.position + Vector3.right * backgroundWidth;
        transform.position = newPosition;
    }
}