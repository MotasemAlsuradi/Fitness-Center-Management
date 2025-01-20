namespace Fitness_Center_Management.Models
{
    public class PagesViewModel
    {
        public Maininformationoffitnesscenter Maininformationoffitnesscenter { get; set; }
        public Landingsection LandingSection { get; set; }
        public Featuressection FeatureSection { get; set; }
        public IEnumerable<Blogsection> BlogSections {  get; set; }
        public IEnumerable<Trainer> Trainers { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
