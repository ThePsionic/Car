using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizePath : MonoBehaviour {

    public Color LineColor = Color.green;
    public List<Transform> nodePath = new List<Transform>();

    void OnDrawGizmos()
    {
        Gizmos.color = LineColor;

        Transform[] GameNodes = GetComponentsInChildren<Transform>();
        nodePath = new List<Transform>();

        foreach (Transform myTransform in GameNodes)
        {
            if (myTransform != transform)
            {
                nodePath.Add(myTransform);
            }
        }
        Vector3 fPoint = nodePath[nodePath.Count - 1].position;
        //Vector3 sPoint = nodePath[0].position;

        foreach (Transform myTransform in nodePath)
        {
            Vector3 sPoint = myTransform.position;
            Gizmos.DrawLine(fPoint, sPoint);
            Gizmos.DrawWireSphere(sPoint,0.3f);

            fPoint = sPoint;
        }
    }
}
