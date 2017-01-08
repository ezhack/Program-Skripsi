using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Backpropagation : MonoBehaviour {
	private float[] X = new float[7];
	private float[] Xi = new float[7];
	private float[,] Vij = new float[3,7];
	private float[,] Voj = new float[1,3];
	private float[] Wjk = new float[4];
	private float[] DWjk = new float[4];
	private float[] Zinj = new float[3];
	private float[] Zj = new float[3];
	private float[] A_inj = new float[3];
	private float[] A = new float[3];
	private float[,] DVoj = new float[1,3];
	private float[,] DVij = new float[3,7];
	private float Yink, Yk, Ak, Tk, Eror, Te, Lr,ErorA;
	private int iterasi;

	private int minHealth = 0;
	private int maxHealth = 100;
	private int minJarak = 0;
	private int maxJarak = 300;

	public Text TIterasi,TError,TKeputusan,TWarior,TArcher,TAssassin;
	public InputField HPwarrior,HPAssassin,HParcher,HPplayer,RANGEwarrior,RANGEassassin,RANGEarcher;

	public static List<DatasetList> Dataset = new List<DatasetList> ();
	List<string> LTk = new List<string>() { "0.8","0.25","0.42","0.58","0.75","0.92",};
	public Dropdown ITk;

	// Use this for initialization
	void Start () {
		Tk = 0.16f;
		ITk.AddOptions (LTk);
		Eror = 1;
		iterasi = 0;
		random ();
		input ();
		Load ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Load() {
		if(File.Exists("DataTraining.ds")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open("DataTraining.ds", FileMode.Open);
			Dataset = (List<DatasetList>)bf.Deserialize(file);
			file.Close();
		}
	}

	public void GetLr(string LrValue) {
		Lr = (float)double.Parse(LrValue,System.Globalization.NumberStyles.AllowDecimalPoint);
	}

	public void GetTe(string TeValue) {
		Te = (float)double.Parse(TeValue,System.Globalization.NumberStyles.AllowDecimalPoint);
	}

	public void GetX1(string TeValue) {
		X[0] = (float)double.Parse(TeValue,System.Globalization.NumberStyles.AllowDecimalPoint);
		Xi [0] = X [0] / maxHealth;
	}

	public void GetX2(string TeValue) {
		X[1] = (float)double.Parse(TeValue,System.Globalization.NumberStyles.AllowDecimalPoint);
		Xi [1] = X [1] / maxHealth;
	}

	public void GetX3(string TeValue) {
		X[2] = (float)double.Parse(TeValue,System.Globalization.NumberStyles.AllowDecimalPoint);
		Xi [2] = X [2] / maxHealth;
	}

	public void GetX4(string TeValue) {
		X[3] = (float)double.Parse(TeValue,System.Globalization.NumberStyles.AllowDecimalPoint);
		Xi [3] = X [3] / maxHealth;
	}

	public void GetX5(string TeValue) {
		X[4] = (float)double.Parse(TeValue,System.Globalization.NumberStyles.AllowDecimalPoint);
		Xi [4] = X [4] / maxHealth;
	}

	public void GetX6(string TeValue) {
		X[5] = (float)double.Parse(TeValue,System.Globalization.NumberStyles.AllowDecimalPoint);
		Xi [5] = X [5] / maxHealth;
	}

	public void GetX7(string TeValue) {
		X[6] = (float)double.Parse(TeValue,System.Globalization.NumberStyles.AllowDecimalPoint);
		Xi [6] = X [6] / maxHealth;
	}

	public void GetTk(int index) {
		Tk = float.Parse(LTk [index]);
	}

	public void SubmitTraining(){
		Backpro ();
		result ();
		ErorA = Eror;
		Eror = 1;
		iterasi = 0;
	}

	public void SubmitReset(){
		random ();
		input ();
	}

	public void SubmitExit() {
		Application.LoadLevel (0);
	}


	public void SubmitSimpan() {
		Dataset.Add (new DatasetList (
			Xi[0],Xi[1],Xi[2],Xi[3],Xi[4],Xi[5],Xi[6],
			Wjk[0],Wjk[1],Wjk[2],Wjk[3],
			Voj[0,0],Voj[0,1],Voj[0,2],
			Vij[0,0],Vij[0,1],Vij[0,2],Vij[0,3],Vij[0,4],Vij[0,5],Vij[0,6],
			Vij[1,0],Vij[1,1],Vij[1,2],Vij[1,3],Vij[1,4],Vij[1,5],Vij[1,6],
			Vij[2,0],Vij[2,1],Vij[2,2],Vij[2,3],Vij[2,4],Vij[2,5],Vij[2,6],
			ErorA,Yk));
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create ("DataTraining.ds");
		bf.Serialize (file, Dataset);
		file.Close ();
	}

	public void result() {
		TIterasi.text = ""+iterasi;
		TError.text = "" + Eror;
		TKeputusan.text = "" + Yk;
		if (Yk >= 0.84 && Yk <= 1){
			TWarior.text = "Hold";
			TAssassin.text = "Hold";
			TArcher.text = "Attack";
		} else if (Yk >= 0.67 && Yk <= 0.83){
			TWarior.text = "Hold";
			TAssassin.text = "Attack";
			TArcher.text = "Hold";
		} else if (Yk >= 0.51 && Yk <= 0.66){
			TWarior.text = "Attack";
			TAssassin.text = "Hold";
			TArcher.text = "Hold";
		} else if (Yk >= 0.34 && Yk <= 0.5){
			TWarior.text = "Attack";
			TAssassin.text = "Attack";
			TArcher.text = "Hold";
		} else if (Yk >= 0.17 && Yk <= 0.33){
			TWarior.text = "Escape";
			TAssassin.text = "Escape";
			TArcher.text = "Attack";
		} else if (Yk >= 0 && Yk <= 0.16){
			TWarior.text = "Escape";
			TAssassin.text = "Escape";
			TArcher.text = "Escape";
		}
	}

	public void input() {
		for (int i = 0; i <= 3; i++) {
			X[i] = Random.Range(minHealth, maxHealth);
			Xi [i] = X [i] / maxHealth;
			Debug.Log ("Input " + i + " = " + Xi [i]);
		}
		for (int i = 4; i <= 6; i++) {
			X[i] = Random.Range(minJarak, maxJarak);
			Xi [i] = X [i] / maxJarak;
			Debug.Log ("Input " + i + " = " + Xi [i]);
		}
		HPwarrior.text = ""+X[0];
		HPAssassin.text = ""+X[1];
		HParcher.text = ""+X[2];
		HPplayer.text = ""+X[3];
		RANGEwarrior.text = ""+X[4];
		RANGEassassin.text = ""+X[5];
		RANGEarcher.text = ""+X[6];
	}

	public void random() {
		for(int i=0; i<=2; i++) {
			for(int j=0; j <= 6; j++) {
				Vij[i,j] = (float)(Random.Range(0.0f, 1.0f));
				//Debug.Log ("V"+i+j+" = "+Vij[i,j]);
			}
		}

		for(int o=0; o<1; o++) {
			for(int j=0; j <= 2; j++) {
				Voj[o,j] = (float)(Random.Range(0.0f, 1.0f));
				//Debug.Log ("V"+o+j+" = "+Voj[o,j]);
			}
		}

		for(int j=0; j <= 3; j++) {
			Wjk[j] = (float)(Random.Range(0.0f, 1.0f));
			//Debug.Log ("W"+j+" = "+Wjk[j]);
		}
	}

	public void Backpro() {
		while(Eror > Te){
			iterasi++;
			//Langkah 4
			for (int i = 0; i <= 2; i++) {
				Zinj[i] = Voj[0,i]+Xi[0]*Vij[i,0]+Xi[1]*Vij[i,1]+Xi[2]*Vij[i,2]+Xi[3]*Vij[i,3]+Xi[4]*Vij[i,4]+Xi[5]*Vij[i,5]+Xi[6]*Vij[i,6];
			}

			for (int i = 0; i <= 2; i++) {
				Zj [i] = 1 / (1 + Mathf.Exp (-Zinj [i]));
			}

			Yink = Zj [0] * Wjk [0] + Zj [1] * Wjk [1] + Zj [2] * Wjk [2] + Wjk [3];

			Yk = 1 / (1 + Mathf.Exp (-Yink));

			Eror = Tk * ((Tk - Yk) * (Tk - Yk));
			//Langkah 5
			Ak = (Tk-Yk)*Yk*(1-Yk);

			for(int i=0; i <= 2; i++) {
				DWjk [i] = Lr * Ak * Zj [i];
			}
			DWjk [3] = Lr * Ak;

			//Langkah 6
			for (int i = 0; i <= 2; i++) {
				A_inj [i] = Ak * DWjk [i];
			}

			for (int i = 0; i <= 2; i++) {
				A [i] = A_inj [i] * Zj [i] * (1 - Zj [i]);
			}

			for(int o=0; o<1; o++) {
				for (int j = 0; j <= 2; j++) {
					DVoj [o,j] = Lr * A [j];
				}
			}

			for (int i = 0; i <= 2; i++) {
				for(int j = 0; j<=6; j++) {
					DVij [i, j] = Lr * A [i] * Xi [j];
				}
			}

			//Langkah 7
			for (int i = 0; i <= 3; i++) {
				Wjk [i] = Wjk [i] + DWjk [i];
			}

			for(int o=0; o<1; o++) {
				for (int j = 0; j <= 2; j++) {
					Voj [0, j] = DVoj [o, j] + Voj [o, j];
				}
			}

			for (int i = 0; i <= 2; i++) {
				for (int j = 0; j <= 6; j++) {
					Vij [i, j] = DVij [i, j] + Vij [i, j];
				}
			}
		}
	}
		
}