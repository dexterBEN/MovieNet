using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MovieNet
{
    class MovieNetContext: DbContext
    {
        public MovieNetContext():base()
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }

    }
}
