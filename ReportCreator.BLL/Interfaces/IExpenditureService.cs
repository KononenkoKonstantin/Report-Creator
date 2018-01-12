using ReportCreator.BLL.DTOs;
using System.Collections.Generic;

namespace ReportCreator.BLL.Interfaces
{
    public interface IExpenditureService
    {
        void Add(ExpenditureDto expenditureDto);
        void Remove(ExpenditureDto expenditureDto);
        void Update(ExpenditureDto expenditureDto);
        ExpenditureDto GetById(int id);
        IEnumerable<ExpenditureDto> GetAll();        
        bool IsUnique(string name);
    }
}
