using UnityEngine;
using System.Collections;

public class PulseImage : MonoBehaviour {

    public float pulseTime;
    public float finishScale;
    private float speedScale;
    private float startScale;
    private float time = 0;
    private bool increase = true;
	void Start () {
        startScale = transform.localScale.x;
        speedScale = ((finishScale - startScale) / (pulseTime / Time.deltaTime)) /2.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (increase == true)
        {
            if (time < pulseTime)
            {
                transform.localScale = new Vector3(transform.localScale.x + speedScale, transform.localScale.y + speedScale, 0);
            }
            else
            {
                increase = !increase;
                time = 0;
            }
        }
        else
        {
            if (time < pulseTime)
            {
                transform.localScale = new Vector3(transform.localScale.x - speedScale, transform.localScale.y - speedScale, 0);
            }
            else
            {
                increase = !increase;
                time = 0;
            }
        }
        time += Time.deltaTime;
	}
}
