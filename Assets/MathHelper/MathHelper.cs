using System.Collections.Generic;
using UnityEngine;

public static class MathHelper
{
    public enum ModificationType 
    {
        Add,
        Subtract,
        Set,
        Multiply,
        Divide
    }

    public static void ModifyValue(ModificationType modificationType, ref float value, float amount)
    {
        switch (modificationType)
        {
            case ModificationType.Add:
                value += amount;
                break;
            case ModificationType.Subtract:
                value -= amount;
                break;
            case ModificationType.Set:
                value = amount;
                break;
            case ModificationType.Divide:
                value /= amount;
                break;
            case ModificationType.Multiply:
                value *= amount;
                break;
            default:
                break;
        }
    }

    public static float RandomBet(float min = 0f, float max = 100f)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public static int RandomBet(int min = 0, int max = 100)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public static T RandomFromArray<T>(T[] list)
    {
        return list[RandomBet(max: list.Length)];
    }

    public static T RandomFromArray<T>(T[] list, out int index)
    {
        index = RandomBet(max: list.Length);
        return list[index];
    }

    public static T RandomFromList<T>(List<T> list)
    {
        return list[RandomBet(max: list.Count)];
    }

    public static T RandomFromList<T>(List<T> list, out int index)
    {
        index = RandomBet(max: list.Count);
        return list[index];
    }

    public static Vector3 RandomInArea(Vector3 area)
    {
        float xPos = UnityEngine.Random.Range(-area.x / 2f, area.x / 2f);
        float yPos = UnityEngine.Random.Range(-area.y / 2f, area.y / 2f);
        float zPos = UnityEngine.Random.Range(-area.z / 2f, area.z / 2f);
        return new Vector3(xPos, yPos, zPos);
    }

    public static Vector3 RandomPointAtCircumferenceXZ(Vector3 center, float radius)
    {
        float theta = RandomBet(max: 360f);
        float opposite = radius * Mathf.Sin(theta);
        float adjacent = radius * Mathf.Cos(theta);
        return center + new Vector3(adjacent, 0f, opposite);
    }

    public static Vector3 RandomDirectionAroundY()
    {
        int index = RandomBet(max: 4);
        if (index == 0)
        {
            return Vector3.forward;
        }
        else if (index == 1)
        {
            return Vector3.back;
        }
        else if (index == 3)
        {
            return Vector3.left;
        }
        else
        {
            return Vector3.right;
        }
    }

    public static Vector3 RandomDirectionAny()
    {
        int index = RandomBet(max: 6);
        if (index == 0)
        {
            return Vector3.forward;
        }
        else if (index == 1)
        {
            return Vector3.back;
        }
        else if (index == 3)
        {
            return Vector3.left;
        }
        else if (index == 4)
        {
            return Vector3.right;
        }
        else if (index == 5)
        {
            return Vector3.up;
        }
        else
        {
            return Vector3.down;
        }
    }

    //List = the list of options 
    //Chances = the rarity corresponding with the list
    public static int RandomRarity<T>(T[] list, float[] chances)
    {
        float[] tierChances = new float[list.Length];
        float prevChance = 0f;

        //makes tierChances look like a number line
        //0--[chance 1]--30--[chance 2]--70--[chance 3]--100
        for (int i = 0; i < list.Length; i++)
        {
            tierChances[i] = prevChance + chances[i];
            prevChance = tierChances[i];
        }

        //simply randomizes a number and then check the ranges
        int randomTier = UnityEngine.Random.Range(0, 100);
        for (int i = 0; i < tierChances.Length; i++)
        {
            float highNum = i == tierChances.Length - 1 ? 100 : tierChances[i];
            float lowNum = i == 0 ? 0 : tierChances[i - 1];
            if (randomTier > lowNum && randomTier < highNum)
            {
                return i;
            }
        }

        return 0;
    }
}