namespace Pdc.Domain.Entities.CourseFramework;

public class CourseFrameworkContentElement
{
    public required ContentElement ContentElement { get; set; }
    public required CourseFramework CourseFramework { get; set; }
    //TODO soit une table many to many sur les COntentElement soit une table many to many sur changeable pis la on va chercher le bon type?....
    //J'ai aussi des éléments de compétence qui sont des strings
    public bool IsAssedElement { get; set; } = false;
}
