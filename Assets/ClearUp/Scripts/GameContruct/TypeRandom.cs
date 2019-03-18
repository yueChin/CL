using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeRandom {
    public static int ControlRandomProportion(int[] arrays)
    {
        int random = Random.Range(0, arrays[arrays.Length - 1]);
        for (int i=0; i < arrays.Length; i++)
        {
            if (random < arrays[i] && arrays[i] != 0)
            {
                return i;
            }
        }
        UnityEngine.Debug.Log("所有数值不符合，请检查是否输错");
        return 0;
    }

    public static int RandomProportion(int min, int max)
    {
        //Debug.Log("最小" + min + "最大" + max);
        int[] proportion = new int[max];
        for (int i = 0; i < max; i++)
        {
            if (i + 2 > max) { proportion[i] = 1000; }
            else if(i + 1 < min){ proportion[i] = 0; }
            else {
                proportion[i] = (int)(1000 * ((2 * i + 1) * 1.0f / (2 * (i + 1))));
            }
        }
        //for (int i = 0; i < proportion.Length; i++)
        //{
        //    Debug.Log(proportion[i]);
        //}
        return ControlRandomProportion(proportion);
    }

}
