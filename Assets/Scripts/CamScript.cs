using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{

    private Vector2 velocity;
    private Transform player;

    private float smoothTimeX = 0.2f;
    private float smoothTimeY = 0.2f;

    private float shakeTimer;
    private float shakeAmount;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        FollowPlayer();
    }

    void Update()
    {
        ExecuteShakeCamera();
    }

    public void ShakeCamera(float timer, float amount)
    {
        this.shakeTimer = timer;
        this.shakeAmount = amount;
    }


    void ExecuteShakeCamera()
    {
        if (shakeTimer > 0)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmount;
            transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);
        }

        shakeTimer = shakeTimer - Time.deltaTime;
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, player.position.x, ref velocity.x, smoothTimeX);
            float posY = Mathf.SmoothDamp(transform.position.y, player.position.y, ref velocity.y, smoothTimeX);

            transform.position = new Vector3(posX, posY, transform.position.z);
        }
    }
}
