using UnityEngine;

namespace MaroonSeal.Maths {
    public static class Interpolation {

        #region Lerp
        static public float LerpTime(float _a, float _b, float _t) {
            return Mathf.Lerp(_a, _b, _t);
        }

        static public float LerpTimeWrapped(float _a, float _b, float _t) {
            float startAngle = _a / 360.0f;
            float endAngle = _b / 360.0f;

            float step =  Mathf.LerpAngle(startAngle, endAngle, _t) / 360.0f;
            return Mathf.Repeat(step, 1.0f);
        }
        #endregion

        #region Move Towards
        static public float MoveTimeTowards(float _current, float _target, float _delta) {
            return Mathf.MoveTowards(_current, _target, _delta);
        }

        static public float MoveTimeTowardsWrapped(float _current, float _target, float _delta) {
            return Mathf.MoveTowardsAngle(_current * 360.0f, _target * 360.0f, _delta * 360.0f) / 360.0f;
        }
        #endregion

        #region Delta
        static public float GetTimeDelta(float _from, float _to) { 
            return _to - _from; 
        }

        static public float GetTimeDeltaWrapped(float _from, float _to) {  
            return Mathf.DeltaAngle(_from * 360.0f, _to * 360.0f) / 360.0f; 
        }
        #endregion
    }
}