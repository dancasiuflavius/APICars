using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookAPI.Books.Model;

namespace test.Products.Helper;

public class TestProductFactory
{
    public static List<Book> CreateBooks(int count)
    {
        var products = new List<Book>();
        for(int i=1; i<=count;i++)
        {
            products.Add(CreateBook(i));
        }
        return products;
    }
    public static Book CreateBook(int id)
    {
        return new Book
        {
            Id = id,
            Title = $"Book {id}",
            Author = $"Book {id}",
            Category = $"Category {id % 3 + 1}",
            PublishDate = DateTime.UtcNow.AddDays(id)
        };
    }
    public static List<Book> CreateBooksInCategory(string category, int count)
    {
        var products = new List<Book>();
        for(int i=1;i<=count;i++)
        {
            var product = CreateBook(i);
            product.Category = category;
            products.Add(product);
        }
        return products;
    }

    public static Book CreateProductWithNoCategory(int id)
    {
        var product = CreateBook(id);
        product.Category = null;
        return product;
    }
}
