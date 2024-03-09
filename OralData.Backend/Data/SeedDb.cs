using Microsoft.EntityFrameworkCore;
using OralData.Backend.Data;
using OralData.Backend.Helpers;
using OralData.Backend.services;
using OralData.Shared.Entities;
using OralData.Shared.Enums;
using OralData.Shared.Responses;

namespace OralData.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IApiService _apiService;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IApiService apiService, IUserHelper userHelper)
        {
            _context = context;
            _apiService = apiService;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckSpecialtiesAsync();
            await CheckCountriesAsync();
            await CheckRolesAsync();
            await CheckStudentsAsync();
            await CheckCourseEnrolledAsync();
            await CheckUserAsync("1010", "Oral", "Data", "oraldata@yopmail.com", "320 222 2688", "Calle planetaria", UserType.Admin);

        }
        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Name == "Medellín");
                if (city == null)
                {
                    city = await _context.Cities.FirstOrDefaultAsync();
                }

                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = city,
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
            await _userHelper.CheckRoleAsync(UserType.Patient.ToString());
            await _userHelper.CheckRoleAsync(UserType.Student.ToString());
            await _userHelper.CheckRoleAsync(UserType.Teacher.ToString());
        }


        private async Task CheckSpecialtiesAsync()
        {
            if (!_context.Specialties.Any())
            {
                _context.Specialties.Add(new Specialtie { Name = "Blanqueamiento dental" });
                _context.Specialties.Add(new Specialtie { Name = "Carillas dentales" });
                _context.Specialties.Add(new Specialtie { Name = "Implantes dentales " });
                _context.Specialties.Add(new Specialtie { Name = "Ortodoncia" });
                _context.Specialties.Add(new Specialtie { Name = "Periodoncia" });
                _context.Specialties.Add(new Specialtie { Name = "Endodoncia" });
                _context.Specialties.Add(new Specialtie { Name = "Prótesis dentales" });
                _context.Specialties.Add(new Specialtie { Name = "Cirugía oral" });
                _context.Specialties.Add(new Specialtie { Name = "Carillas bucales de Porcelana" });
                _context.Specialties.Add(new Specialtie { Name = "Férula dental" });
                _context.Specialties.Add(new Specialtie { Name = "Ortodoncia invisible" });
                _context.Specialties.Add(new Specialtie { Name = "Prótesis dental removible" });
                _context.Specialties.Add(new Specialtie { Name = "Sellador individual" });
                _context.Specialties.Add(new Specialtie { Name = "Pulpotomia" });
                _context.Specialties.Add(new Specialtie { Name = " Alargamiento de corona" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckStudentsAsync()
        {
            if (!_context.Students.Any())
            {
                _context.Students.Add(new Student { Name = "Pregrado" });
                _context.Students.Add(new Student { Name = "Posgrado" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCourseEnrolledAsync()
        {
            if (!_context.CoursesEnrolled.Any())
            {
                _context.CoursesEnrolled.Add(new CourseEnrolled { Name = "Periodoncia" });
                _context.CoursesEnrolled.Add(new CourseEnrolled { Name = "Ortodoncia" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                var responseCountries = await _apiService.GetAsync<List<CountryResponse>>("/v1", "/countries");
                if (responseCountries.WasSuccess)
                {
                    var countries = responseCountries.Result!;
                    foreach (var countryResponse in countries)
                    {
                        var country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == countryResponse.Name!)!;
                        if (country == null)
                        {
                            country = new() { Name = countryResponse.Name!, States = new List<State>() };
                            var responseStates = await _apiService.GetAsync<List<StateResponse>>("/v1", $"/countries/{countryResponse.Iso2}/states");
                            if (responseStates.WasSuccess)
                            {
                                var states = responseStates.Result!;
                                foreach (var stateResponse in states!)
                                {
                                    var state = country.States!.FirstOrDefault(s => s.Name == stateResponse.Name!)!;
                                    if (state == null)
                                    {
                                        state = new() { Name = stateResponse.Name!, Cities = new List<City>() };
                                        var responseCities = await _apiService.GetAsync<List<CityResponse>>("/v1", $"/countries/{countryResponse.Iso2}/states/{stateResponse.Iso2}/cities");
                                        if (responseCities.WasSuccess)
                                        {
                                            var cities = responseCities.Result!;
                                            foreach (var cityResponse in cities)
                                            {
                                                if (cityResponse.Name == "Mosfellsbær" || cityResponse.Name == "Șăulița")
                                                {
                                                    continue;
                                                }
                                                var city = state.Cities!.FirstOrDefault(c => c.Name == cityResponse.Name!)!;
                                                if (city == null)
                                                {
                                                    state.Cities.Add(new City() { Name = cityResponse.Name! });
                                                }
                                            }
                                        }
                                        if (state.CitiesNumber > 0)
                                        {
                                            country.States.Add(state);
                                        }
                                    }
                                }
                            }
                            if (country.StatesNumber > 0)
                            {
                                _context.Countries.Add(country);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
        }
    }
}
