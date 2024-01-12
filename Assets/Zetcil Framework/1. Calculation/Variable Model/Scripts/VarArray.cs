using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetcil
{
    public static class VarArray
    {
        // Start is called before the first frame update
        public static List<T> ToList<T>(this T[] array) where T : class
        {
            List<T> output = new List<T>();
            output.AddRange(array);
            return output;
        }

    }
}
