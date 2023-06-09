using System.Collections.Generic;
using System.Linq;
using Meta.Interface;
using UnityEngine;

namespace Meta.Save.Databases
{
    public static class DatabaseID
    {
        public static void Set<T>(T[] inFolder) where T : ScriptableObject, IDatabaseItem
        {
            var taken = inFolder.Select(t => t.DatabaseID).ToHashSet();
            foreach (var so in inFolder)
            {
                if (so.DatabaseID == 0)
                {
                    var id = GetNewId(taken);
                    so.SET_DATABASE_ID(id);
                }
            }
        }

        static int GetNewId(ICollection<int> taken)
        {
            var max = taken.Max();
            // for (var i = 0; i < max; i++)
            // {
            //     if (taken.Contains(i)) continue;
            //     taken.Add(i);
            //     return i;
            // }

            var newId = max + 1;
            taken.Add(newId);
            return newId;
        }
    }
}