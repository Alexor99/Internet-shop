using Microsoft.AspNetCore.Html;
using sirena.Infrustructure;
using sirena.Infrustructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ts.DatabaseEngine;
using Ts.DatabaseEngine.Models;

namespace sirena.HtmlHelpers
{
    public class PLiteral
    {
        static EntityService _db = null;
        static readonly List<Entity> literals = new List<Entity>();

        public static IHtmlContent PageLiteral(string name, string value, LiteralGroup? groupName = null)
        {
            var literal = GetLiteral(name, value, groupName);

            return new HtmlString(literal.Attributes["text"].ToString());
        }

        static Entity GetLiteral(string name, string value, LiteralGroup? groupName = null)
        {
            var literal = literals
                .FirstOrDefault(l => l.Attributes["name"].ToString() == name
                && l.Attributes["text"].ToString() == value
                && l.Attributes["groupname"].ToString() == groupName.Value.ToString());

            if (literal == null)
            {
                literal = GetLiteralInDB(name, value, groupName);

                literals.Add(literal);
            }

            return literal;
        }

        static Entity GetLiteralInDB(string name, string value, LiteralGroup? groupName = null)
        {
            if (_db == null)
                _db = new EntityService(Configuration.Config);

            var fieldLists = new List<KeyValuePair<string, object>>();

            fieldLists.Add(new KeyValuePair<string, object>("name", name));

            if (groupName.HasValue)
                fieldLists.Add(new KeyValuePair<string, object>("groupname", groupName.Value.ToString()));

            var literal = _db.RetrieveAsync("literals", fieldLists, new ColumnSet(true)).GetAwaiter().GetResult();

            if (literal == null)
            {
                literal = new Entity("literals", Guid.NewGuid());

                literal.Attributes.Add("name", name);
                literal.Attributes.Add("text", value);

                if (groupName.HasValue)
                    literal.Attributes.Add("groupname", groupName.Value.ToString());

                literal.Attributes.Add("createdon", DateTime.Now);

                _db.CreateAsync(literal).GetAwaiter();
            }

            return literal;
        }

    }
}
