using Product.Entities.Dtos;
using Product.Entities.Models;
using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Product.Contracts.Services;
using Product.Contracts.Repositories;
using Product.Helper;
using System.Collections.Generic;
using System.Linq;
using Order.Entities.Dtos;

namespace Product.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IMapper mapper, IProductRepository ProductRepository, ICategoryRepository CategoryRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _productRepository = ProductRepository ?? throw new ArgumentNullException(nameof(ProductRepository));
            _categoryRepository = CategoryRepository ?? throw new ArgumentNullException(nameof(CategoryRepository));
        }

        ///<summary>
        ///create new product in db
        ///</summary>
        ///<param name="authId"></param>
        ///<param name="productInput"></param>
        public Guid CreateProduct(CreateProductDto productInput, Guid authId)
        {
            ProductDetail product = _mapper.Map<ProductDetail>(productInput);
            _productRepository.CreateProduct(product);
            _productRepository.Save(authId);
            return product.Id;
        }

        ///<summary>
        ///get category id by category name
        ///</summary>
        ///<param name="name"></param>
        public Category GetCategoryByName(string name)
        {
            return _categoryRepository.GetTypeByName(name);
        }

        ///<summary>
        ///get all product based on filter
        ///</summary>
        ///<param name="pageSortParam"></param>
        public List<ResultProductDto> GetAllProducts(PageSortParam pageSortParam, string role)
        {
            IEnumerable<ProductDetail> foundedUserList = _productRepository.GetAllProducts();
            if (role != "admin")
                foundedUserList = foundedUserList.Where(e => e.Visibility == true);

            PaginationHandler<ProductDetail> list = new PaginationHandler<ProductDetail>(pageSortParam);
            List<ProductDetail> filteredList = list.GetData(foundedUserList);
            List<ResultProductDto> productList = _mapper.Map<IEnumerable<ResultProductDto>>(filteredList).ToList();
            productList.ForEach(product =>
            {
                filteredList.ForEach(filProduct =>
                {
                    if (filProduct.Id == product.Id)
                    {
                        product.Type = _categoryRepository.GetTypeById(filProduct.CategoryId).Name;
                    }
                });
            });
            return productList;
        }

        ///<summary>
        ///get product by product id
        ///</summary>
        ///<param name="productId"></param>
        public ProductDto GetDetailedProductById(Guid productId)
        {
            ProductDetail product = _productRepository.GetProductById(productId);
            if (product == null)
                return null;

            ProductDto fetchedProduct = _mapper.Map<ProductDto>(product);
            fetchedProduct.Type = _categoryRepository.GetTypeById(product.CategoryId).Name;
            return fetchedProduct;
        }

        ///<summary>
        ///get product by product id
        ///</summary>
        ///<param name="productIds"></param>
        public List<ResultProductDto> GetProductByIds(List<Guid> productIds)
        {
            List<ResultProductDto> productList = new List<ResultProductDto>();
            foreach (Guid id in productIds)
            {
                ProductDetail product = _productRepository.GetProductById(id);
                if (product != null)
                {
                    ResultProductDto fetchedProduct = _mapper.Map<ResultProductDto>(product);
                    fetchedProduct.Type = _categoryRepository.GetTypeById(product.CategoryId).Name;
                    productList.Add(fetchedProduct);
                }
            }
            return productList;
        }

        ///<summary>
        ///get product by product id
        ///</summary>
        ///<param name="productId"></param>
        public ProductDetail GetProductById(Guid productId)
        {
            return _productRepository.GetProductById(productId);
        }

        ///<summary>
        ///delete product in database
        ///</summary>
        ///<param name="productId"></param>
        public void DeleteProduct(Guid productId)
        {
            ProductDetail productFromRepo = _productRepository.GetProductById(productId);
            productFromRepo.IsActive = false;
            _productRepository.Save(productId);
        }

        ///<summary>
        ///update proudct details 
        ///</summary>
        ///<param name="authId"></param>
        ///<param name="productFromRepo"></param>
        ///<param name="productInput"></param>
        public void UpdateProduct(UpdateProductDto productInput,Guid productId ,Guid authId, Guid categoryId)
        {
            ProductDetail productFromRepo = _productRepository.GetProductById(productId);
            productInput.Type = categoryId.ToString();
            _mapper.Map(productInput, productFromRepo);
            _productRepository.UpdateProduct(productFromRepo);
            _productRepository.Save(authId);
        }

        ///<summary>
        ///update product details list
        ///</summary>
        ///<param name="products"></param>
        public void UpdateProductList(List<UpdateProductQuantityDto> products)
        {
            foreach (UpdateProductQuantityDto productDetail in products)
            {
                //update product detail
                ProductDetail product = GetProductById(productDetail.Id);
                if (product != null)
                {
                    product.Quantity -= productDetail.Quantity;
                    _productRepository.UpdateProduct(product);
                    _productRepository.Save(productDetail.UserId);
                }
            }
            
        }
    }
}