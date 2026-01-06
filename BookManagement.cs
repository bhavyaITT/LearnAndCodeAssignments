class Book
{
    public string getTitle()
    {
        return "A Great Book";
    }

    public string getAuthor()
    {
        return "John Doe";
    }

    public void turnPage()
    {
        // pointer to next page
    }

    public string getCurrentPage()
    {
        return "current page content";
    }
}

public class LibraryManagement
{
    public string getLocation(Book book)
    {
        // Example logic
        return "Room 2, Shelf 5";
    }
}

public class BookRepository
{
    public void Save(Book book)
    {
        var fileName = $"{book.Title} - {book.Author}.json";
        var path = Path.Combine("documents", fileName);

        var json = JsonSerializer.Serialize(book);
        File.WriteAllText(path, json);
    }
}

public interface Printer
{
    void PrintPage(string page);
}

public class PlainTextPrinter : Printer
{
    public void PrintPage(string page)
    {
        Console.WriteLine(page);
    }
}

public class HtmlPrinter : IPrinter
{
    public void PrintPage(string page)
    {
        Console.WriteLine($"<div class='single-page'>{page}</div>");
    }
}