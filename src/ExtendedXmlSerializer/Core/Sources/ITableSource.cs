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

using System.Collections.Generic;

namespace ExtendedXmlSerializer.Core.Sources
{
	public interface ITableSource<TKey, TValue> : ISpecificationSource<TKey, TValue>,
	                                              IAssignable<TKey, TValue>
	{
		bool Remove(TKey key);
	}

	public class CoercedTable<TFrom, TTo, TValue> : ITableSource<TTo, TValue>
	{
		readonly ITableSource<TFrom, TValue> _table;
		readonly IParameterizedSource<TTo, TFrom> _coercer;

		public CoercedTable(ITableSource<TFrom, TValue> table, IParameterizedSource<TTo, TFrom> coercer)
		{
			_table = table;
			_coercer = coercer;
		}

		public bool IsSatisfiedBy(TTo parameter) => _table.IsSatisfiedBy(_coercer.Get(parameter));

		public TValue Get(TTo parameter) => _table.Get(_coercer.Get(parameter));

		public void Execute(KeyValuePair<TTo, TValue> parameter)
		{
			_table.Execute(Pairs.Create(_coercer.Get(parameter.Key), parameter.Value));
		}

		public bool Remove(TTo key) => _table.Remove(_coercer.Get(key));
	}

	public class DecoratedTable<TKey, TValue> : ITableSource<TKey, TValue>
	{
		readonly ITableSource<TKey, TValue> _table;

		public DecoratedTable(ITableSource<TKey, TValue> table)
			=> _table = table;

		public bool IsSatisfiedBy(TKey parameter) => _table.IsSatisfiedBy(parameter);

		public TValue Get(TKey parameter) => _table.Get(parameter);

		public bool Remove(TKey key) => _table.Remove(key);

		public void Execute(KeyValuePair<TKey, TValue> parameter)
		{
			_table.Execute(parameter);
		}
	}
}