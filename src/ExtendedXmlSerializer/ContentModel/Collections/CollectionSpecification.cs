// MIT License
// 
// Copyright (c) 2016-2018 Wojciech Nag�rski
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

using ExtendedXmlSerializer.Core;
using ExtendedXmlSerializer.Core.Specifications;
using ExtendedXmlSerializer.ReflectionModel;
using System.Reflection;

namespace ExtendedXmlSerializer.ContentModel.Collections
{
	/*
		sealed class CollectionContentOption : CollectionContentOptionBase
		{
			readonly IMemberSerializations _serializations;
			readonly IEnumerators _enumerators;
			readonly IInnerContentServices _contents;

			public CollectionContentOption(IActivatingTypeSpecification specification, IMemberSerializations serializations,
										   IEnumerators enumerators, ISerializers serializers, IInnerContentServices contents)
				: base(specification, serializers)
			{
				_serializations = serializations;
				_enumerators = enumerators;
				_contents = contents;
			}

			protected override ISerializer Create(ISerializer item, TypeInfo classification, TypeInfo itemType)
			{
				var members = _serializations.Get(classification);
				var handler = new CollectionWithMembersInnerContentHandler(_contents,
																	  new MemberInnerContentHandler(members, _contents, _contents),
																	  new CollectionInnerContentHandler(item, _contents));
				var reader = _contents.Create(classification, handler);
				var writer = new MemberedCollectionWriter(new MemberListWriter(members), new EnumerableWriter(_enumerators, item));
				var result = new Serializer(reader, writer);
				return result;
			}
		}
	*/

	public class CollectionSpecification : DecoratedSpecification<TypeInfo>
	{
		public CollectionSpecification(ISpecification<TypeInfo> specification) :
			base(IsCollectionTypeSpecification.Default.And(specification)) {}
	}
}