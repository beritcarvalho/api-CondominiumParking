using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CondominiumParkingApi.Api.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            var errors = new List<string>();
            foreach (var state in modelState)
            {
                var key = string.IsNullOrEmpty(state.Key) ? "Property" : state.Key;
                errors.AddRange(state.Value.Errors.Select(error => string.Format("{0}: {1}", key, error.ErrorMessage)));
            }

            return errors;
        }
    }
}
