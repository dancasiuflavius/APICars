using BookAPI.Books.DTO;
using BookAPI.Books.Model;
//using ProductsCrudApi.Products.Model.Comparers;
using BookAPI.Books.Repository;
using BookAPI.Books.Service.Interfaces;
using BookAPI.Books.Service;
using BookAPI.System.Constants;
using BookAPI.System.Exceptions;
using Moq;
using test.Products.Helper;
using BookAPI.Books.Repository.Interfaces;
using BookAPI.Books.Service;

namespace tests.Products.UnitTests;

public class ProductQueryServiceTests
{
    private readonly Mock<IBookRepository> _mockRepo;
    private readonly IBookQuerryService _service;

    public ProductQueryServiceTests()
    {
        _mockRepo = new Mock<IBookRepository>();
        _service = new BookQuerryService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllProducts_NoProductsExist_ThrowItemsDoNotExistException()
    {
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Book>());
        
        var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAllBooks());
        
        Assert.Equal(Constants.NO_PRODUCTS_EXIST, exception.Message);
    }
    
    [Fact]
    public async Task GetAllProducts_ProductsExist_ReturnsAllProducts()
    {
        var products = TestProductFactory.CreateBooks(3);

        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);
        var result = await _service.GetAllBooks();
        
        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
        Assert.Contains(products[0], result);
        Assert.Contains(products[1], result);
    }
    
    [Fact]
    public async Task GetProductsWithCategory_NoProducts_ThrowItemsDoNotExistException()
    {
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Book>());
        
        var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAllBooks());
        
        Assert.Equal(Constants.NO_PRODUCTS_EXIST, exception.Message);
    }
    
    [Fact]
    public async Task GetProductsWithCategory_NoProductsWithCategory_ThrowItemsDoNotExistException()
    {
        var products = TestProductFactory.CreateBooks(3);
        products.RemoveAt(0);
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);
        
        var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetBooksWithCategory("Category 2"));
        
        Assert.Equal(Constants.NO_PRODUCTS_EXIST, exception.Message);
    }
    
    [Fact]
    public async Task GetProductsWithCategory_ProductsFound_ReturnsAllFoundProducts()
    {
        var products = TestProductFactory.CreateBooks(3);

        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);
        var result = await _service.GetBooksWithCategory("Category 2");
        
        Assert.Single(result);
        Assert.Contains(products[0], result);
        Assert.DoesNotContain(products[1], result);
        Assert.DoesNotContain(products[2], result);
    }
    
    [Fact]
    public async Task GetProductsWithNoCategory_NoProducts_ThrowItemsDoNotExistException()
    {
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Book>());
        
        var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAllBooks());
        
        Assert.Equal(Constants.NO_PRODUCTS_EXIST, exception.Message);
    }
    
    [Fact]
    public async Task GetProductsWithNoCategory_NoProductsWithoutCategory_ThrowItemsDoNotExistException()
    {
        var products = TestProductFactory.CreateBooks(3);
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);
        
        var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetBooksWithNoCategory());
        
        Assert.Equal(Constants.NO_PRODUCTS_EXIST, exception.Message);
    }
    
    [Fact]
    public async Task GetProductsWithNoCategory_ProductsFound_ReturnsAllFoundProducts()
    {
        var products = TestProductFactory.CreateBooks(4);
        products.Add(TestProductFactory.CreateProductWithNoCategory(5));
        products.Add(TestProductFactory.CreateProductWithNoCategory(6));

        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);
        var result = await _service.GetBooksWithNoCategory();
        
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        
        for (int i = 0; i < 4; i++)
        {
            Assert.DoesNotContain(products[i], result);
        }

        Assert.Contains(products[4], result);
        Assert.Contains(products[5], result);
    }
    

    
    [Fact]
    public async Task GetProductById_ProductNotFound_ThrowItemDoesNotExistException()
    {
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Book)null);
        
        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetBookById(1));
        
        Assert.Equal(Constants.PRODUCT_DOES_NOT_EXIST, exception.Message);
    }
    
    [Fact]
    public async Task GetProductById_ProductFound_ReturnsProduct()
    {
        var product = TestProductFactory.CreateBook(1);

        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);
        var result = await _service.GetBookById(1);
        
        Assert.NotNull(result);
        Assert.Equal(product, result);
    }
}