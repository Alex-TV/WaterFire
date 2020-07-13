
using System.Collections.Generic;
using Module.VisualElementsModule.Models;

namespace Module.VisualElementsModule.Providers
{
    public interface IVisualElementsProvider
    {
        IReadOnlyList<VisualElementsDescriptionModel> Elements { get; }
    }
}
