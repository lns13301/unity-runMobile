using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject[] effects;
    public static EffectManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createEffect(Vector2 position, GameObject gameObject, int index = 0)
    {
        GameObject effect = Instantiate(effects[index]);
        effect.transform.SetParent(gameObject.transform.parent);
        effect.transform.position = position;
    }
}
