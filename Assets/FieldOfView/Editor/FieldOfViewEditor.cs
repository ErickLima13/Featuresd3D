using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.blue;
        Handles.DrawWireArc(fow.transform.position, Vector3.up,
            Vector3.forward,360, fow.viewRadius);

        Handles.color = Color.red;

        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2);

        Handles.DrawLine(fow.transform.position,
            fow.transform.position + viewAngleA * fow.viewRadius);

        Handles.DrawLine(fow.transform.position,
          fow.transform.position + viewAngleB * fow.viewRadius);

    }
}
