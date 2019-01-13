﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// NetworkManager derivation for the server
/// </summary>
public class NetworkManagerServer : NetworkManager
{
    IDictionary<int, GameObject> pETVIDs;
    
    // Called on the server, when it starts
    public override void OnStartServer()
    {
        base.OnStartServer();

        pETVIDs = new Dictionary<int, GameObject>();

        Debug.Log("Server started");
    }

    private void Start()
    {
        // Initialize dependent Services
        Services.Persistence().Load();

        StartServer();
        GetComponent<NetworkDiscovery>().Initialize();
        GetComponent<NetworkDiscovery>().StartAsServer();
    }

    // Called on the server when a new client connects.
    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        
        pETVIDs.Add(conn.connectionId, null); // Show ConnectionID on anchor and on physical device

        Debug.Log("A new pETV connected, with ID: " + conn.connectionId);
    }

    // Called on the server when a client disconnects.
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);

        pETVIDs.Remove(conn.connectionId);

        Debug.Log("A pETV disconnected, with ID: " + conn.connectionId);
    }
}
