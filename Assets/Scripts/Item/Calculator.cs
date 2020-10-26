using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator
{
    public static HeroData calcAll(HeroData heroData)
    {
        HeroData data = heroData;
/*        data.power = powerCalc(data);
        data.armor = armorCalc(data);
        data.accuracy = accuracyCalc(data);
        data.avoid = avoidCalc(data);
        data.critRate = critRateCalc(data);
        data.critDam = critDamCalc(data);
        data.healthPointMax = healthPointMaxCalc(data);
        data.manaPointMax = manaPointMaxCalc(data);*/

        return data;
    }

    public static int powerCalc(PlayerData data)
    {
        float[] datas = new float[5];

        datas[0] = data.level;
        datas[1] = data.level;
        datas[2] = data.level;
        datas[3] = data.level;
        datas[4] = 0; // data.powerEquipment;
        return (int)(datas[0] * 3 + datas[1] * 2 + datas[2] / 2 + datas[3] / 5 + datas[4]);
    }

    public static int armorCalc(PlayerData data)
    {
        float[] datas = new float[4];

        datas[0] = data.level;
        datas[1] = data.level;
        datas[2] = data.level;
        datas[3] = 0; // data.armorEquipment;
        return (int)(datas[0] * 2 + datas[1] * 1 + datas[2] / 2 + datas[3]);
    }

    public static int accuracyCalc(PlayerData data)
    {
        float[] datas = new float[4];

        datas[0] = data.level;
        datas[1] = data.level;
        datas[2] = data.level;
        datas[3] = 0; // data.accuracyEquipment;
        return (int)(datas[0] * 2 + datas[1] * 0.5 + datas[2] / 4 + datas[3]);
    }

    public static int avoidCalc(PlayerData data)
    {
        float[] datas = new float[4];

        datas[0] = data.level;
        datas[1] = data.level;
        datas[2] = data.level;
        datas[3] = 0; // data.avoidEquipment;
        return (int)(datas[0] / 3 + datas[1] * 2 + datas[2] / 5 + datas[3]);
    }

    public static int critRateCalc(PlayerData data)
    {
        float[] datas = new float[4];

        datas[0] = data.level;
        datas[1] = data.level;
        datas[2] = data.level;
        datas[3] = 0; // data.critRateEquipment;
        return (int)(datas[0] / 10 + datas[1] / 20 + datas[2] / 20 + datas[3]);
    }

    public static int critDamCalc(PlayerData data)
    {
        float[] datas = new float[4];

        datas[0] = data.level;
        datas[1] = data.level;
        datas[2] = data.level;
        datas[3] = 0; // data.critDamEquipment;
        return (int)(datas[0] / 3 + datas[1] / 5 + datas[2] / 4 + datas[3]);
    }

    public static int healthPointMaxCalc(PlayerData data)
    {
        float[] datas = new float[3];

        datas[0] = data.level;
        datas[1] = data.level;
        datas[2] = 0; // data.healthPointEquipment;

        return 1;
    }

    public static int manaPointMaxCalc(PlayerData data)
    {
        float[] datas = new float[4];

        datas[0] = data.level;
        datas[1] = data.level;
        datas[2] = 0; // data.manaPointEquipment;

        return 1;
    }

    public static string numberToFormatting(int num)
    {
        string result = "";

        while (num > 0)
        {
            result = (num % 1000) + result;
            num /= 1000;

            if (num > 0)
            {
                result = "," + result;
            }
        }

        return result;
    }

    public static string numberToFormatting(double num)
    {
        string result = "";

        while (num > 0)
        {
            result = (num % 1000) + result;
            num /= 1000;

            if (num > 0)
            {
                result = "," + result;
            }
        }

        return result;
    }

    public static int fomattingToInteger(string num)
    {
        string[] numbers = num.Split(',');
        string number = "";

        for (int i = 0; i < numbers.Length; i++)
        {
            number += numbers[i];
        }

        return int.Parse(number);
    }

    public static double fomattingToDouble(string num)
    {
        string[] numbers = num.Split(',');
        string number = "";

        for (int i = 0; i < numbers.Length; i++)
        {
            number += numbers[i];
        }

        return double.Parse(number);
    }
}