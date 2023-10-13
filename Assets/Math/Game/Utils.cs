using System;
using System.Collections.Generic;

using UnityEngine;

public static class Utils
{
    public static T LastElement<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            throw new InvalidOperationException("The list is empty.");
        }

        return list[list.Count - 1];
    }
}
