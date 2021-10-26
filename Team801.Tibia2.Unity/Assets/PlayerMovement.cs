using System.Collections;
using System.Collections.Generic;
using Team801.Tibia2.Client;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Client _client;

    public Transform PlayerTransform;

    // Start is called before the first frame update
    void Start()
    {
        _client = new Client();
        _client.Connect("makz unity");
        _client.PlayerManager.Moved += PlayerManagerOnMoved;
    }

    private void PlayerManagerOnMoved(Vector2 newPosition)
    {
        PlayerTransform.position = newPosition;
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        _client.Move(new Vector2(x, y));
    }
}
