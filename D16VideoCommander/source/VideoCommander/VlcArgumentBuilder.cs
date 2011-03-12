﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D16.VideoCommander
{
    abstract class VlcArgumentBuilder
    {
        protected VlcArgumentBuilder()
        {

        }

        protected readonly Dictionary<string, string> commands = new Dictionary<string, string>();

        protected void SetString(string key, string value)
        {
            if (commands.ContainsKey(key))
            {
                commands[key] = value;
            }
            else
            {
                commands.Add(key, value);
            }
        }

        protected void SetBoolean(bool value, string trueValue, string falseValue)
        {
            commands.Remove(trueValue);
            commands.Remove(falseValue);

            if (value)
            {
                commands.Add(trueValue, null);
            }
            else
            {
                commands.Add(falseValue, null);
            }
        }

        public virtual string GetArgumentString()
        {
            StringBuilder result = new StringBuilder();

            foreach (var item in commands)
            {
                string value = item.Value;

                if (String.IsNullOrEmpty(value))
                {
                    result.AppendFormat(" {0}", item.Key);
                }
                else
                {
                    if (value.Contains(" "))
                    {
                        value = String.Concat("\"", value, "\"");
                    }

                    result.AppendFormat(" {0}={1}", item.Key, value);
                }
            }

            return result.ToString().Trim();
        }
    }
}
