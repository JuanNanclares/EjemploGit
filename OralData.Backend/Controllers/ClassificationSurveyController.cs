using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OralData.Backend.Controllers;
using OralData.Backend.Data;
using OralData.Backend.Interfaces;
using OralData.Backend.UnitsOfWork;
using OralData.Shared.Entities;

namespace OralData.Backend.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ClassificationSurveyController : GenericController<ClassificationSurvey>
    {
        public ClassificationSurveyController(IGenericUnitOfWork<ClassificationSurvey> unitOfWork, DataContext context) : base(unitOfWork, context)
        {
        }

    }
}

