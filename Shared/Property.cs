﻿using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace EFCore.Scaffolding.Extension.Models
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class Property
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string DefaultValueSql { get; set; }

        [XmlAttribute]
        public string ValueGenerated { get; set; }

        [XmlAttribute]
        public string Column { get; set; }

        [XmlAttribute]
        public string Converter { get; set; }

        [XmlAttribute]
        public string Type { get; set; }

        [XmlAttribute]
        public string FK { get; set; }

        [XmlAttribute]
        public string Summary { get; set; }

        [XmlIgnore]
        public ValueConverterEnum ConverterEnum => string.IsNullOrEmpty(this.Converter) ? default : (ValueConverterEnum)Enum.Parse(typeof(ValueConverterEnum), this.Converter);

        [XmlIgnore]
        public string PrincipalTableName => FK.Split('.')[0];

        [XmlIgnore]
        public string PrincipalColumnName => FK.Split('.')[1];

        [XmlIgnore]
        public string ColumnName
        {
            get
            {
                if (string.IsNullOrEmpty(this.Column))
                {
                    return this.Name;
                }
                else
                {
                    return this.Column;
                }
            }
        }
    }
}