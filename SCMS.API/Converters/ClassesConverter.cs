using AutoMapper;
using SCMS.API.Converters.Interfaces;
using SCMS.API.DTO;
using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;
using System.Collections.Generic;

namespace SCMS.API.Converters
{
    public class ClassesConverter : IClassesConverter
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public ClassesConverter(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public List<GetClassDto> Convert(IEnumerable<Class> classes)
        {
            List<GetClassDto> getClassDtos = new List<GetClassDto>();

            foreach (var clas in classes)
            {
                var getClassDto = _mapper.Map<GetClassDto>(clas);
                var trainer = _usersRepository.GetSingle(getClassDto.TrainerUserId);
                getClassDto.TrainerName = trainer.Name;
                getClassDto.TrainerSurname = trainer.Surname;
                getClassDtos.Add(getClassDto);
            }

            return getClassDtos;
        }
    }
}
