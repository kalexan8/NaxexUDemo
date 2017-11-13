using System.ComponentModel;

namespace NaxexUDemo.Models
{
    public class CourseViewModel
    {

        public CourseViewModel(Course course)
        {
            CourseId = course.CourseId;
            Title = course.Title;
            Description = course.Description;
            CourseCapacity = course.CourseCapacity;
            NumberEnrolled = course.NumberEnrolled;
            Credits = course.Credits;
        }

        public bool StudentIsEnrolled { get; set; }

        public int CourseId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        [DisplayName("Course Capacity")]
        public int CourseCapacity { get; set; }
        [DisplayName("Enrolled")]
        public int NumberEnrolled { get; set; }

        public int Credits { get; set; }
    }
}
