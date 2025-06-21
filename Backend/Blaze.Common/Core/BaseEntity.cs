using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Blaze.Common.Core
{
    public abstract class BaseEntity<TId> : IDataErrorInfo
    {
        private readonly IValidator _validator;

        protected BaseEntity()
        {
            _validator = GetValidator();
            CreatedDate = DateTime.UtcNow;
            LastModifiedDate = DateTime.UtcNow;
            IsActive = true;
        }

        public TId Id { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
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

        [IgnoreDataMember][NotMapped] public IEnumerable<ValidationFailure> ValidationErrors { get; private set; }

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
