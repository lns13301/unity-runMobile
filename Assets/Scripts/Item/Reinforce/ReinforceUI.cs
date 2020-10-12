using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReinforceUI : MonoBehaviour
{
    public static ReinforceUI instance;

    public string NEW_LINE = "\n";

    public GameObject reinforceSet;
    public ReinforceSlot reinforceSlot;
    public float[] reinforceAmount;

    public GameObject popUp;
    public Text popUpMessage;
    public GameObject doIt;
    public Text doItMessage;

    public Animator buttonMenuAnimator;

    public Item item;
    public int slotNumber;
    public GameObject slot;
    public GameObject hammer;
    public Animator hammerAnim;
    public Animator slotAnim;
    public bool isStarted = false;
    public int hammeringCount = 0;
    public bool isFinish = true;
    public int hammerCount = 0;

    public GameObject particlePrefab;
    public float particleTimer;
    public int particleCount;

    public float successRate;

    //when doing buttons
    public Button reinforce;
    public Button selectItem;

    //particle Bounus
    public float particleScore;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        particleScore = 0;

        popUp.SetActive(false);
        doIt.SetActive(false);
        hammer.SetActive(false);
        reinforceSet.SetActive(false);

        reinforceAmount = new float[21];
        reinforceAmount[0] = 0.01f;
        reinforceAmount[1] = 0.02f;
        reinforceAmount[2] = 0.04f;
        reinforceAmount[3] = 0.07f;
        reinforceAmount[4] = 0.11f;
        reinforceAmount[5] = 0.15f;
        reinforceAmount[6] = 0.21f;
        reinforceAmount[7] = 0.28f;
        reinforceAmount[8] = 0.36f;
        reinforceAmount[9] = 0.45f;
        reinforceAmount[10] = 0.55f;
        reinforceAmount[11] = 0.60f;
        reinforceAmount[12] = 0.65f;
        reinforceAmount[13] = 0.70f;
        reinforceAmount[14] = 0.75f;
        reinforceAmount[15] = 0.80f;
        reinforceAmount[16] = 0.85f;
        reinforceAmount[17] = 0.90f;
        reinforceAmount[18] = 0.95f;
        reinforceAmount[19] = 1f;
        reinforceAmount[20] = 1.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        {
            isStarted = false;
            isFinish = false;
            GameObject.Find("Canvas").GetComponent<ItemMenuSet>().isReinforceProgressing = true;
            moveSlotToHammer();
        }
    }

    public void uiOnOff()
    {
        if (reinforceSet.activeSelf)
        {
            reinforceSet.SetActive(false);
        }
        else
        {
            if (GetComponent<InventoryUI>().inventorySet.activeSelf)
            {
                GetComponent<InventoryUI>().inventorySet.SetActive(false);
            }
            if (GetComponent<StatUI>().statSet.activeSelf)
            {
                GetComponent<StatUI>().statSet.SetActive(false);
            }
            if (GetComponent<EquipmentUI>().equipmentSet.activeSelf)
            {
                GetComponent<EquipmentUI>().equipmentSet.SetActive(false);
            }
            reinforceSet.SetActive(true);
            hammer.SetActive(false);
            reinforce.gameObject.SetActive(true);
            selectItem.gameObject.SetActive(true);
            buttonMenuAnimator.SetBool("isUIOn", true);
        }
    }

    public void doItOn()
    {
        if (reinforceSlot.item == null)
        {
            return;
        }

        successRate = (100 - reinforceSlot.item.reinforce * 9);

        if (successRate < 0)
        {
            successRate = 0;
        }

        doIt.SetActive(true);
        doItMessage.text = "강화를 진행하시겠습니까?" + NEW_LINE
            + "비용 : " + (reinforceSlot.item.reinforce + 1) * 2 + " Money" + NEW_LINE
            + "보유금액 : " + GameManager.instance.playerData.money + " Money" + NEW_LINE
            + "성공확률 (기본) : " + successRate + "%";
    }

    public void doItOff()
    {
        doIt.SetActive(false);
    }

    public void moveSlotToHammer()
    {
        reinforce.gameObject.SetActive(false);
        selectItem.gameObject.SetActive(false);

        doIt.SetActive(false);

        if (item.reinforce > 20)
        {
            popUp.SetActive(true);
            popUpMessage.text = "더이상 강화를 진행할 수 없습니다.";
            settings();

            return;
        }

        if (GameManager.instance.playerData.money < (item.reinforce + 1) * 2)
        {
            popUp.SetActive(true);
            popUpMessage.text = "비용이 부족합니다.";
            settings();

            return;
        }
        //누르는 순간 재화소모
        GameManager.instance.playerData.money -= (item.reinforce + 1) * 2;
        GameManager.instance.savePlayerDataToJson();

        //particleTimer = (float) (1 - item.reinforce * 0.1);

        hammer.SetActive(true);
        slotAnim.SetTrigger("doReinforce");

        // 파티클 수량 조절 시 1
        InvokeRepeating("onHammer", 1f, 0.2f);
        Invoke("doReinforce", 7.8f);
    }

    public void summonParticle()
    {
        /*        particles[particleIndex].transform.position = reinforceSlot.gameObject.transform.position;
                particles[particleIndex].gameObject.SetActive(true);
                particles[particleIndex].GetComponent<Animator>().SetTrigger("doParticle");
                particles[particleIndex++].setNote();

                if (particleIndex == particles.Length)
                {
                    particleIndex = 0;
                }*/

        GameObject go = Instantiate(particlePrefab, new Vector2(reinforceSlot.gameObject.transform.position.x, reinforceSlot.gameObject.transform.position.y + 100), Quaternion.identity);
        go.transform.SetParent(transform);
        go.GetComponent<ReinforceParticle>().setNote();
    }

    public void onHammer()
    {
        hammerAnim.SetTrigger("doHammering");
        summonParticle();

        // 파티클 수량 조절 시 2
        if (hammerCount == 20)
        {
            CancelInvoke("onHammer");
            hammerCount = 0;
        }

        hammerCount++;
        hammer.GetComponent<AudioSource>().Play();
    }

    public void startReinforceWork()
    {
        if (isFinish)
        {
            isStarted = true;
        }
    }

    public void doReinforce()
    {
        successRate = 100 - item.reinforce * 9;

        if (successRate < 0)
        {
            successRate = 0;
        }

        // 파티클 수량 조절 시 3
        successRate += ((100 - successRate) / 63) * particleScore;
        successRate = Mathf.Round(successRate * 100) / 100;

        if (generateRandom(successRate))
        {
            item = progressReinforce(item);
            popUp.SetActive(true);
            popUpMessage.text = "장비 강화에 성공했습니다!" + NEW_LINE + "(최종확률 : " + successRate + "%)";

            settings();
            PlayerInventory.instance.removeItem(slotNumber);
            PlayerInventory.instance.addItem(item);
            PlayerInventory.instance.onChangeItem.Invoke();
            slotNumber = PlayerInventory.instance.items.Count - 1;
            reinforceSlot.item = PlayerInventory.instance.items[slotNumber];
            reinforceSlot.updateSlotUI();
        }
        else
        {
            popUp.SetActive(true);
            popUpMessage.text = "장비 강화에 실패했습니다!" + NEW_LINE + "(최종확률 : " + successRate + "%)";

            settings();
        }
        /* 이 방법은 강화재화가 사라지면 오류발생
         * PlayerInventory.instance.items[reinforceSlot.slotNumber] = instanceItem;
        PlayerInventory.instance.onChangeItem.Invoke();
        reinforceSlot.item = PlayerInventory.instance.items[reinforceSlot.slotNumber];*/
    }

    private void settings()
    {
        particleScore = 0;
        isStarted = false;
        isFinish = true;
        hammer.SetActive(false);
        GameObject.Find("Canvas").GetComponent<ItemMenuSet>().isReinforceProgressing = false;
        reinforce.gameObject.SetActive(true);
        selectItem.gameObject.SetActive(true);
    }

    public void offMessage()
    {
        Debug.Log("팝업끄기");
        popUp.SetActive(false);
    }

    public bool generateRandom(float num)
    {
        return Random.Range(0, 1000) / 10 <= num;
    }

    public Item progressReinforce(Item instanceItem)
    {
        if (instanceItem.power != 0)
        {
            float value = Mathf.Ceil((float)instanceItem.power * reinforceAmount[instanceItem.reinforce]);
            if (value < 0)
            {
                value = 0;
            }

            instanceItem.powerReinforce += (int)(value + instanceItem.reinforce);
        }
        if (instanceItem.armor != 0)
        {
            float value = Mathf.Ceil((float)instanceItem.armor * reinforceAmount[instanceItem.reinforce]);
            if (value < 0)
            {
                value = 0;
            }

            instanceItem.armorReinforce += (int)(value + instanceItem.reinforce);
        }
        if (instanceItem.accuracyReinforce != 0)
        {
            float value = Mathf.Ceil((float)instanceItem.accuracy * reinforceAmount[instanceItem.reinforce]);
            if (value < 0)
            {
                value = 0;
            }

            instanceItem.accuracyReinforce += (int)(value + Mathf.Ceil(instanceItem.reinforce / 2));
        }
        if (instanceItem.avoid != 0)
        {
            float value = Mathf.Ceil((float)instanceItem.avoid * reinforceAmount[instanceItem.reinforce]);
            if (value < 0)
            {
                value = 0;
            }

            instanceItem.avoidReinforce += (int)(value + Mathf.Ceil(instanceItem.reinforce / 2));
        }
        if (instanceItem.critRate != 0)
        {
            float value = instanceItem.critRate * reinforceAmount[instanceItem.reinforce];
            if (value < 0)
            {
                value = 0;
            }

            instanceItem.critRateReinforce += (float)(value + instanceItem.reinforce * 0.1);
        }
        if (instanceItem.critDam != 0)
        {
            float value = instanceItem.critDam * reinforceAmount[instanceItem.reinforce];
            if (value < 0)
            {
                value = 0;
            }

            instanceItem.critDamReinforce += (float)(value + instanceItem.reinforce * 0.3);
        }


        if (instanceItem.intellectPoint != 0)
        {
            float value = Mathf.Ceil((float)instanceItem.intellectPoint * reinforceAmount[instanceItem.reinforce]);
            if (value < 0)
            {
                value = 0;
            }

            instanceItem.intellectPoint += (int)(value + instanceItem.reinforce * 3);
        }
        if (instanceItem.wisdomPoint != 0)
        {
            float value = Mathf.Ceil((float)instanceItem.wisdomPoint * reinforceAmount[instanceItem.reinforce]);
            if (value < 0)
            {
                value = 0;
            }

            instanceItem.wisdomPoint += (int)(value + instanceItem.reinforce * 3);
        }

        if (instanceItem.dexterityPoint != 0)
        {
            float value = Mathf.Ceil((float)instanceItem.dexterityPoint * reinforceAmount[instanceItem.reinforce]);
            if (value < 0)
            {
                value = 0;
            }

            instanceItem.dexterityPoint += (int)(value + instanceItem.reinforce * 3);
        }
        if (instanceItem.concentrationPoint != 0)
        {
            float value = Mathf.Ceil((float)instanceItem.concentrationPoint * reinforceAmount[instanceItem.reinforce]);
            if (value < 0)
            {
                value = 0;
            }

            instanceItem.concentrationPoint += (int)(value + instanceItem.reinforce * 3);
        }

        if (instanceItem.healthPoint != 0)
        {
            float value = Mathf.Ceil((float)instanceItem.healthPoint * reinforceAmount[instanceItem.reinforce]);
            if (value < 0)
            {
                value = 0;
            }

            instanceItem.healthPointReinforce += (int)(value + instanceItem.reinforce * 3);
        }
        if (instanceItem.manaPoint != 0)
        {
            float value = Mathf.Ceil((float)instanceItem.manaPoint * reinforceAmount[instanceItem.reinforce]);
            if (value < 0)
            {
                value = 0;
            }

            instanceItem.manaPointReinforce += (int)(value + instanceItem.reinforce * 3);
        }

        if (instanceItem.expEff != 0)
        {
            float value = instanceItem.expEff * reinforceAmount[instanceItem.reinforce];
            if (value < 0)
            {
                value = 0;
            }

            instanceItem.expEffReinforce += (float)(value + instanceItem.reinforce * 0.2);
        }

        if (instanceItem.effecienty != 0)
        {
            float value = instanceItem.effecienty * reinforceAmount[instanceItem.reinforce];
            if (value < 0)
            {
                value = 0;
            }

            instanceItem.effecientyReinforce += (float)(value + instanceItem.reinforce * 0.2);
        }
        if (instanceItem.speed != 0)
        {
            float value = instanceItem.speed * reinforceAmount[instanceItem.reinforce];
            if (value < 0)
            {
                value = 0;
            }

            instanceItem.speedReinforce += (float)(value + instanceItem.reinforce * 0.2);
        }
        if (instanceItem.luck != 0)
        {
            float value = instanceItem.luck * reinforceAmount[instanceItem.reinforce];
            if (value < 0)
            {
                value = 0;
            }

            instanceItem.luckReinforce += (float)(value + instanceItem.reinforce * 0.2);
        }
        if (instanceItem.bonus != 0)
        {
            float value = instanceItem.bonus * reinforceAmount[instanceItem.reinforce];
            if (value < 0)
            {
                value = 0;
            }

            instanceItem.bonusReinforce += (float)(value + instanceItem.reinforce * 0.2);
        }
        if (instanceItem.ability != 0)
        {
            float value = instanceItem.ability * reinforceAmount[instanceItem.reinforce];
            if (value < 0)
            {
                value = 0;
            }

            instanceItem.abilityReinforce += (float)(value + instanceItem.reinforce * 0.2);
        }

        instanceItem.reinforce++;

        return instanceItem;
    }
}
