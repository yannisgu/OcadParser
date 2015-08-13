
using System.Collections.Generic;
using System.Linq;

namespace OcadParser.Models.CourseSetting
{
    public class OcadCourseSettingProject : OcadBaseProject
    {
        public List<CourseSettingObject> CourseSettingObjects { get; } = new List<CourseSettingObject>();

        public override void Load(OcadFile ocadFile)
        {
            base.Load(ocadFile);
            LoadCsObjects(ocadFile);
            LoadCourses(ocadFile);
        }

        private void LoadCourses(OcadFile ocadFile)
        {
            foreach (var item in ocadFile.Strings.Where(_ => _.Record is CourseSSR))
            {
                var course = (CourseSSR) item.Record;
                var newCourse = new Course
                {
                    Objects =
                        course.CourseSettingObjects.Select(_ => CourseSettingObjects.FirstOrDefault(o => o.Code == _))
                            .ToList(),
                    Name = course.CourseName,
                    Climb = course.Climb,
                    ExtraDistance = course.ExtraDistance,
                    FirstStartNumber = course.FromStartNumber,
                    ControlDescriptionCourseName = course.ClassNameForControlDescription,
                    Combination = course.Combination
                };
                Courses.Add(newCourse);
            }

            foreach (var item in ocadFile.Strings.Where(_ => _.Record is CsClassSSR))
            {
                var recClass = (CsClassSSR)item.Record;
                var newClass = new Class()
                {
                    ClassName = recClass.ClassName,
                    Course =   Courses.FirstOrDefault(_ => _.Name == recClass.CourseName),
                    FromNumber = recClass.FromNumber,
                    NumberOfRunners = recClass.NumberOfRunners,
                    ToNumber = recClass.ToNumber
                };

                Classes.Add(newClass);
            }
        }

        public List<Class> Classes { get; } = new List<Class>();

        public List<Course> Courses { get; } = new List<Course>();

        private void LoadCsObjects(OcadFile ocadFile)
        {
            foreach (var item in ocadFile.Strings.Where(_ => _.Record is CsObjectSSR))
            {
                var csObject = (CsObjectSSR) item.Record;
                CourseSettingObject newObject = null;
                if (csObject.Type == "c")
                {
                    newObject = new ControlObject();
                }
                if (csObject.Type == "s")
                {
                    newObject = new StartObject();
                }
                if (csObject.Type == "m")
                {
                    newObject = new MarkedRouteObject();
                }

                if (csObject.Type == "f")
                {
                    newObject = new FinishObject();
                }
                if (csObject.Type == "d")
                {
                    newObject = new ControlDescriptionObject();
                }

                if (csObject.Type == "n")
                {
                    newObject = new CourseNameObject();
                }

                if (csObject.Type == "u")
                {
                    newObject = new StartNumberObject();
                }

                if (csObject.Type == "v")
                {
                    newObject = new VariationCodeObject();
                }

                if (csObject.Type == "t")
                {
                    newObject = new TextBlockObject();
                }
                if (newObject == null)
                {
                    newObject = new CourseSettingObject();
                }

                newObject.Code = csObject.Code;
                newObject.Object = Objects[item.ObjectIndex - 1];
                CourseSettingObjects.Add(newObject);
            }
        }
    }

    public class Class
    {
        public string ClassName { get; set; }
        public Course Course{ get; set; }
        public string NumberOfRunners { get; set; }
        public string FromNumber { get; set; }
        public string ToNumber { get; set; }
    }
}
