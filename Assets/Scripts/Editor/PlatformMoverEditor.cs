using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlatformMover))]
public class NewBehaviourScript : Editor {

    PlatformMover m_MovingPlatform;

    private void OnEnable()
    {
        m_MovingPlatform = target as PlatformMover;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        m_MovingPlatform.platform = EditorGUILayout.ObjectField("Platform", m_MovingPlatform.platform, typeof(GameObject), true) as GameObject;
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed Platform");
        }

        EditorGUI.BeginChangeCheck();
        float newMoveSpeed = EditorGUILayout.Slider("Move Speed", m_MovingPlatform.moveSpeed, 0.0f, 15.0f);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed Move Speed");
            m_MovingPlatform.moveSpeed = newMoveSpeed;
        }

        EditorGUI.BeginChangeCheck();
        bool newLoop = EditorGUILayout.Toggle("Is looping", m_MovingPlatform.loop);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed Looping property");
            m_MovingPlatform.loop = newLoop;
        }

        
        if(m_MovingPlatform.myWaypoints.Length != m_MovingPlatform.waitAtWaypointTime.Length)
        {
            for(int i = 0; i < m_MovingPlatform.myWaypoints.Length; i++)
            {
                ArrayUtility.Add(ref m_MovingPlatform.waitAtWaypointTime, 1.0f);
            }
        }

        int delete = -1;
        EditorGUILayout.LabelField("Waypoints:");
        for (int i = 0; i < m_MovingPlatform.myWaypoints.Length; i++)
        {
            EditorGUI.BeginChangeCheck();

            Vector3 newPosition = EditorGUILayout.Vector3Field("Waypoint " + i, m_MovingPlatform.myWaypoints[i]);
            EditorGUILayout.BeginHorizontal();
            float newWaitAtWaypointTime = EditorGUILayout.Slider("Wait", m_MovingPlatform.waitAtWaypointTime[i], 0.0f, 10.0f);
            if (i != 0 && GUILayout.Button("Del", GUILayout.Width(40)))
            {
                delete = i;
            }
            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "changed waypoints");
                m_MovingPlatform.waitAtWaypointTime[i] = newWaitAtWaypointTime;
                m_MovingPlatform.myWaypoints[i] = newPosition;
            }
        }
        if (delete != -1)
        {
            Undo.RecordObject(target, "Removed point moving platform");

            ArrayUtility.RemoveAt(ref m_MovingPlatform.myWaypoints, delete);
            ArrayUtility.RemoveAt(ref m_MovingPlatform.waitAtWaypointTime, delete);
        }

        if (GUILayout.Button("Add Waypoint"))
        {
            Undo.RecordObject(target, "Added Waypoint");

            Vector3 position = m_MovingPlatform.myWaypoints[m_MovingPlatform.myWaypoints.Length - 1] + Vector3.right;

            ArrayUtility.Add(ref m_MovingPlatform.myWaypoints, position);
            ArrayUtility.Add(ref m_MovingPlatform.waitAtWaypointTime, 1.0f);
        }
    }

    public void OnSceneGUI()
    {
        for(int i = 0; i < m_MovingPlatform.myWaypoints.Length; i++)
        {
            Vector3 worldPosition;
            worldPosition = m_MovingPlatform.myWaypoints[i];

            Vector3 newWorld = worldPosition;
            //if (i != 0)
                newWorld = Handles.PositionHandle(worldPosition, Quaternion.identity);
            Handles.color = Color.green;
            if(i == 0)
                Handles.DrawDottedLine(m_MovingPlatform.transform.position, m_MovingPlatform.myWaypoints[0], 5);
            else
            {
                Handles.DrawDottedLine(worldPosition, m_MovingPlatform.myWaypoints[i - 1], 5);
            }
            if (worldPosition != newWorld)
            {
                Undo.RecordObject(target, "moved point");
                m_MovingPlatform.myWaypoints[i] = newWorld;
            }
            
        }
    }
}
