using System;

namespace GarantiVP
{
    public class FormElementAttribute : Attribute
    {
        private string[] names;

        public FormElementAttribute(params string[] names)
        {
            this.names = names;
        }

        public string[] Names
        {
            get
            {
                return names;
            }
        }
    }
}