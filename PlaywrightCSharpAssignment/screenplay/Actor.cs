using Microsoft.Playwright;

namespace PlaywrightCSharpAssignment.screenplay
{
    public class Actor
    {
        public string Name { get; }
        public IPage Page { get; }

        public Actor(string name, IPage page)
        {
            Name = name;
            Page = page;
        }

        public async Task AttemptsTo(params ITask[] tasks)
        {
            foreach (var task in tasks)
            {
                await task.PerformAs(this);
            }
        }

        public async Task<T> AsksFor<T>(IQuestion<T> question)
        {
            return await question.AnsweredBy(this);
        }
    }
}