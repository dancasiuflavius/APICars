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

namespace test.Products.UnitTests;

public class ProductsCommandServiceTests
{
    private readonly Mock<IBookRepository> _mockRepo;
    private readonly IBookComandService _service;

    public ProductsCommandServiceTests()
    {
        _mockRepo = new Mock<IBookRepository>();
        _service = new BookCommandService(_mockRepo.Object);
    }

    
    [Fact]
    public async Task CreateProduct_ProductWithSameNameAlreadyExists_ThrowsItemAlreadyExistsException()
    {
        var createRequest = new CreateBookRequest
        {
            Title = "New Book",
            Author = "New Author",     
            Category = "Test Category",
            Publish_Date = DateTime.UtcNow
        };

        var expectedProduct = TestProductFactory.CreateBook(1);
        expectedProduct.Title = createRequest.Title;


        var existingProduct = TestProductFactory.CreateBook(2);
        existingProduct.Title = createRequest.Title;

        _mockRepo.Setup(repo => repo.GetByTitleAsync(createRequest.Title)).ReturnsAsync(existingProduct);

        var exception = await Assert.ThrowsAsync<ItemAlreadyExists>(() => _service.CreateBook(createRequest));

        Assert.Equal(Constants.PRODUCT_ALREADY_EXISTS, exception.Message);
    }

    [Fact]
    public async Task CreateProduct_ValidData_ReturnsCreatedProduct()
    {
        var createRequest = new CreateBookRequest
        {
            Title = "New Book",
            Author = "New Author",
            Category = "Test Category",
            Publish_Date = DateTime.UtcNow
        };

        var expectedProduct = TestProductFactory.CreateBook(1);
        expectedProduct.Title = createRequest.Title;

        _mockRepo.Setup(repo => repo.GetByTitleAsync(It.IsAny<string>())).ReturnsAsync((Book)null!);
        _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<CreateBookRequest>())).ReturnsAsync(expectedProduct);

        var result = await _service.CreateBook(createRequest);

        Assert.NotNull(result);
        Assert.Equal(createRequest.Title, result.Title);
    }

    

    

    [Fact]
    public async Task DeleteProduct_ProductDoesNotExist_ThrowsItemDoesNotExistException()
    {
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Book)null!);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteBook(1));

        Assert.Equal(Constants.PRODUCT_DOES_NOT_EXIST, exception.Message);
    }

    //[Fact]
    //public async Task DeleteProduct_ValidData_ReturnsUpdatedProduct()
    //{
    //    var product = TestProductFactory.CreateProduct(1);

    //    _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

    //    var result = await _service.DeleteProduct(1);

    //    Assert.NotNull(result);
    //    Assert.Equal(product, result, new ProductEqualityComparer());
    //}
}
