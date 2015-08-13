using System.Collections.Generic;

namespace OcadParser.Models.CourseSetting
{
    public class Course
    {
        public List<CourseSettingObject> Objects { get; set; }
        public string Name { get; set; }
        public string Climb { get; set; }
        public string ExtraDistance { get; set; }
        public string FirstStartNumber { get; set; }
        public string ControlDescriptionCourseName { get; set; }
        public string Combination { get; set; }
    }
}