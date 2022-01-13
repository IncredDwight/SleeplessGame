using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveHelper 
{
    public class ListSave
    {
        public static void SaveList(List<GameObject> savingList, GameObject[] keyArray, string listName)
        {
            if (savingList.Count <= 0)
            {
                PlayerPrefs.SetInt(listName + "Length", savingList.Count);
            }
            else
            {
                for (int i = 0; i < savingList.Count; i++)
                {
                    PlayerPrefs.SetInt(listName + i, System.Array.IndexOf(keyArray, savingList[i]));
                    PlayerPrefs.SetInt(listName + "Length", savingList.Count);
                }
            }
        }

        public static void LoadList(List<GameObject> loadingList, GameObject[] keyArray, string listName)
        {
            loadingList.Clear();
            if (PlayerPrefs.HasKey(listName + "Length"))
            {
                for (int i = 0; i < PlayerPrefs.GetInt(listName + "Length"); i++)
                {
                    loadingList.Add(keyArray[PlayerPrefs.GetInt(listName + i)]);
                }
            }
            else
                Debug.Log("No Save Found");
        }
    }

    public class ObjectDetection
    {
        public static bool IsObjectInsideCamera(Camera camera, BoxCollider2D collider)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            if (GeometryUtility.TestPlanesAABB(planes, collider.bounds))
                return true;
            else
                return false;

        }
    }
    
}
