using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Affecto.EntityFramework.PostgreSql
{
    public class LowerCasePropertyNameConvention : IStoreModelConvention<EdmProperty>
    {
        public void Apply(EdmProperty property, DbModel model)
        {
            property.Name = property.Name.ToLower();
        }
    }
}