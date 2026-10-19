using BLL.Interfaces;
using BLL.Service.Dto;
using DBL.Data;
using DBL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class MedicalServiceService : IMedicalServiceService
    {
        private readonly AppDbContext _context;
        public MedicalServiceService(AppDbContext context)
        {
            _context = context;
        }
        public MedicalServiceDto Create(CreateMedicalServiceDto dto)
        {
            var lastCode = _context.MadicalServices
                  .OrderByDescending(x => x.ServiceCode)
                  .Select(x => x.ServiceCode)
                  .FirstOrDefault();

                 int nextCode = 1;

            if (!string.IsNullOrEmpty(lastCode))
            {
                nextCode = int.Parse(lastCode) + 1;
            }

            var MediclService = new MadicalService
            {
                ServiceName=dto.ServiceName,
                Desription = dto.Desription,
                Price=dto.Price,
                IsActive=dto.IsActive,
                IsDeleted = false,
                DurationInMinutes=dto.DurationInMinutes,
                CostPrice=dto.CostPrice,
                SellingPrice=dto.SellingPrice,
                Clincid=dto.Clincid,
                ServiceCode = nextCode.ToString(),

            };
            _context.MadicalServices.Add(MediclService);
            _context.SaveChanges();
            return GetById(MediclService.Id)!;
        }
        public List<MedicalServiceDto> GatAll(string? ServiceName, string? ServiceCode, int? IsActive)
        {
            var query = from madicalService in _context.MadicalServices
                        join clinc in _context.Clincs
                        on madicalService.Clincid equals clinc.Id
                        where !madicalService.IsDeleted
                        select new
                        {
                            madicalService,
                            clinc
                        };

            if (!string.IsNullOrWhiteSpace(ServiceName))
            {
                query = query.Where(x =>
                    x.madicalService.ServiceName.Contains(ServiceName));
            }

            if (!string.IsNullOrWhiteSpace(ServiceCode))
            {
                query = query.Where(x =>
                    x.madicalService.ServiceCode.Contains(ServiceCode));
            }

            if (IsActive.HasValue)
            {
                query = query.Where(x => x.madicalService.IsActive == IsActive || IsActive == -1);
            }




            return query
               .Select(x => new MedicalServiceDto
                { 
                    Id = x.madicalService.Id,
                    ServiceName = x.madicalService.ServiceName,
                    ServiceCode = x.madicalService.ServiceCode,
                    Price = x.madicalService.Price,
                    Desription = x.madicalService.Desription,
                    IsActive = x.madicalService.IsActive,
                    DurationInMinutes = x.madicalService.DurationInMinutes,
                    CostPrice = x.madicalService.CostPrice,
                    SellingPrice = x.madicalService.SellingPrice,
                    Clincid = x.madicalService.Clincid,
                    ClincName = x.clinc.ClincName,

               })
                .ToList();
    }
        public bool Delete(int id)
        {
            var MedicalService = _context.MadicalServices
              .FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (MedicalService == null)
                return false;
            if (IsMedicalServiceConnected(id))
            {
                throw new Exception("Medical Service connected to other records.");
            }
            MedicalService.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }
        public MedicalServiceDto GetById(int id)
        {
            return _context.MadicalServices
                 .Where(x => x.Id == id && !x.IsDeleted)
                 .Select(x => new MedicalServiceDto
            {
                 Id = x.Id,
                 ServiceCode = x.ServiceCode,
                 ServiceName = x.ServiceName,
                 Desription = x.Desription,
                 Price = x.Price,
                 IsActive=x.IsActive,
                 IsDeleted = x.IsDeleted,
                 DurationInMinutes = x.DurationInMinutes,
                 CostPrice = x.CostPrice,
                 SellingPrice=x.SellingPrice,
                 Clincid=x.Clincid
            
                 })
                           .FirstOrDefault();
        }
        public bool Update(int id, UpdateMedicalServiceDto dto)
        {
            var MedicalService = _context.MadicalServices
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (MedicalService == null)
                return false;
            MedicalService.ServiceName = dto.ServiceName;
            MedicalService.Price = dto.Price;
            MedicalService.Desription=dto.Desription;
            if (MedicalService.IsActive == 1 && dto.IsActive == 0)
            {
                if (IsMedicalServiceConnected(id))
                {
                    throw new Exception("Medical Service connected to other records.");
                }
            }
            
            MedicalService.IsActive=dto.IsActive;
            MedicalService.DurationInMinutes = dto.DurationInMinutes;
            MedicalService.CostPrice = dto.CostPrice;
            MedicalService.SellingPrice = dto.SellingPrice;
            MedicalService.Clincid = dto.Clincid;
            _context.SaveChanges();

            return true;
        }
        private bool IsMedicalServiceConnected(int id)
        {
            var result = _context.Appointments.Where(x => x.MedicalServiceId == id).Any();
            return result;
        }
        

    }
}
