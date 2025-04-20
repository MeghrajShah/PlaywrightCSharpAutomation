namespace PlaywrightCSharpAssignment.screenplay
{
    public interface IQuestion<T>
    {
        Task<T> AnsweredBy(Actor actor);
    }
}