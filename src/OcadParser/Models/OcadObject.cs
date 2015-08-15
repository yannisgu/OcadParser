namespace OcadParser.Models
{
    public class OcadObject
    {
        public Symbol Symbol { get; set; }
        public TdPoly[] Poly { get; set; }
        public OcadObjectStatus Status { get; set; }

        public enum OcadObjectStatus
        {
            Deleted = 0,
            Normal = 1,
            Hidden = 2,
            DeletedForUndo =3
        }
    }

}