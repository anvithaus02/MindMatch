using System.Collections.Generic;
using UnityEngine;

namespace MindMatch.Core
{
    public static class Utility
    {
        public static List<T> ShuffleList<T>(List<T> list)
        {
            List<T> shuffled = new List<T>(list);
            for (int i = 0; i < shuffled.Count; i++)
            {
                int rand = Random.Range(i, shuffled.Count);
                (shuffled[i], shuffled[rand]) = (shuffled[rand], shuffled[i]);
            }
            return shuffled;
        }
    }
}