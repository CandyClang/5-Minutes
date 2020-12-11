using UnityEngine;
using UnityEditor;

//This is for JinScene's Debug Rigid Sphere control

[CustomEditor(typeof(DebugRigidSphere))]
public class DebugRigidSphereEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var debugRigidSphere = (DebugRigidSphere)target;
        if (debugRigidSphere == null) return;

        if (GUILayout.Button("Go Front")) {
            debugRigidSphere.GoFront();
        }
        if (GUILayout.Button("Go Back")) {
            debugRigidSphere.GoBack();
        }
        if (GUILayout.Button("Go Left")) {
            debugRigidSphere.GoLeft();
        }
        if (GUILayout.Button("Go Right")) {
            debugRigidSphere.GoRight();
        }
        if (GUILayout.Button("Reset to Default")) {
            debugRigidSphere.ResetToIdentity();
        }
    }
}
