using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTarget;
    public Vector3 offset;

    void Start()
    {
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
    }

    void Update() //Have the camera follow the player.
    {
        transform.position = new Vector3(playerTarget.position.x + offset.x, playerTarget.position.y + offset.y, offset.z);
    }

    public static class TagManager
    {
        public static string PLAYER_TAG = "Player";
    }
}
