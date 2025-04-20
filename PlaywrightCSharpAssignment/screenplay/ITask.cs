namespace PlaywrightCSharpAssignment.screenplay
{
    public interface ITask
    {
        Task PerformAs(Actor actor);
    }
}