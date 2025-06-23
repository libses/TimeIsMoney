namespace TimeIsMoney.Extensions;

public static class TaskExtension
{
    public static Task<T> AsTask<T>(this T obj)
    {
        return Task.FromResult(obj);
    }
}