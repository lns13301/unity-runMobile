using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public int healthPoint = 0;
    public int manaPoint = 0;
    public bool isHPPercent = false;
    public bool isMPPercent = false;

    public ItemEffect(int healthPoint, int manaPoint, bool isHPPercent = false, bool isMPPercent = false)
    {
        this.healthPoint = healthPoint;
        this.manaPoint = manaPoint;
        this.isHPPercent = isHPPercent;
        this.isMPPercent = isMPPercent;
    }

    public void useItem()
    {
        if (isHPPercent)
        {
            GameManager.instance.playerData.healthPoint += GameManager.instance.playerData.healthPointMax / 100 * healthPoint;
        }
        else
        {
            GameManager.instance.playerData.healthPoint += healthPoint;
        }

        if (isMPPercent)
        {
            GameManager.instance.playerData.manaPoint += GameManager.instance.playerData.manaPointMax / 100 * manaPoint;
        }
        else
        {
            GameManager.instance.playerData.manaPoint += manaPoint;
        }
    }
}
