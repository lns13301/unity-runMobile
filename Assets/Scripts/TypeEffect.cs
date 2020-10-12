using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public float charPerSeconds;
    public GameObject touchCursor;
    public bool isAnimation;

    private Text messageText;
    private AudioSource audioSource;

    private string targetMessage;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        //charPerSeconds = 0.1f;
    }

    private void Awake()
    {
        messageText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void setMessage(string msg)
    {
        if (isAnimation)
        {
            messageText.text = targetMessage;
            CancelInvoke();
            effectEnd();
        }
        else
        {
            targetMessage = msg;
            effectStart();
        }
    }

    void effectStart()
    {
        messageText.text = "";
        index = 0;
        isAnimation = true;
        //touchCursor.SetActive(false); // 대사가 다 끝나야 다음 대사를 볼 수 있는 기능

        Invoke("effecting", charPerSeconds);
    }

    void effecting()
    {
        if (messageText.text == targetMessage)
        {
            effectEnd();
            return;
        }

        messageText.text += targetMessage[index];

        if (targetMessage[index] != ' ' || targetMessage[index] != '.')
        {
            audioSource.Play();
        }

        index++;

        Invoke("effecting", charPerSeconds);
    }

    void effectEnd()
    {
        isAnimation = false;
        touchCursor.SetActive(true);
    }
}
