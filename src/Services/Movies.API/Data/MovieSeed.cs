using Movies.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movies.API.Data
{
    public static class MovieSeed
    {
        public static void SeedAsync(MovieDbContext movieDbContext) {
            if (!movieDbContext.Movies.Any())
            {
                var movies = new List<Movie>
                {
                    new Movie {
                        Id = 1,
                        Genre = "Drama",
                        Title = "The shawshank Redemption",
                        Rating ="5.3",
                        ImageUrl ="images/src",
                        ReleaseTime=new DateTime(1994,5,5),
                        Owner ="alice"
                    },
                    new Movie {
                        Id = 2,
                        Genre = "Crime",
                        Title = "The Godfather",
                        Rating ="9.2",
                        ImageUrl ="images/src",
                        ReleaseTime=new DateTime(1994,5,5),
                        Owner ="alice"
                    },
                    new Movie {
                        Id = 3,
                        Genre = "Action",
                        Title = "The Dark Knight",
                        Rating ="9.1",
                        ImageUrl ="images/src",
                        ReleaseTime=new DateTime(1994,5,5),
                        Owner ="bob"
                    },
                    new Movie {
                        Id = 4,
                        Genre = "Crime",
                        Title = "12 Angry Men",
                        Rating ="8.9",
                        ImageUrl ="images/src",
                        ReleaseTime=new DateTime(1994,5,5),
                        Owner ="bob"
                    },
                };
                movieDbContext.Movies.AddRange(movies);
                movieDbContext.SaveChanges();
            }
        }
    }
}
