using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attach : MonoBehaviour
{

    public GameObject wandFirepoint;
    public GameObject wand;
    // Start is called before the first frame update
    void Start()
    {
        wandFirepoint.transform.parent = wand.transform;
    }

    // Update is called once per frame
    void Update()
    {
        wandFirepoint.transform.localPosition = wand.GetComponent<PosRef>().GetNextPosition();
    }
}

public class PosRef
{
    public Transform[] positions;
    private int index = 0;

    public Vector3 GetNextPosition()
    {
        Vector3 result = positions[index].localPosition;
        index += 1;
        return result;
    }

}
