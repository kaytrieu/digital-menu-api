using AutoMapper;
using DigitalMenuApi.Data;
using DigitalMenuApi.Dtos.BoxDtos;
using DigitalMenuApi.Dtos.ProductListDtos;
using DigitalMenuApi.Dtos.ProductListProductDtos;
using DigitalMenuApi.Dtos.TemplateDtos;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;
using System;
using System.Linq;

namespace DigitalMenuApi.Service.Implement
{
    public class TemplateService : BaseService<Template>, ITemplateService
    {
        private readonly ITemplateRepository _templateRepository;
        private IBoxRepository _boxRepository;
        private readonly IProductListRepository _productListRepository;
        private IProductListProductRepository _productListProductRepository;
        private readonly IProductRepository _productRepository;
        private readonly IScreenTemplateRepository _screenTemplateRepository;

        public TemplateService(DigitalMenuSystemContext dbContext, IMapper mapper,
                               ITemplateRepository templateRepository, IBoxRepository boxRepository,
                               IProductListRepository productListRepository,
                               IProductListProductRepository productListProductRepository,
                               IProductRepository productRepository,
                               IScreenTemplateRepository screenTemplateRepository) : base(dbContext, mapper)
        {
            _templateRepository = templateRepository;
            _boxRepository = boxRepository;
            _productListRepository = productListRepository;
            _productListProductRepository = productListProductRepository;
            _productRepository = productRepository;
            _screenTemplateRepository = screenTemplateRepository;
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

        public bool UpdateTemplateDetail(int templateId, TemplateUpdateDto templateUpdateDto)
        {
            Template TemplateFromRepo = _templateRepository.Get(x => x.Id == templateId);

            var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                if (TemplateFromRepo == null)
                {
                    transaction.Rollback();

                    return false;
                }

                //Mapper to Update
                _mapper.Map(templateUpdateDto, TemplateFromRepo);

                foreach (var box in templateUpdateDto.Boxes)
                {
                    Box boxFromRepo = _boxRepository.Get(x => x.Id == box.Id);

                    if (boxFromRepo == null)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    _mapper.Map(box, boxFromRepo);
                    _boxRepository.SaveChanges();


                    foreach (var productList in box.ProductLists)
                    {
                        ProductList productListFromRepo = _productListRepository.Get(x => x.Id == productList.Id);

                        if (productListFromRepo == null)
                        {
                            transaction.Rollback();
                            return false;
                        }

                        _mapper.Map(productList, productListFromRepo);
                        _productListRepository.SaveChanges();


                        var productListProductfromRepoList = _productListProductRepository.GetAll(x => x.ProductListId == productList.Id && x.IsAvailable == true).OrderBy(x => x.Location);

                        var productListDtoList = productList.Products.OrderBy(x => x.Location);

                        //_mapper.Map(productListDtoList.ToList(), productListProductfromRepoList.ToList());

                        var diff = productListDtoList.Count() - productListProductfromRepoList.Count();

                        if (diff >= 0)
                        {
                            //mapping right productListProduct
                            for (int i = 0; i < productListProductfromRepoList.ToList().Count; i++)
                            {
                                var productListProduct = _productListProductRepository.Get(x => x.Id == productListProductfromRepoList.ToList()[i].Id);

                                if (productListProduct != null)
                                {
                                    var productTemp = productListDtoList.ToList()[i];
                                    productListProduct.ProductId = productTemp.Id;
                                    productListProduct.Location = productTemp.Location;
                                    _productListProductRepository.SaveChanges();
                                }
                            }

                            if (diff > 0)
                            {
                                //add more productListProduct
                                for (int i = productListDtoList.Count() - 1; i >= productListDtoList.Count() - diff; i--)
                                {
                                    ProductListProduct productListProduct = _mapper.Map<ProductListProduct>(productListDtoList.ToList()[i]);
                                    productListProduct.ProductListId = productList.Id;
                                    _productListProductRepository.Add(productListProduct);
                                    _productListProductRepository.SaveChanges();
                                }
                            }
                        }
                        else if (diff < 0)
                        {
                            //mapping right productListProduct
                            for (int i = 0; i < productListDtoList.ToList().Count; i++)
                            {
                                var productListProduct = _productListProductRepository.Get(x => x.Id == productListProductfromRepoList.ToList()[i].Id);

                                if (productListProduct != null)
                                {
                                    var productTemp = productListDtoList.ToList()[i];
                                    productListProduct.ProductId = productTemp.Id;
                                    productListProduct.Location = productTemp.Location;
                                    _productListProductRepository.SaveChanges();
                                }

                            }

                            for (int i = productListProductfromRepoList.Count() - 1; i >= productListProductfromRepoList.Count() + diff; i--)
                            {
                                _productListProductRepository.Get(x => x.Id == productListProductfromRepoList.ToList()[i].Id).IsAvailable = false;
                                _productListProductRepository.SaveChanges();
                                //_productListProductRepository.Delete();
                            }
                        }

                    }

                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }

            return true;
        }

        public int GetTemplateIdFromUDID(string udid)
        {
            return _screenTemplateRepository.Get(x => x.Screen.Uid == udid, x => x.Screen).TemplateId;
        }
    }
}
