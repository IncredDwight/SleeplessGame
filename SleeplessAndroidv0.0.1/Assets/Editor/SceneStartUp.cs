using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class SceneStartUp : MonoBehaviour
{
    static SceneStartUp()
    {
        EditorSceneManager.newSceneCreated += SceneCreating;
    }

    public static void SceneCreating(Scene scene, NewSceneSetup setup, NewSceneMode mode)
    {
        GameObject[] references = new GameObject[8];
        references[0] = Resources.Load<GameObject>("Prefabs/UI");
        references[1] = Resources.Load<GameObject>("Prefabs/Core");
        references[2] = Resources.Load<GameObject>("Prefabs/Player");
        references[3] = Resources.Load<GameObject>("Prefabs/Particles");
        references[4] = Resources.Load<GameObject>("Prefabs/Projectiles");
        references[5] = Resources.Load<GameObject>("Prefabs/Enemies");
        references[6] = Resources.Load<GameObject>("Prefabs/Decorations");
        references[7] = Resources.Load<GameObject>("Prefabs/Background/Grid");
        foreach (GameObject reference in references)
        {
            GameObject final = Instantiate(reference);
            final.name = reference.name;
        }

    }
}
