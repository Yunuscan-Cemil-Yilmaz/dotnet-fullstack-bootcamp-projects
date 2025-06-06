using Bogus;
using LibraryConsoleManager.Models;
using LibraryConsoleManager.Config;


namespace LibraryConsoleManager.Seed;

public static class DataSeeder
{
    public static void Seed(DBContext context)
    {
        const int expectedAuthorCount = 5;
        const int expectedBookCount = 10;

        int existingAuthorCount = context.Authors.Count();
        int existingBookCount = context.Books.Count();

        if (existingAuthorCount >= expectedAuthorCount && existingBookCount >= expectedBookCount)
        {
            Console.WriteLine("Database already seeded correctly. Skipping seed process.");
            return;
        }

 
        var missingAuthors = expectedAuthorCount - existingAuthorCount;
        var authors = new List<Author>();

        if (missingAuthors > 0)
        {
            var locales = new[] { "tr", "en", "fr", "de", "it" };

            var authorFaker = new Faker<Author>()
                .CustomInstantiator(f =>
                {
                    var locale = f.PickRandom(locales);
                    var lf = new Faker(locale);

                    return new Author
                    {
                        Name = lf.Name.FullName(),
                        Country = lf.Address.Country(),
                        BirthYear = lf.Date.Past(50, DateTime.Today.AddYears(-20)).Year
                    };
                });

            authors = authorFaker.Generate(missingAuthors);
            context.Authors.AddRange(authors);
            context.SaveChanges();
        }
        else
        {
            authors = context.Authors.ToList();
        }

        var missingBooks = expectedBookCount - existingBookCount;

        if (missingBooks > 0)
        {
            var bookFaker = new Faker<Book>()
                .RuleFor(b => b.Title, f => f.Lorem.Sentence(3))
                .RuleFor(b => b.Genre, f => f.PickRandom("Science Fiction", "Fantasy", "Mystery", "Romance", "History"))
                .RuleFor(b => b.PublishYear, f => f.Date.Past(20).Year)
                .RuleFor(b => b.AuthorId, f => f.PickRandom(authors).Id);

            var books = bookFaker.Generate(missingBooks);
            context.Books.AddRange(books);
            context.SaveChanges();
        }

        Console.WriteLine("Database seeded with missing data.");
    }
}