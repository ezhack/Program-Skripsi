using UnityEngine;
using System.Collections;

public class PlayerStatusBar : MonoBehaviour {
	public int MaxHealth = 100;
	public int CurHealth = 100;
	public float healthBarLength;
	// Use this for initialization
	void Start () {
		healthBarLength = Screen.width / 2;
	}
	
	// Update is called once per frame
	void Update () {
		AddjustCurrentHealth(0);
	}

	void OnGUI () {
		GUI.Box(new Rect(10, 10, healthBarLength, 20), CurHealth +"/"+ MaxHealth);
	}

	public void AddjustCurrentHealth(int adj) {
		CurHealth += adj;

		if(CurHealth < 0) 
			CurHealth = 0;

		if (CurHealth > MaxHealth)
			CurHealth = MaxHealth;

		if (MaxHealth < 1)
			MaxHealth = 1;

		healthBarLength = (Screen.width / 3) * (CurHealth / (float)MaxHealth);
	}
}
