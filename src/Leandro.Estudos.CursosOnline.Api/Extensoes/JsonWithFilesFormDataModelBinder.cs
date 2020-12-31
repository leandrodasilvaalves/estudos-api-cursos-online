using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Leandro.Estudos.CursosOnline.Api.Extensoes
{
  public class JsonWithFilesFormDataModelBinder : IModelBinder
  {
    private readonly MvcJsonOptions _jsonOptions;
    private readonly FormFileModelBinder _formFileModelBinder;

    public JsonWithFilesFormDataModelBinder(ILoggerFactory loggerFactory)
    {
      _jsonOptions = new MvcJsonOptions();
      _formFileModelBinder = new FormFileModelBinder(loggerFactory);
    }

    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
      if (bindingContext == null)
        throw new ArgumentNullException(nameof(bindingContext));

      var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);
      if (valueResult == ValueProviderResult.None)
      {
        var message = bindingContext.ModelMetadata.ModelBindingMessageProvider.MissingBindRequiredValueAccessor(bindingContext.FieldName);
        bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, message);
      }

      var rawValue = valueResult.FirstValue;
      var model = JsonConvert.DeserializeObject(rawValue, bindingContext.ModelType, _jsonOptions.SerializerSettings);
      foreach (var property in bindingContext.ModelMetadata.Properties)
      {
        if (property.ModelType != typeof(IFormFile))
          continue;

        var fieldName = property.BinderModelName ?? property.PropertyName;
        var modelName = fieldName;
        var propertyModel = property.PropertyGetter(bindingContext.Model);
        ModelBindingResult propertyResult;
        using (bindingContext.EnterNestedScope(property, fieldName, modelName, propertyModel))
        {
          await _formFileModelBinder.BindModelAsync(bindingContext);
          propertyResult = bindingContext.Result;
        }

        if (propertyResult.IsModelSet)
        {
          property.PropertySetter(model, propertyResult.Model);
        }
        else if (property.IsBindingRequired)
        {
          var message = property.ModelBindingMessageProvider.MissingBindRequiredValueAccessor(fieldName);
          bindingContext.ModelState.TryAddModelError(modelName, message);
        }
      }
      bindingContext.Result = ModelBindingResult.Success(model);
    }
  }

  //Extraido do código fonte original
  //https://github.com/aspnet/Mvc/blob/master/src/Microsoft.AspNetCore.Mvc.Formatters.Json/MvcJsonOptions.cs
  public class MvcJsonOptions : IEnumerable<ICompatibilitySwitch>
  {
    private readonly CompatibilitySwitch<bool> _allowInputFormatterExceptionMessages;
    private readonly ICompatibilitySwitch[] _switches;

    public MvcJsonOptions()
    {
      _allowInputFormatterExceptionMessages = new CompatibilitySwitch<bool>(nameof(AllowInputFormatterExceptionMessages));

      _switches = new ICompatibilitySwitch[]
      {
        _allowInputFormatterExceptionMessages,
      };
    }

    public bool AllowInputFormatterExceptionMessages
    {
      get => _allowInputFormatterExceptionMessages.Value;
      set => _allowInputFormatterExceptionMessages.Value = value;
    }

    public JsonSerializerSettings SerializerSettings { get; } = JsonSerializerSettingsProvider.CreateSerializerSettings();

    IEnumerator<ICompatibilitySwitch> IEnumerable<ICompatibilitySwitch>.GetEnumerator()
    {
      return ((IEnumerable<ICompatibilitySwitch>)_switches).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => _switches.GetEnumerator();
  }
}