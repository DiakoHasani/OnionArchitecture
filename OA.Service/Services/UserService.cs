using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OA.Common.Helpers;
using OA.Data.Entities;
using OA.Service.DTO.User;
using OA.Service.General;

namespace OA.Service.Services;
internal class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    public UserService(UserManager<ApplicationUser> userManager,
    IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    public async Task<MessageModel> AddUser(RegisterUserModel model)
    {
        model.Email = model.Email.GetEnglishNumber();
        model.PhoneNumber = model.PhoneNumber.GetEnglishNumber();
        model.Password = model.Password.GetEnglishNumber();

        if (CheckExistUserByEmail(model.Email))
        {
            return new MessageModel
            {
                Message = "email is already exist"
            };
        }

        if (CheckExistUserByPhoneNumber(model.PhoneNumber))
        {
            return new MessageModel
            {
                Message = "phone number is already exist"
            };
        }

        var user = _mapper.Map<ApplicationUser>(model);
        user.EmailConfirmed = true;
        user.PhoneNumberConfirmed = true;
        user.UserName = model.Email;

        var resultAdd = await _userManager.CreateAsync(user, model.Password);
        if (resultAdd.Succeeded)
        {
            return new MessageModel
            {
                Result = true,
                Message = "Added User to database"
            };
        }
        else
        {
            var message = "";
            foreach (var error in resultAdd.Errors)
            {
                if (error.Code.Equals("PasswordRequiresNonAlphanumeric"))
                {
                    message += "Password must have at least one non-alphabetic character \n";
                }
                else if (error.Code.Equals("PasswordRequiresLower"))
                {
                    message += "Password must contain at least one lowercase letter \n";
                }
                else if (error.Code.Equals("PasswordRequiresUpper"))
                {
                    message += "Password must contain at least one uppercase letter \n";
                }
                else if (error.Code.Equals("PasswordTooShort"))
                {
                    message += "Password must be at least 8 characters long \n";
                }
                else
                {
                    message += error.Description + "\n";
                }
            }
            return new MessageModel { Message = message };
        }
    }

    public bool CheckExistUserByEmail(string email)
    {
        return _userManager.Users.Where(a => a.Email.Equals(email)).Any();
    }

    public bool CheckExistUserById(string userId)
    {
        return _userManager.Users.Where(a => a.Id.Equals(userId)).Any();
    }

    public bool CheckExistUserByPhoneNumber(string phoneNumber)
    {
        return _userManager.Users.Where(a => a.Email.Equals(phoneNumber)).Any();
    }

    public bool CheckExistUserByUserName(string userName)
    {
        return _userManager.Users.Where(a => a.UserName.Equals(userName)).Any();
    }

    public ApplicationUser GetUserByUserName(string userName)
    {
        return _userManager.Users.Where(a => a.UserName == userName).FirstOrDefault();
    }

    public async Task<ResponseLoginModel> Login(LoginBodyModel model)
    {
        model.UserName = model.UserName.GetEnglishNumber();
        model.Password = model.Password.GetEnglishNumber();

        var user = GetUserByUserName(model.UserName);
        if (user is null)
        {
            return new ResponseLoginModel { Message = "notfound user" };
        }

        if (!await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return new ResponseLoginModel { Message = "invalid password" };
        }

        return new ResponseLoginModel
        {
            Result = true,
            UserId = user.Id
        };
    }
}
public interface IUserService
{
    Task<MessageModel> AddUser(RegisterUserModel model);
    bool CheckExistUserByEmail(string email);
    bool CheckExistUserById(string userId);
    bool CheckExistUserByPhoneNumber(string phoneNumber);
    bool CheckExistUserByUserName(string userName);
    ApplicationUser GetUserByUserName(string userName);
    Task<ResponseLoginModel> Login(LoginBodyModel model);
}