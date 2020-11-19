using System.Collections.Generic;

public class Book
{
    public string id { get; set; }
    public string name { get; set; }
    public string shelfId { get; set; }
}

public class Books
{
    public List<Book> books { get; set; }
}

