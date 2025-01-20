namespace Fitness_Center_Management.Models
{
    public class AdminDashboardViewModel
    {
        public IEnumerable<Contactform> ContactForm { get; set; }
        public IEnumerable<Admin> Admins { get; set; }
        public IEnumerable<User> users { get; set; }
        public IEnumerable<Usersubscription> usersubscriptions { get; set; }
        public IEnumerable<Feedback> feedbacks { get; set; }
    }
}
