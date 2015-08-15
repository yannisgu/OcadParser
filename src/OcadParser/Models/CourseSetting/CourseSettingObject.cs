namespace OcadParser.Models.CourseSetting
{
    public class CourseSettingObject
    {
        public string Code { get; set; }
        public OcadFileOcadObject Object { get; set; }
    }

    public class StartObject : CourseSettingObject
    {
    }

    public class FinishObject : CourseSettingObject
    {
    }
    public class MarkedRouteObject : CourseSettingObject
    {
    }

    public class ControlDescriptionObject : CourseSettingObject
    {
    }

    public class CourseNameObject : CourseSettingObject
    {
    }


    public class StartNumberObject : CourseSettingObject
    {
    }


    public class VariationCodeObject : CourseSettingObject
    {
    }

    public class TextBlockObject : CourseSettingObject
    {
    }

    public class ControlObject : CourseSettingObject
    {
    }
}