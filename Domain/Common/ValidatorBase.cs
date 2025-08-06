using Application.Common.Constants;
using Application.Setting;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Validator
{
    /// <summary>
    /// Base class for validating data using FluentValidation in the context of HTTP requests.
    /// </summary>
    /// <typeparam name="T">The type of the object to validate.</typeparam>
    public class ValidatorBase<T> : AbstractValidator<T> where T : class
    {
        /// <summary>
        /// Validates the specified object using FluentValidation rules and returns an HTTP response status.
        /// </summary>
        /// <param name="context">The validation context containing the object to validate.</param>
        /// <returns>
        /// An instance of <see cref="ResponseHttp"/> containing the HTTP status and validation failure messages.
        /// </returns>
        public ResponseHttp Validate(ValidationContext<T> context)
        {
            // Validate the object using FluentValidation rules
            var validationResult = base.Validate(context);

            // If validation is successful, return a 200 OK response
            if (validationResult.IsValid)
            {
                return new ResponseHttp
                {
                    Status = StatusCodes.Status200OK
                };
            }

            // If validation fails, return a 400 Bad Request response with failure messages
            string failureMessage = string.Concat(ValidationConstants.ValidationErrors,
                " : ",
                string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

            return new ResponseHttp
            {
                Status = StatusCodes.Status400BadRequest,
                Fail_Messages = failureMessage,
            };
        }
    }
}
