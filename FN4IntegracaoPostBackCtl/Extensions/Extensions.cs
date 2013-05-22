using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace FN4IntegracaoPostBackCtl.Extensions
{
    public static class Extensions
    {
        public static string ToJson(this object obj)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(obj);

        }
    }
}
