using ReportCreator.BLL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ReportCreator.BLL.DTOs;
using ReportCreator.Repository.Common;
using ReportCreator.Domain.Entities;
using ReportCreator.Repository.Interfaces;
using ReportCreator.Repository.Repo;
using AutoMapper;

namespace ReportCreator.BLL.Services
{
    public class CategoryService : ICategoryService
    {        
        private readonly IGenericRepository<Category> _repoCategory;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(CategoryRepository repoCategory, UnitOfWork unitOfWork)
        {
            _repoCategory = repoCategory;
            _unitOfWork = unitOfWork;
        }
        public void Add(CategoryDto categoryDto)
        {
            _repoCategory.Add(Mapper.Map<Category>(categoryDto));
            _unitOfWork.Save();
        }
        public IEnumerable<CategoryDto> GetAll()
        {
            return _repoCategory.GetAll()                
                .Select(Mapper.Map<Category, CategoryDto>);
        }

        public CategoryDto GetById(int id)
        {
            return Mapper.Map<CategoryDto>(_repoCategory.Get(id));                
        }

        public bool IsUnique(string name)
        {
            return (_repoCategory.FindBy(c => c.Name.ToLower() == name.ToLower()).Any());
        }

        public void Remove(CategoryDto categoryDto)
        {

            var category = _repoCategory.Get(categoryDto.CategoryId);
            if (category != null)
            {               
                _repoCategory.Delete(category);
                _unitOfWork.Save();
            }
        }

        public void Update(CategoryDto categoryDto)
        {
            var category = _repoCategory.Get(categoryDto.CategoryId);
            if (category != null)
            {
                category.Name = categoryDto.Name;
                _unitOfWork.Save();
            }
        }
    }
}
