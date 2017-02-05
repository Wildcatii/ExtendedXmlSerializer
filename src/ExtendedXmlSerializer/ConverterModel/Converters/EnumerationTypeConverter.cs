// MIT License
// 
// Copyright (c) 2016 Wojciech Nag�rski
//                    Michael DeMond
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Reflection;
using ExtendedXmlSerialization.Core.Sources;
using ExtendedXmlSerialization.Core.Specifications;

namespace ExtendedXmlSerialization.ConverterModel.Converters
{
	class EnumerationTypeConverter : ConverterOptionBase
	{
		public static EnumerationTypeConverter Default { get; } = new EnumerationTypeConverter();
		EnumerationTypeConverter() : base(IsAssignableSpecification<Enum>.Default) {}

		public override IConverter Get(TypeInfo parameter) => new EnumerationConverter(parameter.AsType());

		class EnumerationConverter : ValueConverter<Enum>
		{
			public EnumerationConverter(Type enumerationType) : base(new Source(enumerationType).Get, x => x.ToString()) {}

			class Source : IParameterizedSource<string, Enum>
			{
				readonly Type _enumerationType;

				public Source(Type enumerationType)
				{
					_enumerationType = enumerationType;
				}

				public Enum Get(string parameter)
					=> parameter != null ? (Enum) Enum.Parse(_enumerationType, parameter) : default(Enum);
			}
		}
	}
}