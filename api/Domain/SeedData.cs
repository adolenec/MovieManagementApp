using System;
using api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Domain
{
    public static class Seed
    {
        public static void SeedData(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Action",
                    Description = "Should contain numerous scenes where action is spectacular and usually destructive. Often includes non-stop motion, high energy physical stunts, chases, battles, and destructive crises (floods, explosions, natural disasters, fires, etc.) Note: if a movie contains just one action scene (even if prolonged, i.e. airplane-accident) it does not qualify. Subjective."
                },
                new Category
                {
                    Id = 2,
                    Name = "Adventure",
                    Description = "Should contain numerous consecutive and inter-related scenes of characters participating in hazardous or exciting experiences for a specific goal. Often include searches or expeditions for lost continents and exotic locales, characters embarking in treasure hunt or heroic journeys, travels, and quests for the unknown. Not to be confused with Action, and should only sometimes be supplied with it.",
                },
                new Category
                {
                    Id = 3,
                    Name = "Comedy",
                    Description = "Virtually all scenes should contain characters participating in humorous or comedic experiences. The comedy can be exclusively for the viewer, at the expense of the characters in the title, or be shared with them. Please submit qualifying keywords to better describe the humor (i.e. spoof, parody, irony, slapstick, satire, dark-comedy, comedic-scene, etc.). If the title does not conform to the 'virtually all scenes' guideline then please do not add the comedy genre; instead, submit the same keyword variations described above to signify the comedic elements of the title. The subgenre keyword \"dramedy-drama\" can also be used to categorize titles with comedic undertones that qualify for the Drama genre but not necessarily the Comedy genre.",
                },
                new Category
                {
                    Id = 4,
                    Name = "Crime",
                    Description = "Whether the protagonists or antagonists are criminals this should contain numerous consecutive and inter-related scenes of characters participating, aiding, abetting, and/or planning criminal behavior or experiences usually for an illicit goal. Not to be confused with Film-Noir, and only sometimes should be supplied with it.",
                },
                new Category
                {
                    Id = 5,
                    Name = "Drama",
                    Description = "Should contain numerous consecutive scenes of characters portrayed to effect a serious narrative throughout the title, usually involving conflicts and emotions. This can be exaggerated upon to produce melodrama.",
                },
                new Category
                {
                    Id = 6,
                    Name = "Fantasy",
                    Description = "Should contain numerous consecutive scenes of characters portrayed to effect a magical and/or mystical narrative throughout the title. Usually has elements of magic, supernatural events, mythology, folklore, or exotic fantasy worlds.Note: not to be confused with Sci-Fi which is not usually based in magic or mysticism.",
                },
                new Category
                {
                    Id = 7,
                    Name = "Horror",
                    Description = "Should contain numerous consecutive scenes of characters effecting a terrifying and/or repugnant narrative throughout the title. Note: not to be confused with Thriller which is not usually based in fear or abhorrence.",
                },
                new Category
                {
                    Id = 8,
                    Name = "Thriller",
                    Description = "Should contain numerous sensational scenes or a narrative that is sensational or suspenseful. Note: not to be confused with Mystery or Horror, and should only sometimes be accompanied by one (or both).",
                },
                new Category
                {
                    Id = 9,
                    Name = "Western",
                    Description = "Should contain numerous scenes and/or a narrative where the portrayal is similar to that of frontier life in the American West during 1600s to contemporary times.",
                },
            });

            builder.Entity<Director>().HasData(new List<Director>
            {
                new Director
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "McTiernan",
                    DateOfBirth = new DateTime(1951, 1, 8),
                    Description = "John McTiernan was born on January 8, 1951 in Albany, New York, USA. He is a director and producer, known for Die Hard (1988), Rollerball (2002) and Last Action Hero (1993). He has been married to Gail Sistrunk since 2012. He was previously married to Kate Harrington, Donna Dubrow and Carol Land."
                },
                new Director
                {
                    Id = 2,
                    FirstName = "Peter",
                    LastName = "Jackson",
                    DateOfBirth = new DateTime(1961, 10, 31),
                    Description = "Sir Peter Jackson made history with The Lord of the Rings trilogy, becoming the first person to direct three major feature films simultaneously. The Fellowship of the Ring, The Two Towers and The Return of the King were nominated for and collected a slew of awards from around the globe, with The Return of the King receiving his most impressive collection of awards.",
                },
                new Director
                {
                    Id = 3,
                    FirstName = "Billy",
                    LastName = "Wilder",
                    DateOfBirth = new DateTime(1906, 6, 22),
                    Description = "Originally planning to become a lawyer, Billy Wilder abandoned that career in favor of working as a reporter for a Viennese newspaper, using this experience to move to Berlin, where he worked for the city's largest tabloid. He broke into films as a screenwriter in 1929 and wrote scripts for many German films until Adolf Hitler came to power in 1933.",
                },
                new Director
                {
                    Id = 4,
                    FirstName = "Quentin",
                    LastName = "Tarantino",
                    DateOfBirth = new DateTime(1963, 3, 27),
                    Description = "Quentin Jerome Tarantino was born in Knoxville, Tennessee. His father, Tony Tarantino, is an Italian-American actor and musician from New York, and his mother, Connie (McHugh), is a nurse from Tennessee. Quentin moved with his mother to Torrance, California, when he was four years old.In January of 1992, first-time writer-director Tarantino's Reservoir Dogs (1992) appeared at the Sundance Film Festival.",
                },
                new Director
                {
                    Id = 5,
                    FirstName = "Frank",
                    LastName = "Darabont",
                    DateOfBirth = new DateTime(1951, 1, 28),
                    Description = "Three-time Oscar nominee Frank Darabont was born in a refugee camp in 1959 in Montbeliard, France, the son of Hungarian parents who had fled Budapest during the failed 1956 Hungarian revolution. Brought to America as an infant, he settled with his family in Los Angeles and attended Hollywood High School.",
                },
                new Director
                {
                    Id = 6,
                    FirstName = "Chris",
                    LastName = "Columbus",
                    DateOfBirth = new DateTime(1958, 9, 10),
                    Description = "Born in Pennsylvania and raised in Ohio, Chris Columbus was first inspired to make movies after seeing \"The Godfather\" at age 15. After enrolling at NYU film school, he sold his first screenplay (never produced) while a sophomore there. After graduation Columbus tried to sell his fourth script, \"Gremlins\", with no success, until Steven Spielberg optioned it; Columbus moved to Los Angeles for a year during rewrites on the project in Spielberg's bungalow at Universal.",
                },
                new Director
                {
                    Id = 7,
                    FirstName = "Stanley",
                    LastName = "Kubrick",
                    DateOfBirth = new DateTime(1928, 7, 26),
                    Description = "Stanley Kubrick was born in Manhattan, New York City, to Sadie Gertrude (Perveler) and Jacob Leonard Kubrick, a physician. His family were Jewish immigrants (from Austria, Romania, and Russia). Stanley was considered intelligent, despite poor grades at school. Hoping that a change of scenery would produce better academic performance, Kubrick's father sent him in 1940 to Pasadena, California, to stay with his uncle, Martin Perveler.",
                },
                new Director
                {
                    Id = 8,
                    FirstName = "Darren",
                    LastName = "Aronofsky",
                    DateOfBirth = new DateTime(1961, 2, 12),
                    Description = "Darren Aronofsky was born February 12, 1969, in Brooklyn, New York. Growing up, Darren was always artistic: he loved classic movies and, as a teenager, he even spent time doing graffiti art. After high school, Darren went to Harvard University to study film (both live-action and animation). He won several film awards after completing his senior thesis film, \"Supermarket Sweep\", starring Sean Gullette, which went on to becoming a National Student Academy Award finalist. Aronofsky didn't make a feature film until five years later, in February 1996, where he began creating the concept for Pi (1998).",
                },
                new Director
                {
                    Id = 9,
                    FirstName = "Sergio",
                    LastName = "Leone",
                    DateOfBirth = new DateTime(1921, 1, 3),
                    Description = "Sergio Leone was virtually born into the cinema - he was the son of Roberto Roberti (A.K.A. Vincenzo Leone), one of Italy's cinema pioneers, and actress Bice Valerian. Leone entered films in his late teens, working as an assistant director to both Italian directors and U.S. directors working in Italy (usually making Biblical and Roman epics, much in vogue at the time).",
                },
            });

            builder.Entity<Movie>().HasData(new List<Movie>
            {
                new Movie
                {
                    Id = 1,
                    Name = "Die Hard",
                    Description = "A New York City police officer tries to save his estranged wife and several others taken hostage by terrorists during a Christmas party at the Nakatomi Plaza in Los Angeles.",
                    Rating = 8.2,
                    Duration = 132,
                    DirectorId = 1,
                    CategoryId = 1,
                    IsFavourite = false,
                    IsOnWatchlist = false,
                    Watched = false,
                },
                new Movie
                {
                    Id = 2,
                    Name = "The Lord of the Rings: The Fellowship of the Ring",
                    Description = "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.",
                    Rating = 8.8,
                    Duration = 178,
                    DirectorId = 2,
                    CategoryId = 2,
                    IsFavourite = false,
                    IsOnWatchlist = false,
                    Watched = false,
                },
                new Movie
                {
                    Id = 3,
                    Name = "Some Like It Hot",
                    Description = "After two male musicians witness a mob hit, they flee the state in an all-female band disguised as women, but further complications set in.",
                    Rating = 8.2,
                    Duration = 121,
                    DirectorId = 3,
                    CategoryId = 3,
                    IsFavourite = false,
                    IsOnWatchlist = false,
                    Watched = false,
                },
                new Movie
                {
                    Id = 4,
                    Name = "Pulp Fiction",
                    Description = "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                    Rating = 8.9,
                    Duration = 154,
                    DirectorId = 4,
                    CategoryId = 4,
                    IsFavourite = true,
                    IsOnWatchlist = false,
                    Watched = true,
                },
                new Movie
                {
                    Id = 5,
                    Name = "The Shawshank Redemption",
                    Description = "Over the course of several years, two convicts form a friendship, seeking consolation and, eventually, redemption through basic compassion.",
                    Rating = 9.3,
                    Duration = 144,
                    DirectorId = 5,
                    CategoryId = 5,
                    IsFavourite = true,
                    IsOnWatchlist = false,
                    Watched = true,
                },
                new Movie
                {
                    Id = 6,
                    Name = "Harry Potter and the Sorcerer's Stone",
                    Description = "An orphaned boy enrolls in a school of wizardry, where he learns the truth about himself, his family and the terrible evil that haunts the magical world.",
                    Rating = 7.6,
                    Duration = 152,
                    DirectorId = 6,
                    CategoryId = 6,
                    IsFavourite = false,
                    IsOnWatchlist = false,
                    Watched = true,
                },
                new Movie
                {
                    Id = 7,
                    Name = "The Shining",
                    Description = "A family heads to an isolated hotel for the winter where a sinister presence influences the father into violence, while his psychic son sees horrific forebodings from both past and future.",
                    Rating = 8.4,
                    Duration = 146,
                    DirectorId = 7,
                    CategoryId = 7,
                    IsFavourite = false,
                    IsOnWatchlist = true,
                    Watched = false,
                },
                new Movie
                {
                    Id = 8,
                    Name = "Black Swan",
                    Description = "Nina is a talented but unstable ballerina on the verge of stardom. Pushed to the breaking point by her artistic director and a seductive rival, Nina's grip on reality slips, plunging her into a waking nightmare.",
                    Rating = 8.0,
                    Duration = 108,
                    DirectorId = 8,
                    CategoryId = 8,
                    IsFavourite = false,
                    IsOnWatchlist = false,
                    Watched = false,
                },
                new Movie
                {
                    Id = 9,
                    Name = "The Good, the Bad and the Ugly",
                    Description = "A bounty hunting scam joins two men in an uneasy alliance against a third in a race to find a fortune in gold buried in a remote cemetery.",
                    Rating = 8.8,
                    Duration = 178,
                    DirectorId = 9,
                    CategoryId = 9,
                    IsFavourite = false,
                    IsOnWatchlist = false,
                    Watched = false,
                },
            });
        }
    }
}

