using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public GameObject secondHand;
    public GameObject minuteHand;
    public GameObject hourHand;

    void Update()
    {
        secondHand.transform.eulerAngles = new Vector3(0, 0, -int.Parse(System.DateTime.Now.ToString("ss")) * 6f);
        minuteHand.transform.eulerAngles = new Vector3(0, 0, -int.Parse(System.DateTime.Now.ToString("mm")) * 6f);
        hourHand.transform.eulerAngles = new Vector3(0, 0, -int.Parse(System.DateTime.Now.ToString("hh")) * 30f);
    }

}
