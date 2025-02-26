using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Maths {
    public struct PolarCoordinate2D
    {
        public float theta;
        public float degrees { get { return theta * Mathf.Rad2Deg; } }
        public float radius;

        public PolarCoordinate2D(float _theta, float _radius) {
            theta = Mathf.Repeat(_theta, Mathf.PI * 2.0f);
            radius = Mathf.Abs(_radius);
        }

        public PolarCoordinate2D(Vector2 _position) {
            theta = Mathf.Atan2(_position.y, _position.x);
            if (theta < 0.0f) { theta += Mathf.PI * 2.0f; }
            radius = _position.magnitude;
        }

        readonly public Vector2 GetVector2() {
            return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)) * radius;
        }

        readonly public float GetArcLength() { return theta * radius; }

        #region Operators
        static public PolarCoordinate2D operator +(PolarCoordinate2D _a, PolarCoordinate2D _b) {
            return new PolarCoordinate2D(_a.theta + _b.theta, _a.radius + _b.radius);
        }

        static public PolarCoordinate2D operator -(PolarCoordinate2D _a, PolarCoordinate2D _b) {
            return new PolarCoordinate2D(_a.theta - _b.theta, _a.radius - _b.radius);
        }
        #endregion

        #region Distances
        static public float CartesianDistance(PolarCoordinate2D _a, PolarCoordinate2D _b) {
            return Vector2.Distance(_a.GetVector2(), _b.GetVector2());
        }

        static public float LinearRadialDistance(PolarCoordinate2D _a, PolarCoordinate2D _b) {
            return _a.radius + _b.radius;
        }

        static public float PolarDistance(PolarCoordinate2D _a, PolarCoordinate2D _b) {
            float angleDelta = Mathf.Abs(Mathf.DeltaAngle(_a.degrees, _b.degrees)) * Mathf.Deg2Rad;
            float radiusDelta = Mathf.Abs(_a.radius - _b.radius);
            return angleDelta + radiusDelta;
        }
        #endregion

        #region Move Towards
        static public PolarCoordinate2D MoveTowardsTargetPolar(PolarCoordinate2D _current, PolarCoordinate2D _target, float _deltaTheta, float _deltaRadius, bool _angularStep) {
            float newTheta;
            if (_angularStep) { newTheta = Mathf.MoveTowardsAngle(_current.degrees, _target.degrees, _deltaTheta * Mathf.Rad2Deg) * Mathf.Deg2Rad; }
            else { newTheta = Mathf.MoveTowards(_current.theta, _target.theta, _deltaTheta); }

            float newRadius = Mathf.MoveTowards(_current.radius, _target.radius, _deltaRadius);

            return new PolarCoordinate2D(newTheta, newRadius);
        }

        static public PolarCoordinate2D MoveTowardsTargetThroughOrigin(PolarCoordinate2D _current, PolarCoordinate2D _target, float _stepDelta) {
            if (_current.theta == _target.theta || _current.radius <= 0.0f) {
                return new PolarCoordinate2D(_target.theta, Mathf.MoveTowards(_current.radius, _target.radius, _stepDelta));
            }

            float newTheta = _current.theta;
            float newRadius = _current.radius - _stepDelta;
            if (newRadius <= 0.0f) { newTheta = _target.theta; }

            newRadius = Mathf.Abs(newRadius);

            return new PolarCoordinate2D(newTheta, newRadius);
        }
        #endregion

        #region Lerp
        static public PolarCoordinate2D Lerp(PolarCoordinate2D _a, PolarCoordinate2D _b, float _t, bool _angularLerp) {
            float lerpTheta;
            if (_angularLerp) { lerpTheta = Mathf.LerpAngle(_a.degrees, _b.degrees, _t) * Mathf.Deg2Rad; }
            else { lerpTheta = Mathf.Lerp(_a.theta, _b.theta, _t); }
            
            float lerpRadius = Mathf.Lerp(_a.radius, _b.radius, _t);

            return new PolarCoordinate2D(lerpTheta, lerpRadius);
        }

        static public float InverseLerp(PolarCoordinate2D _a, PolarCoordinate2D _b, PolarCoordinate2D _t) {
            float inverseLerpTheta = Mathf.InverseLerp(_a.theta, _b.theta, _t.theta);
            float inverseLerpRadius = Mathf.InverseLerp(_a.radius, _b.radius, _t.radius);

            return (inverseLerpTheta + inverseLerpRadius) / 2.0f;
        }
        #endregion
    }
}

