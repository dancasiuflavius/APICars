//using BookAPI.Books.DTO;
//using BookAPI.Books.Model;
////using ProductsCrudApi.Products.Model.Comparers;
//using BookAPI.Books.Repository;
//using BookAPI.Books.Service.Interfaces;
//using BookAPI.Books.Service;
//using BookAPI.System.Constants;
//using BookAPI.System.Exceptions;
//using Moq;
//using test.Products.Helper;
//using Microsoft.AspNetCore.Mvc;
//using BookAPI.Books.Repository.Interfaces;
//using BookAPI.Books.Service;
//using BookAPI.Books.Controller;

//namespace tests.Products.UnitTests;

//public class ProductControllerTests
//{
//    private readonly Mock<IBookQuerryService> _mockQueryService;
//    private readonly Mock<IBookComandService> _mockCommandService;
//    private readonly BookApiController _controller;

//    public ProductControllerTests()
//    {
//        _mockQueryService = new Mock<IBookQuerryService>();
//        _mockCommandService = new Mock<IBookComandService>();
//        _controller = new BooksController(_mockQueryService.Object, _mockCommandService.Object);
//    }

//    [Fact]
//    public async Task GetProducts_NoProductsExist_ReturnsNotFound()
//    {
//        _mockQueryService.Setup(service => service.GetAllBooks())
//            .ThrowsAsync(new ItemsDoNotExist(Constants.NO_PRODUCTS_EXIST));

//        var result = await _controller.GetProducts();

//        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
//        Assert.Equal(Constants.NO_PRODUCTS_EXIST, notFoundResult.Value);
//        Assert.Equal(404, notFoundResult.StatusCode);
//    }

//    [Fact]
//    public async Task GetProducts_WhenProductsExist_ReturnsOkWithProducts()
//    {
//        var products = TestProductFactory.CreateBooks(2);
//        _mockQueryService.Setup(service => service.GetAllBooks()).ReturnsAsync(products);

//        var result = await _controller.GetProducts();

//        var okResult = Assert.IsType<OkObjectResult>(result.Result);
//        var returnedProducts = Assert.IsType<List<Book>>(okResult.Value);
//        Assert.Equal(2, returnedProducts.Count);
//        Assert.Equal(200, okResult.StatusCode);
//    }

//    [Fact]
//    public async Task CreateProduct_InvalidPrice_ReturnsBadRequest()
//    {
//        var createRequest = new CreateBookRequest
//        {
//            Title = "New Product",
//            Author = "Author",
//            Category = "Test Category",
//            Publish_Date = DateTime.UtcNow
//        };

//        _mockCommandService.Setup(service => service.CreateBook(It.IsAny<CreateBookRequest>()))
//            .ThrowsAsync(new InvalidPrice(Constants.INVALID_PRICE));

//        var result = await _controller.CreateProduct(createRequest);

//        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
//        Assert.Equal(Constants.INVALID_PRICE, badRequestResult.Value);
//        Assert.Equal(400, badRequestResult.StatusCode);
//    }

//    [Fact]
//    public async Task CreateProduct_ProductWithSameNameAlreadyExists_ReturnsBadRequest()
//    {
//        var createRequest = new CreateBookRequest
//        {
//            Title = "New Product",
//            Author = "Author",
//            Category = "Test Category",
//            Publish_Date = DateTime.UtcNow
//        };

//        _mockCommandService.Setup(service => service.CreateBook(It.IsAny<CreateBookRequest>()))
//            .ThrowsAsync(new ItemAlreadyExists(Constants.PRODUCT_ALREADY_EXISTS));

//        var result = await _controller.CreateProduct(createRequest);

//        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
//        Assert.Equal(Constants.PRODUCT_ALREADY_EXISTS, badRequestResult.Value);
//        Assert.Equal(400, badRequestResult.StatusCode);
//    }

//    [Fact]
//    public async Task CreateProduct_ValidData_ReturnsOkWithProduct()
//    {
//        var createRequest = new CreateBookRequest
//        {
//            Title = "New Product",
//            Author = "Author",
//            Category = "Test Category",
//            Publish_Date = DateTime.UtcNow
//        };

//        Book product = TestProductFactory.CreateBook(1);
//        product.Title = createRequest.Title;
//        product.Author = createRequest.Author;
//        product.Category = createRequest.Category;
//        product.PublishDate = createRequest.Publish_Date;

//        _mockCommandService.Setup(service => service.CreateBook(It.IsAny<CreateBookRequest>()))
//            .ReturnsAsync(product);

//        var result = await _controller.CreateProduct(createRequest);

//        var okResult = Assert.IsType<CreatedResult>(result.Result);
//        //Assert.Equal(product, okResult.Value as Book, new BookEqualityComparer()!);
//        Assert.Equal(201, okResult.StatusCode);
//    }

//    [Fact]
//    //public async Task UpdateProduct_InvalidPrice_ReturnsBadRequest()
//    //{
//    //    var updateRequest = new UpdateBookRequest
//    //    {
//    //        Id = 1,
//    //        Title = "New Product",
//    //        Author = "Author",
//    //        Category = "Test Category",
//    //        Publish_Date = DateTime.UtcNow
//    //    };

//    //    //_mockCommandService.Setup(service => service.UpdateBook(It.IsAny<UpdateBookRequest>()))
//    //        .ThrowsAsync(new InvalidPrice(Constants.INVALID_PRICE));

//    //    var result = await _controller.UpdateProduct(updateRequest);

//    //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
//    //    Assert.Equal(Constants.INVALID_PRICE, badRequestResult.Value);
//    //    Assert.Equal(400, badRequestResult.StatusCode);
//    //}

    

    
//}