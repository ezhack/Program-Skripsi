﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Targeting : MonoBehaviour {
	public List<Transform> targets;
	public Transform selectedTarget;

	private Transform myTransform;
	// Use this for initialization
	void Start () {
		targets = new List<Transform> ();
		selectedTarget = null;
		myTransform = transform;

		AddAllEnemies ();
	}

	public void AddAllEnemies() {
		GameObject[] go = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (GameObject enemy in go)
			AddTarget (enemy.transform);
	}

	public void AddTarget(Transform enemy) {
		targets.Add (enemy);
	}

	private void SortTargetByDistance() {
		targets.Sort(delegate(Transform t1, Transform t2) {
			return(Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position)));
		});
	}

	private void TargetEnemy() {
		if(selectedTarget == null) {
			SortTargetByDistance ();
			selectedTarget = targets[0];
		} else {
			int index = targets.IndexOf(selectedTarget);
			if(index < targets.Count - 1) {
				index++;
			} else {
				index = 0;
			}
			DeselectTarget ();
			selectedTarget = targets[index];
		}
		SelectTarget();
	}

	private void SelectTarget() {
		//selectedTarget.GetComponent<Shader>() = Color.red;

		PlayerControl pa = (PlayerControl)GetComponent ("PlayerControl");
		pa.target = selectedTarget.gameObject;
	}

	private void DeselectTarget() {
		//selectedTarget.GetComponent<Shader>() = Color.blue;
		selectedTarget = null;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			TargetEnemy ();
		}
	}
}
