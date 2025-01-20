using Fitness_Center_Management.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Fitness_Center_Management.Services
{
    public class PagesViewService: IPagesViewService
    {
        private readonly ModelContext _context;
        public PagesViewService(ModelContext Context) 
        {
            _context = Context;
        }

        public PagesViewModel GetPagesViewModel()
        {
            return new PagesViewModel
            {
                Maininformationoffitnesscenter = _context.Maininformationoffitnesscenters.AsNoTracking().FirstOrDefault(),
                LandingSection = _context.Landingsections.AsNoTracking().FirstOrDefault(),
                FeatureSection = _context.Featuressections.AsNoTracking().FirstOrDefault(),
                BlogSections = _context.Blogsections.AsNoTracking().ToList(),
                Trainers = _context.Trainers.AsNoTracking().ToList()
            };
        }

    }
}
