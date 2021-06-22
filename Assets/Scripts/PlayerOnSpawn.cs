using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class PlayerOnSpawn : NetworkBehaviour
{

    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        cam = gameObject.GetComponentInChildren<Camera>();

        if (!IsLocalPlayer)
        {
            cam.gameObject.SetActive(false);
        }
    }
}
