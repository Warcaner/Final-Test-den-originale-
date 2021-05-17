using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public List<GameObject> objects;
    public void cast(string objectName)
    {
        foreach (var item in objects)
        {

            item.SetActive(objectName == item.name);
        }


    }

}
