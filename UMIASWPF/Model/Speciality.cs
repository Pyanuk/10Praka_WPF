
using System.Xml.Linq;

namespace UMIASWPF.Model
{
    public class Speciality
    {
        public int? IdSpeciality { get; set; }

        public string NameSpecialities { get; set; } = null!;

        public int NumberImage { get; set; }

        public Speciality(int imagePath, string namespecialities)
        {
            NumberImage = imagePath;
            NameSpecialities = namespecialities;
        }
    }
}