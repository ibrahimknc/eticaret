using AutoMapper;
using eticaret.Data;
using eticaret.Services.defaultPageServices.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eticaret.Services.defaultPageServices
{
    public class defaultPageService : IdefaultPageService
    {
        readonly dbeticaretContext _dbeticaretContext;
        readonly IMapper _mapper;

        public defaultPageService(dbeticaretContext dbeticaretContext, IMapper mapper)
        {
            _dbeticaretContext = dbeticaretContext;
            _mapper = mapper;
        }

        public List<lastProductsDto> getLastProducts(string number)
        {
            int topNumber = Convert.ToInt32(number);
            var result = _dbeticaretContext.products
                        .Where(p => p.isActive) 
                        .Select(g => new lastProductsDto
                        {
                            ProductID = g.id,
                            Name = g.name,
                            Tags = g.tags, 
                            Stock = g.stock,
                            CreatingTime = g.creatingTime, 
                            Details = g.details,
                            BasePrice = g.basePrice,
                            SalePrice = g.salePrice,
                            CategoryID = g.categoriID,
                            CategoryName = g.Category.name,
                            Image = g.image,
                            shippingAmount = g.shippingAmount,
                            commentCount = _dbeticaretContext.comments.Count(c => c.productID == g.id),
                            averageRating = _dbeticaretContext.comments
                                         .Where(c => c.productID == g.id)
                                         .Average(c => (double?)c.rating) ?? 0
                        }).OrderBy(x=>x.CreatingTime).Take(topNumber)
                        .ToList();
            return result;
        }

        List<lastCommentsDto> IdefaultPageService.getLastComments(string number)
        {
            int topNumber = Convert.ToInt32(number);
            var selLastComments = _dbeticaretContext.comments.Include(x => x.Product).Include(x => x.User).Where(x => x.isActive == true).OrderByDescending(x => x.creatingTime).Take(topNumber).ToList();
            var response = _mapper.Map<List<lastCommentsDto>>(selLastComments);
            return response;
        }
    }
}
