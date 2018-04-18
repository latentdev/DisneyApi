using ridetimes.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ridetimes.ViewModel
{
    public class RideTimesViewModel
    {
        [Display(Name = "Disneyland")]
        public List<Entry> DisneyLand { get; set; }
        [Display(Name = "California Adventure")]
        public List<Entry> CaliforniaAdventure { get; set; }
    }
}