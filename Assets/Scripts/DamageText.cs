using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float speed;
    public float alphaSpeed;
    public float destroyTimer;
    TextMeshPro text;
    Color alpha;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        if (damage == 0)
        {
            text.text = "Miss";
        }
        else
        {
            text.text = damage.ToString();
        }
        alpha = text.color;
        Invoke("DestroyObject", destroyTimer);
        text.sortingOrder = 1;
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
