using Blaze.Common.Core;
using FluentValidation.Results;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace Blaze.Domain.Entities
{
    public class User : IdentityUser<Guid>, IDataErrorInfo
    {
        private readonly IValidator _validator;

        public User()
        {
            _validator = GetValidator();
            CreatedDate = DateTime.UtcNow;
            LastModifiedDate = DateTime.UtcNow;
            IsActive = true;
        }

        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {(string.IsNullOrEmpty(MiddleName) ? "" : MiddleName + " ")}{LastName}";
        public string? PictureUrl { get; set; }
       
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string? IsProbation { get; set; }
        public int ProbationPeriod { get; set; }
        public DateTime? ProbationEndDate { get; set; }
        public string? CompanyId { get; set; }
        public Company? Company { get; set; }
        public void ModifiedBy(string userId)
        {
            LastModifiedBy = userId;
            LastModifiedDate = DateTime.UtcNow;
        }

        #region Validation
        [IgnoreDataMember]
        [NotMapped]
        public virtual bool IsValid
        {
            get
            {
                if (ValidationErrors != null && ValidationErrors.Any())
                    return false;
                return true;
            }
        }

        [IgnoreDataMember]
        [NotMapped]
        public IEnumerable<ValidationFailure> ValidationErrors { get; private set; }

        [IgnoreDataMember]
        public string ValidationErrorsMessage
        {
            get
            {
                var errors = new StringBuilder();
                if (ValidationErrors != null && ValidationErrors.Any())
                    foreach (var validationError in ValidationErrors)
                        errors.AppendLine(validationError.ErrorMessage);
                return errors.ToString();
            }
        }

        protected virtual IValidator GetValidator()
        {
            return null;
        }

        public void Validate()
        {
            if (_validator != null)
            {
                var results = _validator.Validate((IValidationContext)this);
                ValidationErrors = results.Errors;
            }
        }
        #endregion

        #region IDataErrorInfo members
        string IDataErrorInfo.Error => string.Empty;
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                var errors = new StringBuilder();
                if (ValidationErrors != null && ValidationErrors.Any())
                    foreach (var validationError in ValidationErrors)
                        if (validationError.PropertyName == columnName)
                            errors.AppendLine(validationError.ErrorMessage);
                return errors.ToString();
            }
        }
        #endregion
    }
}