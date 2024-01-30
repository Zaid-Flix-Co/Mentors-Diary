using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.GroupEventStudents.Domains;
using MentorsDiary.Application.Entities.GroupEventStudents.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.GroupEventStudents.Repositories;

public class GroupEventStudentRepository(IMentorsDiaryContext context) : BaseRepository<GroupEventStudent>((DbContext)context), IGroupEventStudentRepository
{
    public async Task AddStudentsInGroupEvent(IEnumerable<GroupEventStudent> groupEventStudents)
    {
        await RemovingUnnecessaryStudents(groupEventStudents);

        if(groupEventStudents.FirstOrDefault()?.StudentId != 0)
        {
            await context.GroupEventStudents.AddRangeAsync(groupEventStudents);
            await Context.SaveChangesAsync();
        }
    }

    private async Task RemovingUnnecessaryStudents(IEnumerable<GroupEventStudent> groupEventStudents)
    {
        var eventStudents = context.GroupEventStudents
            .Where(g => g.GroupEventId == groupEventStudents.First().GroupEventId);

        if (eventStudents != null)
        {
            context.GroupEventStudents.RemoveRange(eventStudents);
            await Context.SaveChangesAsync();
        }
    }
}