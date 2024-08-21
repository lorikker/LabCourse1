using LabCourse.Entities.BaseEntity;

namespace LabCourse.Entities
{
    public class Stoku : Base
    {

        public String EmriStokut { get; set; }

        public String LlojiProduktit { get; set; }

        public int Sasia { get; set; }

        public String Location { get; set; }

        public int UnitPrice { get; set; }
    }
}
