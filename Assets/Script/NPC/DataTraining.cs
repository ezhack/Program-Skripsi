using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataTraining : MonoBehaviour {

	public Text x1, x2, x3, x4, x5, x6, x7, keputusan, eror;
	public static List<DatasetList> Dataset = new List<DatasetList> ();
	// Use this for initialization
	void Start () {
		Load ();
		foreach (DatasetList call in Dataset) {
			x1.text += "" + call.x1 + "\n";
			x2.text += "" + call.x2 + "\n";
			x3.text += "" + call.x3 + "\n";
			x4.text += "" + call.x4 + "\n";
			x5.text += "" + call.x5 + "\n";
			x6.text += "" + call.x6 + "\n";
			x7.text += "" + call.x7 + "\n";
			keputusan.text += "" + call.Keputusan + "\n";
			eror.text += "" + call.Error + "\n";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClearData() {
		Dataset.Clear();
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create ("DataTraining.ds");
		bf.Serialize (file, Dataset);
		file.Close ();
	}

	public void SubmitExit() {
		Application.LoadLevel (0);
	}

	public void Load() {
		if(File.Exists("DataTraining.ds")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open("DataTraining.ds", FileMode.Open);
			Dataset = (List<DatasetList>)bf.Deserialize(file);
			file.Close();
		}
	}
}
