using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReinforceScore : MonoBehaviour
{
    public float speed;
    public float alphaSpeed;
    public float destroyTimer;
    TextMeshProUGUI text;
    Color alpha;
    public string scoreType;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = scoreType;
        if (scoreType == "PERFECT")
        {
            text.color = new Color(1f, 0.45f, 0.8f);
        }
        else if (scoreType == "GREAT")
        {
            text.color = new Color(0.3f, 0.8f, 0.1f);
        }
        else if (scoreType == "GOOD")
        {
            text.color = new Color(0.95f, 0.7f, 0.2f);
        }
        else if (scoreType == "BAD")
        {
            text.color = new Color(0.7f, 0.3f, 1f);
        }
        else
        {
            text.color = new Color(0.4f, 0.25f, 0.95f);
        }
        alpha = text.color;
        Invoke("DestroyObject", destroyTimer);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
