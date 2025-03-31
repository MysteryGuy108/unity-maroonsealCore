using UnityEngine;

namespace MaroonSeal.Maths.Shapes {

    public interface IShape {}
    public interface IShape<TTransform> : IShape where TTransform : IPointTransform {
        public TTransform Transform { get; }
    }

    public interface IShape3D : IShape<PointTransform> {

    }

    public interface IShape2D : IShape<PointTransform2D> {

    }
}