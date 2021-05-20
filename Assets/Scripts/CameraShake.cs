using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

	// How long the object should shake for.
	public float shakeDuration;

	// Amplitude of the shake. A larger value shakes the camera harder.
	float shakeAmount = 0.5f;
	public float decreaseFactor = 1.0f;
	bool finished = true;

	Quaternion originalRot;

	void Start()
	{
		shakeDuration = 0f;
		//originalRot = this.transform.localRotation;
		//Invoke("SetDuration", 1f);
	}

	public void Shake(float shakeAmount)
    {
		originalRot = this.transform.localRotation;
		float duration = 0.2f;
		finished = false;
		this.shakeDuration = duration;
		Invoke("SetFinished", duration);
    }

	void SetDuration()
    {
		originalRot = transform.rotation;
		shakeDuration = 0.2f;
		Invoke("SetFinished", shakeDuration);
		finished = false;
    }

	void SetFinished()
    {
		finished = true;
    }

	void Update()
	{
		if (shakeDuration > 0)
		{
			//this.transform.localRotation = Quaternion.Euler(/*transform.localRotation.x*/  Random.insideUnitSphere.x * shakeAngle - originalRot.x * shakeAngle, Random.insideUnitSphere.y * shakeAngle - originalRot.y * shakeAngle,  Random.insideUnitSphere.z * shakeAngle - -originalRot.z * shakeAngle) ;
			this.transform.localRotation = Quaternion.Euler(originalRot.eulerAngles.x + Random.Range(-1f, 1f) * shakeAmount, originalRot.eulerAngles.y + Random.Range(-1f, 1f) * shakeAmount, originalRot.eulerAngles.z + Random.Range(-1f, 1f) * shakeAmount);
			//print(Random.insideUnitSphere.x * shakeAmount + "___" + originalRot.x);
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
            if (finished)
            {
				shakeDuration = 0f;
				this.transform.localRotation = originalRot;
				finished = false;
			}
			
		}
	}
}