namespace Licenta.Commons.Math
{
    public interface IOperative { IOperative Clone(); }    
    public interface IAdditive<in T> : IOperative { IOperative Add(T x); }    
    public interface ISubtrative<in T> : IOperative { IOperative Subtract(T x); }
    public interface IMultiplicative<in T> : IOperative { IOperative Multiply(T x); }
    public interface IDivisive<in T> : IOperative { IOperative Divide(T x); }

    public interface ISetAdditive<in T,out S> : IAdditive<T> 
        where S:IOperative where T : IOperative
    { new S Add(T x); }

    public interface ISetSubtractive<in T, out S> : ISubtrative<T>
        where S : IOperative where T : IOperative
    { new S Subtract(T x); }

    public interface ISetMultiplicative<in T, out S> : IMultiplicative<T>
        where S : IOperative where T : IOperative
    { new S Multiply(T x); }

    public interface ISetDivisive<in T, out S> : IDivisive<T>
        where S : IOperative where T : IOperative
    { new S Divide(T x); }

    public interface ISetAdditive<T> : ISetAdditive<T,T> where T:IOperative { }
    public interface ISetSubtractive<T> : ISetSubtractive<T,T> where T:IOperative { }

    public interface ISetMultiplicative<T> : ISetMultiplicative<T, T> where T : IOperative { }
    public interface ISetDivisive<T> : ISetDivisive<T, T> where T : IOperative { }

    public interface ISetAddSubOperative<T> : ISetAdditive<T>, ISetSubtractive<T> where T:IOperative { }
    public interface IMulDivSubOperative<T> : ISetMultiplicative<T>, ISetDivisive<T> where T:IOperative { }
    public interface IAddSubMulDivOperative<T> : ISetAddSubOperative<T>, IMulDivSubOperative<T> where T:IOperative { }

    public interface INumber : IAddSubMulDivOperative<INumber>
    {          
    }    
}
