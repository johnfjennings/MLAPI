using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Spawning;
using System;
using MLAPI.Transports.UNET;

public class ConnectionManager : NetworkBehaviour
{

    public GameObject connectionButtonPanel;
    public GameObject selectRig;
    public NetworkManager netManager;

    public string ipAddress = "127.0.0.1";

    UNetTransport transport;

    //happens on server
    public void Host()
    {
        Destroy(selectRig);
        connectionButtonPanel.SetActive(false);
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost(Vector3.zero, Quaternion.identity);
    }
    //happens on server
    private void ApprovalCheck(byte[] connectionData, ulong clientID, NetworkManager.ConnectionApprovedDelegate callback)
    {
        Debug.Log("Approving connection");
        bool approve = System.Text.Encoding.ASCII.GetString(connectionData) == "pass";
        callback(true, null, approve, Vector3.zero, Quaternion.identity);
    }

    public void Join()
    {
        Destroy(selectRig);
        transport = NetworkManager.Singleton.GetComponent<UNetTransport>();
        transport.ConnectAddress = ipAddress;
        connectionButtonPanel.SetActive(false);
        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("pass");
        NetworkManager.Singleton.StartClient();
    }

    public void IpAddressChanged(string newAddress)
    {
        this.ipAddress = newAddress;
    }
}
