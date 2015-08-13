using System.Collections.Generic;

namespace OcadParser.Models.CourseSetting
{
    public class Course
    {
        public List<CourseSettingObject> Objects { get; set; }
        public string Name { get; set; }
    }
}