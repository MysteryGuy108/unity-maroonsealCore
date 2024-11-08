using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

namespace MaroonSeal.Packages.EditorExtras {
    [System.Serializable]
    public class ComponentCallback
    {
        [SerializeField] protected Component targetComponent;
        [SerializeField] protected string methodName;

        public ComponentCallback(Component _target, string _methodName = "") {
            targetComponent = _target;
            methodName = _methodName;
        }
        
        public void Invoke() {
            FindCurrentMethod()?.Invoke(targetComponent, null);
        }

        protected MethodInfo FindCurrentMethod() {
            return targetComponent.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
        }
    }

    [System.Serializable]
    public class ComponentCallback<TResult> : ComponentCallback
    {
        public ComponentCallback(Component _target, string _methodName = "") : base(_target, _methodName) {}
        new public TResult Invoke() {
            return (TResult)FindCurrentMethod()?.Invoke(targetComponent, null);
        }
    }

    [System.Serializable]
    public class ComponentCallback<TResult, Arg0> : ComponentCallback<TResult>
    {
        public ComponentCallback(Component _target, string _methodName = "") : base(_target, _methodName) {}

        public TResult Invoke(Arg0 _arg0) {
            return (TResult)FindCurrentMethod()?.Invoke(targetComponent, new object[] {_arg0} );
        }
    }

    [System.Serializable]
    public class ComponentCallback<TResult, Arg0, Arg1> : ComponentCallback<TResult>
    {
        public ComponentCallback(Component _target, string _methodName = "") : base(_target, _methodName) {}

        public TResult Invoke(Arg0 _arg0, Arg1 _arg1) {
            return (TResult)FindCurrentMethod()?.Invoke(targetComponent, new object[] {_arg0, _arg1} );
        }
    }

    [System.Serializable]
    public class ComponentCallback<TResult, Arg0, Arg1, Arg2> : ComponentCallback<TResult>
    {
        public ComponentCallback(Component _target, string _methodName = "") : base(_target, _methodName) {}

        public TResult Invoke(Arg0 _arg0, Arg1 _arg1, Arg2 _arg2) {
            return (TResult)FindCurrentMethod()?.Invoke(targetComponent, new object[] {_arg0, _arg1, _arg2} );
        }
    }

    [System.Serializable]
    public class ComponentCallback<TResult, Arg0, Arg1, Arg2, Arg3> : ComponentCallback<TResult>
    {
        public ComponentCallback(Component _target, string _methodName = "") : base(_target, _methodName) {}

        public TResult Invoke(Arg0 _arg0, Arg1 _arg1, Arg2 _arg2, Arg3 _arg3) {
            return (TResult)FindCurrentMethod()?.Invoke(targetComponent, new object[] { _arg0, _arg1, _arg2, _arg3 } );
        }
    }
}