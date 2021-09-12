using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public enum RollState
    {
        None,
        SpeedUp,
        SpeedDown,
        End
    }

    RollState curState;
    float allTime = 0;//spin time
    float endAngle;//final angle
    //--------------------accelerate stage---------------------------------
    [SerializeField]
    float MaxSpeed = 500;//max sped
    float factor;//speed factor
    [SerializeField]
    float accelerateTime = 1;//accelerate time
    [SerializeField]
    float speedUpTime = 3;//accelerate max time

    //-------------------- deceleration  stage---------------------------------
    float tempAngle;
    float speedUpTempAngle;
    float speedUpEndAngle = 0;
    [SerializeField]
    float deceleration = 2f; // deceleration factor 

    // delegate
    public delegate void onSuccess();
    private onSuccess m_onSuccess;

    private void Update()
    {
        if (curState == RollState.None)
        {
            return;
        }
        allTime += Time.deltaTime;
        if (curState == RollState.SpeedUp)
        {
            factor = allTime / accelerateTime;
            factor = factor > 1 ? 1 : factor;
            transform.Rotate(new Vector3(0, 0, -1) * factor * MaxSpeed * Time.deltaTime, Space.Self);
        }
        if (allTime >= speedUpTime && curState == RollState.SpeedUp)
        {
            curState = RollState.SpeedDown;
            tempAngle = GetTempAngle();
            speedUpTempAngle = transform.eulerAngles.z;
            speedUpEndAngle = Mathf.Lerp(tempAngle, endAngle, Time.deltaTime * deceleration);
  }
        if (curState == RollState.SpeedDown)
        {
            if (Mathf.Abs(speedUpEndAngle - speedUpTempAngle) <= 1)
            {
                tempAngle = Mathf.Lerp(tempAngle, endAngle, Time.deltaTime * deceleration);
                transform.rotation = Quaternion.Euler(0, 0, tempAngle);
                if (Mathf.Abs(tempAngle - endAngle) <= 1)
                {
                    curState = RollState.None;
                    if (m_onSuccess != null)
                    {
                        m_onSuccess();
                    }
                }
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, -1) * factor * MaxSpeed * Time.deltaTime, Space.Self);
                speedUpTempAngle = transform.eulerAngles.z;
            }
        }
    }

    private float GetTempAngle()
    {
        return (360 - transform.eulerAngles.z) % 360;
    }
    #region public
    public void SetSpin(bool _active)
    {
        curState = RollState.SpeedUp;
    }

    public void SetStopAngle(float _angle)
    {
        endAngle = _angle;
    }

    public void SetOnSuccessDelegate(onSuccess _fun)
    {
        m_onSuccess = _fun;
    }
    #endregion
}
