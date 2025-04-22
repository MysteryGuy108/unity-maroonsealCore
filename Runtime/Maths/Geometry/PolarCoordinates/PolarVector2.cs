using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Maths {
    public struct PolarVector2
    {
        public float theta;
        readonly public float degrees { get { return theta * Mathf.Rad2Deg; } }
        public float radius;

        public PolarVector2(float _theta, float _radius) {
            theta = Mathf.Repeat(_theta, Mathf.PI * 2.0f);
            radius = Mathf.Abs(_radius);
        }

        public PolarVector2(Vector2 _position) {
            theta = Mathf.Atan2(_position.y, _position.x);
            if (theta < 0.0f) { theta += Mathf.PI * 2.0f; }
            radius = _position.magnitude;
        }

        readonly public Vector2 GetVector2() {
            return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)) * radius;
        }

        readonly public float GetArcLength() { return theta * radius; }

        #region Operators
        static public PolarVector2 operator +(PolarVector2 _a, PolarVector2 _b) {
            return new PolarVector2(_a.theta + _b.theta, _a.radius + _b.radius);
        }

        static public PolarVector2 operator -(PolarVector2 _a, PolarVector2 _b) {
            return new PolarVector2(_a.theta - _b.theta, _a.radius - _b.radius);
        }
        #endregion

        #region Distances
        static public float CartesianDistance(PolarVector2 _a, PolarVector2 _b) {
            return Vector2.Distance(_a.GetVector2(), _b.GetVector2());
        }

        static public float LinearRadialDistance(PolarVector2 _a, PolarVector2 _b) {
            return _a.radius + _b.radius;
        }

        static public float PolarDistance(PolarVector2 _a, PolarVector2 _b) {
            float angleDelta = Mathf.Abs(Mathf.DeltaAngle(_a.degrees, _b.degrees)) * Mathf.Deg2Rad;
            float radiusDelta = Mathf.Abs(_a.radius - _b.radius);
            return angleDelta + radiusDelta;
        }
        #endregion

        #region Move Towards
        static public PolarVector2 MoveTowardsTargetPolar(PolarVector2 _current, PolarVector2 _target, float _deltaTheta, float _deltaRadius, bool _angularStep) {
            float newTheta;
            if (_angularStep) { newTheta = Mathf.MoveTowardsAngle(_current.degrees, _target.degrees, _deltaTheta * Mathf.Rad2Deg) * Mathf.Deg2Rad; }
            else { newTheta = Mathf.MoveTowards(_current.theta, _target.theta, _deltaTheta); }

            float newRadius = Mathf.MoveTowards(_current.radius, _target.radius, _deltaRadius);

            return new PolarVector2(newTheta, newRadius);
        }

        static public PolarVector2 MoveTowardsTargetThroughOrigin(PolarVector2 _current, PolarVector2 _target, float _stepDelta) {
            if (_current.theta == _target.theta || _current.radius <= 0.0f) {
                return new PolarVector2(_target.theta, Mathf.MoveTowards(_current.radius, _target.radius, _stepDelta));
            }

            float newTheta = _current.theta;
            float newRadius = _current.radius - _stepDelta;
            if (newRadius <= 0.0f) { newTheta = _target.theta; }

            newRadius = Mathf.Abs(newRadius);

            return new PolarVector2(newTheta, newRadius);
        }
        #endregion

        #region Lerp
        static public PolarVector2 Lerp(PolarVector2 _a, PolarVector2 _b, float _t, bool _angularLerp) {
            float lerpTheta;
            if (_angularLerp) { lerpTheta = Mathf.LerpAngle(_a.degrees, _b.degrees, _t) * Mathf.Deg2Rad; }
            else { lerpTheta = Mathf.Lerp(_a.theta, _b.theta, _t); }
            
            float lerpRadius = Mathf.Lerp(_a.radius, _b.radius, _t);

            return new PolarVector2(lerpTheta, lerpRadius);
        }

        static public float InverseLerp(PolarVector2 _a, PolarVector2 _b, PolarVector2 _t) {
            float inverseLerpTheta = Mathf.InverseLerp(_a.theta, _b.theta, _t.theta);
            float inverseLerpRadius = Mathf.InverseLerp(_a.radius, _b.radius, _t.radius);

            return (inverseLerpTheta + inverseLerpRadius) / 2.0f;
        }
        #endregion
    }
}

