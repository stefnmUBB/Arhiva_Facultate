using System;

namespace AI.Lab11.Tools.Clustering
{
    internal interface IVectorizer<T>
    {
        void Prepare(T[] trainData);
        double[] Vectorize(T item);
    }

    internal class TransformVectorizer<T> : IVectorizer<T>
    {
        private Func<T, double[]> Transform;
        public TransformVectorizer(Func<T, double[]> transform)
        {
            Transform = transform;
        }

        public virtual void Prepare(T[] trainData) { }
        public double[] Vectorize(T item) => Transform(item);
    }

    internal class PropertyVectorizer<T,P> : TransformVectorizer<T>
    {        
        private static Func<T, double[]> GetTransform(string propName, Func<P, double[]> transform)
        {
            var prop = typeof(T).GetProperty(propName);
            if (prop.PropertyType != typeof(P))
                throw new ArgumentException("Property type mismatch");
            return (item) => transform((P)prop.GetValue(item));
        }

        public PropertyVectorizer(string propName, Func<P, double[]> transform)
            : base(GetTransform(propName, transform))
        { }
    }


}
