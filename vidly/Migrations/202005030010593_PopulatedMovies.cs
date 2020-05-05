namespace vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatedMovies : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Movies (Name, GenreId, ReleaseDate, DateAdded, Stock) VALUES ('Hangover', 1, '06-02-2009', '07-02-2009', 5)");
            Sql("INSERT INTO Movies (Name, GenreId, ReleaseDate, DateAdded, Stock) VALUES ('Die Hard', 2, '07-15-1988', '07-15-1988', 5)");
            Sql("INSERT INTO Movies (Name, GenreId, ReleaseDate, DateAdded, Stock) VALUES ('The Terminator', 2, '10-26-1984', '10-26-1984', 5)");
            Sql("INSERT INTO Movies (Name, GenreId, ReleaseDate, DateAdded, Stock) VALUES ('Toy Story', 3, '11-22-1995', '11-22-1995', 5)");
            Sql("INSERT INTO Movies (Name, GenreId, ReleaseDate, DateAdded, Stock) VALUES ('Titanic', 4, '12-19-1997', '12-19-1997', 5)");
        }
        
        public override void Down()
        {
        }
    }
}
