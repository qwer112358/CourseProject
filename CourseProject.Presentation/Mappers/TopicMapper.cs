using CourseProject.Domain.Models;
using CourseProject.Presentation.ViewModels;

namespace CourseProject.Presentation.Mappers;

public static class TopicMapper
{
    public static TopicViewModel ToViewModel(this Topic topic)
    {
        return new TopicViewModel
        {
            Id = topic.Id,
            Name = topic.Name
        };
    }

    public static Topic ToModel(this TopicViewModel viewModel)
    {
        return new Topic
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
        };
    }
}
