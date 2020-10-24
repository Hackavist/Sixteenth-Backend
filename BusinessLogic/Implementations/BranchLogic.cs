﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLogic.Interfaces;
using Models.DataModels.Core;
using Models.Enums;
using Repository;
using Repository.ExtendedRepositories;
using Services.DTOs;
using Services.Extensions;
using Services.Helpers;

namespace BusinessLogic.Implementations
{
    public class BranchLogic : IBranchLogic
    {
        private readonly IAddressRepository addressRepository;
        private readonly IRepository<Branch> branchRepository;
        private readonly IMapper mapper;

        public BranchLogic(IMapper mapper, IRepository<Branch> branchRepository, IAddressRepository addressRepository)
        {
            this.mapper = mapper;
            this.branchRepository = branchRepository;
            this.addressRepository = addressRepository;
        }

        public IEnumerable<BranchResponseDTO> GetAll()
        {
            return mapper.Map<IEnumerable<Branch>, IEnumerable<BranchResponseDTO>>(branchRepository.GetAll().ToList());
        }

        public BranchResponseDTO Get(int id)
        {
            return mapper.Map<Branch, BranchResponseDTO>(branchRepository.Get(id));
        }

        public int Insert(BranchRequestDTO entity)
        {
            Branch branch = ObjectHelpers.MapTo<Branch>(entity);
            branchRepository.Insert(branch).Wait();
            Address address = new Address
            {
                AddressLink = entity.AddressLink,
                BranchId = branch.Id,
                StreetName = entity.StreetName,
                District = entity.District.ToEnum<Districts>()
            };
            addressRepository.Insert(address);
            branch.AddressId = address.Id;
            branchRepository.Update(branch);
            return branch.Id;
        }

        public IEnumerable<int> InsertRange(IEnumerable<BranchRequestDTO> entities)
        {
            return entities.Select(Insert).ToList();
        }

        public void Update(int branchId, BranchRequestDTO entity)
        {
            Branch oldBranch = branchRepository.Get(branchId);
            Branch newBranch = ObjectHelpers.MapTo<Branch>(entity);
            newBranch.Id = branchId;
            ObjectHelpers.UpdateObjects(oldBranch, newBranch);
            branchRepository.Update(oldBranch);
        }

        public void SoftDelete(int id)
        {
            Branch x = branchRepository.Get(id);
            branchRepository.SoftDelete(x ?? throw new KeyNotFoundException($"{id} is not found in the database"));
        }
    }
}