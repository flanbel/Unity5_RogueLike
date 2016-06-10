using UnityEngine;
using System.Collections;
/// <summary>
/// シーン切り替え(糞)
/// </summary>
public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DungeonScene() 
    {
        // Application.LoadLevel([シーンIDまたはシーン名]);
        UnityEngine.SceneManagement.SceneManager.LoadScene("_Dungeon");
    }
}
