using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<double>(type: "REAL", nullable: false),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    DirectorId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsFavourite = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsOnWatchlist = table.Column<bool>(type: "INTEGER", nullable: false),
                    Watched = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movies_Directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Should contain numerous scenes where action is spectacular and usually destructive. Often includes non-stop motion, high energy physical stunts, chases, battles, and destructive crises (floods, explosions, natural disasters, fires, etc.) Note: if a movie contains just one action scene (even if prolonged, i.e. airplane-accident) it does not qualify. Subjective.", "Action" },
                    { 2, "Should contain numerous consecutive and inter-related scenes of characters participating in hazardous or exciting experiences for a specific goal. Often include searches or expeditions for lost continents and exotic locales, characters embarking in treasure hunt or heroic journeys, travels, and quests for the unknown. Not to be confused with Action, and should only sometimes be supplied with it.", "Adventure" },
                    { 3, "Virtually all scenes should contain characters participating in humorous or comedic experiences. The comedy can be exclusively for the viewer, at the expense of the characters in the title, or be shared with them. Please submit qualifying keywords to better describe the humor (i.e. spoof, parody, irony, slapstick, satire, dark-comedy, comedic-scene, etc.). If the title does not conform to the 'virtually all scenes' guideline then please do not add the comedy genre; instead, submit the same keyword variations described above to signify the comedic elements of the title. The subgenre keyword \"dramedy-drama\" can also be used to categorize titles with comedic undertones that qualify for the Drama genre but not necessarily the Comedy genre.", "Comedy" },
                    { 4, "Whether the protagonists or antagonists are criminals this should contain numerous consecutive and inter-related scenes of characters participating, aiding, abetting, and/or planning criminal behavior or experiences usually for an illicit goal. Not to be confused with Film-Noir, and only sometimes should be supplied with it.", "Crime" },
                    { 5, "Should contain numerous consecutive scenes of characters portrayed to effect a serious narrative throughout the title, usually involving conflicts and emotions. This can be exaggerated upon to produce melodrama.", "Drama" },
                    { 6, "Should contain numerous consecutive scenes of characters portrayed to effect a magical and/or mystical narrative throughout the title. Usually has elements of magic, supernatural events, mythology, folklore, or exotic fantasy worlds.Note: not to be confused with Sci-Fi which is not usually based in magic or mysticism.", "Fantasy" },
                    { 7, "Should contain numerous consecutive scenes of characters effecting a terrifying and/or repugnant narrative throughout the title. Note: not to be confused with Thriller which is not usually based in fear or abhorrence.", "Horror" },
                    { 8, "Should contain numerous sensational scenes or a narrative that is sensational or suspenseful. Note: not to be confused with Mystery or Horror, and should only sometimes be accompanied by one (or both).", "Thriller" },
                    { 9, "Should contain numerous scenes and/or a narrative where the portrayal is similar to that of frontier life in the American West during 1600s to contemporary times.", "Western" }
                });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "DateOfBirth", "Description", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(1951, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "John McTiernan was born on January 8, 1951 in Albany, New York, USA. He is a director and producer, known for Die Hard (1988), Rollerball (2002) and Last Action Hero (1993). He has been married to Gail Sistrunk since 2012. He was previously married to Kate Harrington, Donna Dubrow and Carol Land.", "John", "McTiernan" },
                    { 2, new DateTime(1961, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sir Peter Jackson made history with The Lord of the Rings trilogy, becoming the first person to direct three major feature films simultaneously. The Fellowship of the Ring, The Two Towers and The Return of the King were nominated for and collected a slew of awards from around the globe, with The Return of the King receiving his most impressive collection of awards.", "Peter", "Jackson" },
                    { 3, new DateTime(1906, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Originally planning to become a lawyer, Billy Wilder abandoned that career in favor of working as a reporter for a Viennese newspaper, using this experience to move to Berlin, where he worked for the city's largest tabloid. He broke into films as a screenwriter in 1929 and wrote scripts for many German films until Adolf Hitler came to power in 1933.", "Billy", "Wilder" },
                    { 4, new DateTime(1963, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quentin Jerome Tarantino was born in Knoxville, Tennessee. His father, Tony Tarantino, is an Italian-American actor and musician from New York, and his mother, Connie (McHugh), is a nurse from Tennessee. Quentin moved with his mother to Torrance, California, when he was four years old.In January of 1992, first-time writer-director Tarantino's Reservoir Dogs (1992) appeared at the Sundance Film Festival.", "Quentin", "Tarantino" },
                    { 5, new DateTime(1951, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Three-time Oscar nominee Frank Darabont was born in a refugee camp in 1959 in Montbeliard, France, the son of Hungarian parents who had fled Budapest during the failed 1956 Hungarian revolution. Brought to America as an infant, he settled with his family in Los Angeles and attended Hollywood High School.", "Frank", "Darabont" },
                    { 6, new DateTime(1958, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Born in Pennsylvania and raised in Ohio, Chris Columbus was first inspired to make movies after seeing \"The Godfather\" at age 15. After enrolling at NYU film school, he sold his first screenplay (never produced) while a sophomore there. After graduation Columbus tried to sell his fourth script, \"Gremlins\", with no success, until Steven Spielberg optioned it; Columbus moved to Los Angeles for a year during rewrites on the project in Spielberg's bungalow at Universal.", "Chris", "Columbus" },
                    { 7, new DateTime(1928, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stanley Kubrick was born in Manhattan, New York City, to Sadie Gertrude (Perveler) and Jacob Leonard Kubrick, a physician. His family were Jewish immigrants (from Austria, Romania, and Russia). Stanley was considered intelligent, despite poor grades at school. Hoping that a change of scenery would produce better academic performance, Kubrick's father sent him in 1940 to Pasadena, California, to stay with his uncle, Martin Perveler.", "Stanley", "Kubrick" },
                    { 8, new DateTime(1961, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Darren Aronofsky was born February 12, 1969, in Brooklyn, New York. Growing up, Darren was always artistic: he loved classic movies and, as a teenager, he even spent time doing graffiti art. After high school, Darren went to Harvard University to study film (both live-action and animation). He won several film awards after completing his senior thesis film, \"Supermarket Sweep\", starring Sean Gullette, which went on to becoming a National Student Academy Award finalist. Aronofsky didn't make a feature film until five years later, in February 1996, where he began creating the concept for Pi (1998).", "Darren", "Aronofsky" },
                    { 9, new DateTime(1921, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sergio Leone was virtually born into the cinema - he was the son of Roberto Roberti (A.K.A. Vincenzo Leone), one of Italy's cinema pioneers, and actress Bice Valerian. Leone entered films in his late teens, working as an assistant director to both Italian directors and U.S. directors working in Italy (usually making Biblical and Roman epics, much in vogue at the time).", "Sergio", "Leone" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CategoryId", "Description", "DirectorId", "Duration", "IsFavourite", "IsOnWatchlist", "Name", "Rating", "Watched" },
                values: new object[,]
                {
                    { 1, 1, "A New York City police officer tries to save his estranged wife and several others taken hostage by terrorists during a Christmas party at the Nakatomi Plaza in Los Angeles.", 1, 132, false, false, "Die Hard", 8.1999999999999993, false },
                    { 2, 2, "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.", 2, 178, false, false, "The Lord of the Rings: The Fellowship of the Ring", 8.8000000000000007, false },
                    { 3, 3, "After two male musicians witness a mob hit, they flee the state in an all-female band disguised as women, but further complications set in.", 3, 121, false, false, "Some Like It Hot", 8.1999999999999993, false },
                    { 4, 4, "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.", 4, 154, true, false, "Pulp Fiction", 8.9000000000000004, true },
                    { 5, 5, "Over the course of several years, two convicts form a friendship, seeking consolation and, eventually, redemption through basic compassion.", 5, 144, true, false, "The Shawshank Redemption", 9.3000000000000007, true },
                    { 6, 6, "An orphaned boy enrolls in a school of wizardry, where he learns the truth about himself, his family and the terrible evil that haunts the magical world.", 6, 152, false, false, "Harry Potter and the Sorcerer's Stone", 7.5999999999999996, true },
                    { 7, 7, "A family heads to an isolated hotel for the winter where a sinister presence influences the father into violence, while his psychic son sees horrific forebodings from both past and future.", 7, 146, false, true, "The Shining", 8.4000000000000004, false },
                    { 8, 8, "Nina is a talented but unstable ballerina on the verge of stardom. Pushed to the breaking point by her artistic director and a seductive rival, Nina's grip on reality slips, plunging her into a waking nightmare.", 8, 108, false, false, "Black Swan", 8.0, false },
                    { 9, 9, "A bounty hunting scam joins two men in an uneasy alliance against a third in a race to find a fortune in gold buried in a remote cemetery.", 9, 178, false, false, "The Good, the Bad and the Ugly", 8.8000000000000007, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CategoryId",
                table: "Movies",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorId",
                table: "Movies",
                column: "DirectorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Directors");
        }
    }
}
