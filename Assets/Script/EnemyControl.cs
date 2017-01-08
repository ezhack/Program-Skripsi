using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {
	public GameObject target;
	public float attackTimer;
	public float coolDown;
	// Use this for initialization
	void Start () {
		attackTimer = 0;
		coolDown = 2.0f;
	}

	public Animator animator;
	// Update is called once per frame
	void Update () {
		if (attackTimer > 0)
			attackTimer -= Time.deltaTime;

		if (attackTimer < 0)
			attackTimer = 0;

		if (attackTimer == 0) {
			animator.SetTrigger ("Attack1Trigger");
			StartCoroutine (COStunPause (1.2f));
			Attack ();
			attackTimer = coolDown;
		}
	}

	public IEnumerator COStunPause(float pauseTime)
	{
		yield return new WaitForSeconds(pauseTime);
	}

	private void Attack() {
		float distance = Vector3.Distance (target.transform.position, transform.position);

		//Menentukan arah serangan
		Vector3 dir = (target.transform.position - transform.position).normalized;
		float direction = Vector3.Dot (dir, transform.forward);

		Debug.Log (direction);

		if (distance < 2.2f) {
			if (direction > 0) {
				PlayerStatusBar eh = (PlayerStatusBar)target.GetComponent ("PlayerStatusBar");
				eh.AddjustCurrentHealth (-10);
			}
		}
	}
}
