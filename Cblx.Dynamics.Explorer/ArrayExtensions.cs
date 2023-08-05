﻿namespace Cblx.Dynamics.Explorer;

public static class ArrayExtensions
{
    public static T? Find<T>(this T[] array, Predicate<T> predicate)
    {
        return Array.Find(array, predicate);
    }
}
