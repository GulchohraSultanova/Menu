using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Menu.Application.Absrtacts.Services;
using Menu.Application.Dtos.Account;
using Menu.Application.GlobalAppException;
using Menu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Persistence.Concreters.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<Admin> _userManager;
        private readonly SignInManager<Admin> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AdminService> _logger;

        public AdminService(UserManager<Admin> userManager, SignInManager<Admin> signInManager, RoleManager<IdentityRole> roleManager, IMapper mapper, ITokenService tokenService, ILogger<AdminService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _logger = logger;
        }

        // İstifadəçi qeydiyyatı metodu
        public async Task RegisterAdminAsync(RegisterDto registerDto)
        {
            var admin = _mapper.Map<Admin>(registerDto);

            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new GlobalAppException("Bu e-poçt ilə artıq bir istifadəçi mövcuddur!");
            }

            var result = await _userManager.CreateAsync(admin, registerDto.Password);
            if (!result.Succeeded)
            {
                throw new GlobalAppException($"İstifadəçi yaradılmadı: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            // "Admin" rolu varsa, yeni istifadəçiyə təyin edirik
            var roleExists = await _roleManager.RoleExistsAsync("Admin");
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            await _userManager.AddToRoleAsync(admin, "Admin");
        }

        // İstifadəçi giriş metodu
        public async Task<TokenResponseDto> LoginAdminAsync(LoginDto dto)
        {
            try
            {
                var user = await _userManager.Users
                    .Where(u => (u.Email == dto.Email))
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new GlobalAppException("Daxil edilən email ilə admin tapılmadı.");
                }

                var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
                if (!isPasswordValid)
                {
                    throw new GlobalAppException("Şifrə yalnışdır.");
                }

                var roles = await _userManager.GetRolesAsync(user);

                if (!roles.Any(r => r == "Admin"))
                {
                    throw new GlobalAppException("Giriş rədd edildi! Yalnız Admin  daxil ola bilər.");
                }

                var userRole = roles.FirstOrDefault();

                var tokenResponse = _tokenService.CreateToken(user, userRole);

                return tokenResponse;
            }
            catch (GlobalAppException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İstifadəçi daxil edilərkən bir səhv baş verdi");
                throw new GlobalAppException("İstifadəçi daxil edilərkən gözlənilməz bir səhv baş verdi", ex);
            }
        }

        // Admin silmə metodu
        public async Task DeleteAllAdminsAsync()
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");

            if (admins == null || !admins.Any())
            {
                throw new GlobalAppException("Silmək üçün heç bir admin tapılmadı!");
            }

            foreach (var admin in admins)
            {
                var result = await _userManager.DeleteAsync(admin);
                if (!result.Succeeded)
                {
                    throw new GlobalAppException($"Admin {admin.Email} silinərkən xəta baş verdi: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

    }
}
