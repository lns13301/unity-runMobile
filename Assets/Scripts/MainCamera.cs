using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainCamera : MonoBehaviour
{
    public Transform playerTransform;
    private float followSpeed = 1f;
    private float offsetX = 0f;
    private float offsetY = 0f;
    private float offsetZ = -12f;

    private Vector3 cameraPosition;

    private Vector3 player_position;

    public bool isDamaged;
    public float cameraTimer;

    // Background
    public GameObject background;
    public GameObject[] clouds;
    private float cloudSpeed = -0.2f;

    // Start is called before the first frame update
    void Start()
    {
        //playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FixCameraPositionX();
        FixCameraPositionY();
        cameraPosition.z = playerTransform.position.z + offsetZ;

        transform.position = 
            Vector3.Lerp(transform.position, cameraPosition, followSpeed * Time.deltaTime * 2);

        MoveBackground();
        MoveCloud();
        FixCameraShake();
    }

    private void FixCameraShake()
    {
        if (isDamaged)
        {
            if (cameraTimer < 0.1f)
            {
                transform.position = new Vector3(
                    Random.Range(cameraPosition.x - 0.1f, cameraPosition.x + 0.1f),
                    Random.Range(cameraPosition.y - 0.1f, cameraPosition.y + 0.1f),
                    -10);
                cameraTimer += Time.deltaTime;

                return;
            }
            else
            {
                isDamaged = false;
            }
        }
    }

    public void SetCameraShake()
    {
        isDamaged = true;
        cameraTimer = 0;
    }

    private void FixCameraPositionX()
    {
        player_position = PlayerAction.instance.transform.position;

        cameraPosition.x = playerTransform.position.x + offsetX + 4f;
    }

    private void FixCameraPositionY()
    {
        player_position = PlayerAction.instance.transform.position;

        cameraPosition.y = playerTransform.position.y + offsetY + 2.1f;
    }

    private void MoveBackground()
    {
        // 배경 움직이기
        Vector3 backgroundAimPosition = new Vector3(cameraPosition.x, cameraPosition.y, background.transform.position.z);

        background.transform.position =
            Vector3.Lerp(background.transform.position, backgroundAimPosition, followSpeed * Time.deltaTime * 1.3f);
    }

    private void MoveCloud()
    {
        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i].transform.position = new Vector2(clouds[i].transform.position.x + cloudSpeed * Time.deltaTime, clouds[i].transform.position.y);

            Vector3 CloudAimPosition = new Vector3(clouds[i].transform.position.x, cameraPosition.y + 3, clouds[i].transform.transform.position.z);

            clouds[i].transform.position =
                Vector3.Lerp(clouds[i].transform.position, CloudAimPosition, followSpeed * Time.deltaTime * 1.5f);
        }
    }
}
