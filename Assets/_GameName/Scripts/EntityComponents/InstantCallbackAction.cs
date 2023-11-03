using System;

public class InstantCallbackAction<T>
{
    private T _value;
    private Action<T> _action;

    public void SetValue(T value)
    {
        _value = value;
        if (_action != null)
            _action(value);
    }
    
    public InstantCallbackAction<T> RegisterCallback(Action<T> callback, bool withCallback)
    {
        _action += callback;
        if (withCallback)
            _action(_value);
        return this;
    }

    public InstantCallbackAction<T> DeregisterCallback(Action<T> callback)
    {
        _action -= callback;
        return this;
    }

    public static InstantCallbackAction<T> operator +(InstantCallbackAction<T> a, Action<T> b)
    {
        a ??= new InstantCallbackAction<T>();
        return a.RegisterCallback(b, false);
    }

    public static InstantCallbackAction<T> operator -(InstantCallbackAction<T> a, Action<T> b)
    {
        a ??= new InstantCallbackAction<T>();
        return a.DeregisterCallback(b);
    }
}
