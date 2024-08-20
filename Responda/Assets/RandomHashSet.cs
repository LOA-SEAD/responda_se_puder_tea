using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class RandomHashSet : MonoBehaviour
{

    public static HashSet<int> Gera(int min, int max) {

        int size = max - min;
        HashSet<int> set = new HashSet<int>();

        int valor;
        while (set.Count < size)
        {
            valor = min + UnityEngine.Random.Range(0, size);
            set.Add(valor);
        }

      
        return set;
    }



}
