using Entities.Models;

namespace Entities.Models
{
    public class DogParameters : QueryStringParameters
    {
        public DogParameters()
        {
            OrderBy = "name";
        }
    }
}
