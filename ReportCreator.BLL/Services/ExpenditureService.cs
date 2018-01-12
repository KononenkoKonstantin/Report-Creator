using AutoMapper;
using ReportCreator.BLL.DTOs;
using ReportCreator.BLL.Interfaces;
using ReportCreator.Domain.Entities;
using ReportCreator.Repository.Common;
using ReportCreator.Repository.Interfaces;
using ReportCreator.Repository.Repo;
using System.Collections.Generic;
using System.Linq;

namespace ReportCreator.BLL.Services
{
    public class ExpenditureService : IExpenditureService
    {
        private readonly IGenericRepository<Expenditure> _repoExpenditure;
        private readonly IUnitOfWork _unitOfWork;
        public ExpenditureService(ExpenditureRepository repoExpenditure, UnitOfWork unitOfWork)
        {
            _repoExpenditure = repoExpenditure;
            _unitOfWork = unitOfWork;
        } 
        public void Add(ExpenditureDto expenditureDto)
        {
            _repoExpenditure.Add(Mapper.Map<Expenditure>(expenditureDto));
            _unitOfWork.Save();
        }
        public void Remove(ExpenditureDto expenditureDto)
        {
            var expenditure = _repoExpenditure.Get(expenditureDto.ExpenditureId);
            if (expenditure != null)
            {
                _repoExpenditure.Delete(expenditure);
                _unitOfWork.Save();
            }
        }
        public void Update(ExpenditureDto expenditureDto)
        {
            var expenditure = _repoExpenditure.Get(expenditureDto.ExpenditureId);
            expenditure.CategoryId = expenditureDto.CategoryId;
            expenditure.Number = expenditureDto.Number;
            expenditure.Name = expenditureDto.Name;            
            if (expenditure != null)
            {
                //Mapper.Map(expenditureDto, expenditure);
                _repoExpenditure.Edit(expenditure);
                _unitOfWork.Save();
            }

        }
        
        public ExpenditureDto GetById(int id)
        {
            return Mapper.Map<ExpenditureDto>(_repoExpenditure.Get(id));
        }
        public IEnumerable<ExpenditureDto> GetAll()
        {
            return _repoExpenditure.GetAll()                                 
                 .Select(Mapper.Map<Expenditure, ExpenditureDto>)                 
                 .ToList();   
                 
        }
        
        public bool IsUnique(string name)
        {
            return (_repoExpenditure.FindBy(c => c.Name.ToLower() == name.ToLower()).Any());
        }
    }
}
