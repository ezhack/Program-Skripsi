using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		
	}

	public void MenuTraining() {
		Application.LoadLevel (1);
	}

	public void MenuDataTraining() {
		Application.LoadLevel (2);
	}

	public void MenuUjiCoba() {
		Application.LoadLevel (3);
	}

	public void MenuExit() {
		Application.Quit ();
	}

	void DisplayMenu() {
		if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height/2 - 70, 100, 30), "Pelatihan")) {
			Application.LoadLevel (1);
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height/2 - 30, 100, 30), "Pengujian")) {
			Application.LoadLevel (2);
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height/2 + 10, 100, 30), "Keluar")) {
			Application.Quit();
		}
	}
}
