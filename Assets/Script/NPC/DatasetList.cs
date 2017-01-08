using UnityEngine;
using System.Collections;

[System.Serializable]
public class DatasetList {
	public float x1,x2,x3,x4,x5,x6,x7;
	public float W0,W1,W2,W3;
	public float Vo0,Vo1,Vo2;
	public float V00,V01,V02,V03,V04,V05,V06,V10,V11,V12,V13,V14,V15,V16,V20,V21,V22,V23,V24,V25,V26;
	public float Error, Keputusan;
	public DatasetList(float newx1, float newx2, float newx3, float newx4, float newx5, float newx6,  float newx7,
		float newW0, float newW1, float newW2, float newW3,
		float newVo0, float newVo1, float newVo2,
		float newV00,float newV01,float newV02,float newV03,float newV04,float newV05,float newV06,
		float newV10,float newV11,float newV12,float newV13,float newV14,float newV15,float newV16,
		float newV20,float newV21,float newV22,float newV23,float newV24,float newV25,float newV26,
		float newError, float newKeputusan) {
		x1 = newx1;
		x2 = newx2;
		x3 = newx3;
		x4 = newx4;
		x5 = newx5;
		x6 = newx6;
		x7 = newx7;

		W0 = newW0;
		W1 = newW1;
		W2 = newW2;
		W3 = newW3;

		Vo0 = newVo0;
		Vo1 = newVo1;
		Vo2 = newVo2;

		V00 = newV00;
		V01 = newV01;
		V02 = newV02;
		V03 = newV03;
		V04 = newV04;
		V05 = newV05;
		V06 = newV06;

		V10 = newV10;
		V11 = newV11;
		V12 = newV12;
		V13 = newV13;
		V14 = newV14;
		V15 = newV15;
		V16 = newV16;

		V20 = newV20;
		V21 = newV21;
		V22 = newV22;
		V23 = newV23;
		V24 = newV24;
		V25 = newV25;
		V26 = newV26;

		Error = newError;
		Keputusan = newKeputusan;
	}
}
