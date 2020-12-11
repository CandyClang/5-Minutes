using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerPlaceholderScript : NetworkBehaviour
{
    [SerializeField] private Vector3 up = new Vector3();
    [SerializeField] private Vector3 down = new Vector3();
    [SerializeField] private Vector3 right = new Vector3();
    [SerializeField] private Vector3 left = new Vector3();

    [Client]
    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority) { return; }

        if (Input.GetKeyDown(KeyCode.W))
            CmdUp();

        if (Input.GetKeyDown(KeyCode.S))
            CmdDown();

        if (Input.GetKeyDown(KeyCode.D))
            CmdRight();

        if (Input.GetKeyDown(KeyCode.A))
            CmdLeft();
    }

    [Command]
    private void CmdUp()
    {
        RpcUp();
    }

    [Command]
    private void CmdDown()
    {
        RpcDown();
    }

    [Command]
    private void CmdRight()
    {
        RpcRight();
    }

    [Command]
    private void CmdLeft()
    {
        RpcLeft();
    }

    [ClientRpc]
    private void RpcUp() => transform.Translate(up);

    [ClientRpc]
    private void RpcDown() => transform.Translate(down);

    [ClientRpc]
    private void RpcRight() => transform.Translate(right);

    [ClientRpc]
    private void RpcLeft() => transform.Translate(left);
}
