using AutoMapper;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Entities;

namespace StudentLoanseBonderAPI.Helpers;

public class AutoMapperProfiles : Profile
{
	public AutoMapperProfiles()
	{
		CreateMap<Student, StudentReadDTO>();
		CreateMap<StudentCreateDTO, Student>().ForMember(x => x.NationalIdScan, options => options.Ignore()).ForMember(x => x.StudentIdScan, options => options.Ignore()).ForAllMembers(options => options.Ignore());
		CreateMap<StudentUpdateDTO, Student>().ForAllMembers(options => options.Ignore());

		CreateMap<User, UserReadDTO>();
		CreateMap<UserCreateDTO, User>().ForMember(x => x.Signature, options => options.Ignore()).ForMember(x => x.ProfilePicture, options => options.Ignore()).ForAllMembers(options => options.Ignore());
		CreateMap<UserUpdateDTO, User>().ForAllMembers(options => options.Ignore());
		
		CreateMap<BondingForm, BondingFormReadDTO>();
		CreateMap<BondingFormCreateDTO, BondingForm>().ForMember(x => x.StudentNationalIdScan, options => options.Ignore()).ForMember(x => x.StudentStudentIdScan, options => options.Ignore()).ForMember(x => x.StudentSignature, options => options.Ignore()).ForMember(x => x.InstitutionAdminSignature, options => options.Ignore()).ForMember(x => x.LoansBoardOfficialSignature, options => options.Ignore());

		CreateMap<BondingPeriod, BondingPeriodReadDTO>();
		CreateMap<BondingPeriodCreateDTO, BondingPeriod>();

		CreateMap<Institution, InstitutionReadDTO>();
		CreateMap<InstitutionCreateDTO, Institution>();
		CreateMap<InstitutionUpdateDTO, Institution>();
		CreateMap<Notification, NotificationDTO>();
	}
}
