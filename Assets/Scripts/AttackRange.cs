using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public EntityData selectedHero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!PlayerAction.instance.doAttack && !PlayerAction.instance.doSkill)
        {
            return;
        }

        // 공격마다 맞게끔 처리
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(PlayerAction.instance.actionType, selectedHero);
        }

        // 무적타임 사용 시
/*        if (collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(PlayerAction.instance.actionType);
        }*/
    }
}
