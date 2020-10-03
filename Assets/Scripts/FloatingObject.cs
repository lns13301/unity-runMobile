using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float upDownSpeed = 0.01f;
    private float upDownTimer;
    private bool isStateUp;
    public float turningSpeed = 60f;
    private float turningTimer;

    private Vector3 transformBasePosition;

    // Start is called before the first frame update
    void Start()
    {
        transformBasePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Floating();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // TODO 플레이어 점수 ++
            SoundManager.instance.PlayOneShotEffectSound(0);
            ParticleManager.instance.CreateEffect(new Vector2(transform.position.x + 1f, transform.position.y), gameObject, 0);
            Destroy(gameObject);
        }
    }

    private void Floating()
    {
        upDownTimer += Time.fixedDeltaTime;
        turningTimer += Time.deltaTime;

        // 스핀
        transform.rotation = Quaternion.Euler(-90, 0, turningSpeed * turningTimer);

        // 위 아래 이동
        if (isStateUp)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, transformBasePosition.y + 1, Time.deltaTime / 3), transformBasePosition.z);
            //transform.position = new Vector2(transform.position.x, transform.position.y + upDownSpeed);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, transformBasePosition.y - 1, Time.deltaTime / 3), transformBasePosition.z);
            //transform.position = new Vector2(transform.position.x, transform.position.y - upDownSpeed);
        }

        if (upDownTimer >= 3f)
        {
            if (isStateUp == true)
            {
                isStateUp = false;
            }
            else
            {
                isStateUp = true;
            }

            upDownTimer = 0;
        }
    }
}
