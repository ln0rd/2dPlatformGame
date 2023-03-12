using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{

    private Vector2 velocity;
    private Transform player;

    private float smoothTimeX = 0.5f;
    private float smoothTimeY = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.position.y, ref velocity.y, smoothTimeX);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
