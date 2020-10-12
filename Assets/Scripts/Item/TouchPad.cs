using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TouchPad : MonoBehaviour
{
    public static TouchPad instance;

    public Text[] inputField;
    public int inputCount;
    public string touchPanelNumberString;

    private void Start()
    {
        instance = this;
    }

    public void showPanelNumber()
    {
        try
        {
            // 상점 패널
            if (GameObject.Find("Canvas").GetComponent<ShopUI>().shopSet.activeSelf && ShopInformation.instance.item.count < Calculator.fomattingToInteger(touchPanelNumberString))
            {
                touchPanelNumberString = "" + ShopInformation.instance.item.count;

                if (touchPanelNumberString == null)
                {
                    touchPanelNumberString = "0";
                }
            }

            inputField[0].text = Calculator.numberToFormatting(Calculator.fomattingToInteger(touchPanelNumberString));

            ShopInformation.instance.itemPrice.text = getNumber() * ShopInformation.instance.item.price + " 핀";
            ShopInformation.instance.itemSellPrice.text = getNumber() * ShopInformation.instance.item.price + " 핀";
        }
        catch (NullReferenceException)
        {

        }
    }

    public int getNumber()
    {
        try
        {
            int value = 0;

            if (touchPanelNumberString != null)
            {
                value = Calculator.fomattingToInteger(touchPanelNumberString);
            }

            return value;
        }
        catch
        {
            return 0;
        }
    }

    private void countLimit(int count)
    {
        if (touchPanelNumberString.Length > count)
        {
            touchPanelNumberString = "" + count;
        }
    }

    private void removeEmptyNumber()
    {
        if (touchPanelNumberString == "0")
        {
            touchPanelNumberString = "";
        }
    }

    public void addZero()
    {
        removeEmptyNumber();
        touchPanelNumberString += "0";
        showPanelNumber();
    }

    public void addOne()
    {
        removeEmptyNumber();
        touchPanelNumberString += "1";
        showPanelNumber();
    }

    public void addTwo()
    {
        removeEmptyNumber();
        touchPanelNumberString += "2";
        showPanelNumber();
    }

    public void addThree()
    {
        removeEmptyNumber();
        touchPanelNumberString += "3";
        showPanelNumber();
    }

    public void addFour()
    {
        removeEmptyNumber();
        touchPanelNumberString += "4";
        showPanelNumber();
    }

    public void addFive()
    {
        removeEmptyNumber();
        touchPanelNumberString += "5";
        showPanelNumber();
    }

    public void addSix()
    {
        removeEmptyNumber();
        touchPanelNumberString += "6";
        showPanelNumber();
    }

    public void addSeven()
    {
        removeEmptyNumber();
        touchPanelNumberString += "7";
        showPanelNumber();
    }

    public void addEight()
    {
        removeEmptyNumber();
        touchPanelNumberString += "8";
        showPanelNumber();
    }

    public void addNine()
    {
        removeEmptyNumber();
        touchPanelNumberString += "9";
        showPanelNumber();
    }

    public void removeLastNumberText()
    {
        if (touchPanelNumberString != null)
        {
            touchPanelNumberString = "" + (int.Parse(touchPanelNumberString) / 10);
            showPanelNumber();
        }
    }

    public void removeAllText()
    {
        if (touchPanelNumberString != null)
        {
            touchPanelNumberString = "0";
            showPanelNumber();
        }
    }
}
