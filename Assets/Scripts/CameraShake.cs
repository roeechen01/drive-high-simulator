using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

	float shakeDuration;
	float shakeAmount = 0.5f;
	float decreaseFactor = 1.0f;

	Quaternion originalRot;

	void Start()
	{
		shakeDuration = 0f;
	}

	public void Shake(float shakeAmount, float shakeDuration)
    {
		originalRot = this.transform.localRotation;
		this.shakeAmount = shakeAmount;
		this.shakeDuration = shakeDuration;
    }

	void Update()
	{
		if (shakeDuration > 0)
		{
			this.transform.localRotation = Quaternion.Euler(originalRot.eulerAngles.x + Random.Range(-1f, 1f) * shakeAmount, originalRot.eulerAngles.y + Random.Range(-1f, 1f) * shakeAmount, originalRot.eulerAngles.z + Random.Range(-1f, 1f) * shakeAmount);
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
			shakeDuration = 0f;

	}
}