namespace TheEconomy.Server.Resources.KnowledgeTest.Models;

public class QuestionAnswer
{
    public int UUID { get; set; }
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
}

public class RoleQuestion
{
    public string Title { get; set; }
    public string Description { get; set; }
    public QuestionAnswer[] Answers { get; set; }
}