using AutoMapper;
using DigitalMenuApi.Data;
using DigitalMenuApi.Dtos.BoxDtos;
using DigitalMenuApi.Dtos.ProductListDtos;
using DigitalMenuApi.Dtos.ProductListProductDtos;
using DigitalMenuApi.Dtos.TemplateDtos;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Service.Implement
{
    public class TemplateService : BaseService<Template>, ITemplateService
    {
        private readonly ITemplateRepository _templateRepository;
        private IBoxRepository _boxRepository;
        private readonly IProductListRepository _productListRepository;
        private IProductListProductRepository _productListProductRepository;
        private readonly IProductRepository _productRepository;

        public TemplateService(DigitalMenuSystemContext dbContext, IMapper mapper,
                               ITemplateRepository templateRepository, IBoxRepository boxRepository,
                               IProductListRepository productListRepository,
                               IProductListProductRepository productListProductRepository,
                               IProductRepository productRepository) : base(dbContext, mapper)
        {
            _templateRepository = templateRepository;
            _boxRepository = boxRepository;
            _productListRepository = productListRepository;
            _productListProductRepository = productListProductRepository;
            _productRepository = productRepository;
        }

        public Template CreateNewTemplate(TemplateCreateDto templateDto, string uploadedFileLink)
        {
            Template template = _mapper.Map<Template>(templateDto);
            var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                template.Uilink = uploadedFileLink;

                _templateRepository.Add(template);
                _templateRepository.SaveChanges();

                foreach (BoxCreateWithTemplateDto boxDto in templateDto.Boxes)
                {
                    Box box = _mapper.Map<Box>(boxDto);

                    box.TemplateId = template.Id;

                    _boxRepository.Add(box);
                    _boxRepository.SaveChanges();

                    //reference to view
                    template.Box.Add(box);

                    foreach (ProductListCreateWithTemplateDto productListDto in boxDto.ProductLists)
                    {
                        ProductList productList = _mapper.Map<ProductList>(productListDto);

                        productList.BoxId = box.Id;

                        _productListRepository.Add(productList);
                        _productListRepository.SaveChanges();

                        box.ProductList.Add(productList);

                        foreach (ProductListProductCreateWithTemplateDto productListProductDto in productListDto.Products)
                        {
                            ProductListProduct productListProduct = _mapper.Map<ProductListProduct>(productListProductDto);
                            productListProduct.ProductListId = productList.Id;

                            Product product = _productRepository.Get(predicate: x => x.Id == productListProduct.ProductId
                                                            && x.IsAvailable == true);
                            if (product != null)
                            {
                                _productListProductRepository.Add(productListProduct);
                                _productListProductRepository.SaveChanges();
                            }
                            else
                            {
                                throw new Exception($"Product {productListProduct.ProductId} is not exist");
                            }

                            productListProduct.Product = product;
                        }
                    }

                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }

            return template;
        }
    }
}
