/// <copyright>
/// Copyright ? Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTest.TestModel
{
	public enum TableType : int
	{
		Classifier = 1,
		ClassifierFeatureMap = 2,
		ClassifierMap = 3,
		Column = 4,
		Dependency = 5,
		Element = 6,
		Expression = 7,
		Feature = 8,
		FeatureMap = 9,
		Index = 10,
		IndexedFeature = 11,
		KeyRelationship = 12,
		KeyRelationshipFeature = 13,
		ModelElement = 14,
		NameSpace = 15,
		Schema = 16,
		Table = 17,
		Transformation = 18,
		UniqueKey = 19,
		UniqueKeyFeature = 20,
		User = 21,
		Role = 22,
		Command = 23,
		DataItem = 24,
		DataSet = 25,
		Database = 26,
		FeatureMapSource = 27,
		ClassifierMapTarget = 28,
		ClassifierMapSource = 29,
		RdmsDatabase = 30,
		RdmsColumn = 31,
		RdmsTable = 32,
		Partition = 33
	};

	internal static class ElementType
	{
		internal static Element NewInstanceFromTableType(TableType type, Guid id)
		{
			Element obj = NewInstanceFromTableType(type);
			if (obj != null)
			{
				obj.Id = id;
			}

			return obj;
		}

		internal static Element NewInstanceFromTableType(TableType type)
		{
			switch (type)
			{
				case TableType.Classifier:
					return new Classifier();
				case TableType.Column:
					return new Column();
				case TableType.Dependency:
					return new Dependency();
				case TableType.Element:
					return null;
				case TableType.Expression:
					return new Expression();
				case TableType.Feature:
					return new Feature();
				case TableType.Index:
					return new Index();
				case TableType.IndexedFeature:
					return new IndexedFeature();
				case TableType.KeyRelationship:
					return new KeyRelationship();
				case TableType.ModelElement:
					return new ModelElement();
				case TableType.NameSpace:
					return new NameSpace();
				case TableType.Schema:
					return new Schema();
				case TableType.Table:
					return new Table();
				case TableType.UniqueKey:
					return new UniqueKey();
				case TableType.Database:
					return new Database();

				// These element types don't inherit element, which means they cannot be loaded from database with element id.
				case TableType.KeyRelationshipFeature:
				case TableType.UniqueKeyFeature:
				default:
					throw new ArgumentException();
			}
		}
	}
}
