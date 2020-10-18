using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRange : MonoBehaviour
{
    public EntityData selectedHero;
    public GameObject tileGameObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tileGameObject = collision.gameObject;

        if ((collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Trap") && PlayerAction.instance.isJumping)
        {
            PlayerAction.instance.SetLanding();
        }

        if (collision.gameObject.tag == "Enemy" && PlayerAction.instance.actionType == ActionType.CHOPPING)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(PlayerAction.instance.actionType, selectedHero);
            PlayerAction.instance.SetLanding();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 공중일 때 띄운상태로
        if (collision.gameObject.tag != null && collision.gameObject.tag == "Ground" && tileGameObject.tag != "Trap")
        {
            PlayerAction.instance.isJumping = true;
        }
    }
}
