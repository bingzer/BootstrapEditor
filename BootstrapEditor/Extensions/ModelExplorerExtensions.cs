using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;

namespace BootstrapEditor.Extensions
{
    internal static class ModelExplorerExtensions
    {
        /// <summary>
        /// Returns TemplateHint if available otherwise look for UIHint property and get it from there
        /// </summary>
        /// <param name="modelExplorer"></param>
        /// <returns></returns>
        public static string? GetTemplateHint(this ModelExplorer modelExplorer)
        {
            var templateHint = modelExplorer.Metadata.TemplateHint;
            if (templateHint is not null)
            {
                return templateHint;
            }

            if (modelExplorer.Metadata is DefaultModelMetadata dm)
            {
                var uiHint = dm.Attributes
                    .PropertyAttributes
                    .OfType<UIHintAttribute>()
                    .Where(att => att.UIHint != null)
                    .FirstOrDefault();

                return uiHint?.UIHint;
                    
            }

            return null;
        }
    }
}
