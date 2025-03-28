public class ReferenceContainer<T>
{
    public T Value{get; set;}

    public ReferenceContainer(T item)
    {
        Value = item;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator T (ReferenceContainer<T> item)
    {
        return item.Value;
    }
}
