using UnityEngine;
using UnityEditor;

//This is for MapScene's Cannon control

[CustomEditor(typeof(BookShelf))]
public class BookshelfEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var bookshelf = (BookShelf)target;
        if (bookshelf == null) return;

        if (GUILayout.Button("Mess Up")) {
            bookshelf.MessUp();
        }
        if (GUILayout.Button("Clean Up")) {
            bookshelf.CleanUp();
        }
    }
}
